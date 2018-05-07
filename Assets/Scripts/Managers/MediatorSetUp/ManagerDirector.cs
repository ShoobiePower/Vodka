using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class ManagerDirector : MonoBehaviour, IDirector
{

    [SerializeField] MapManager mapManager;
    [SerializeField] TimeOfDayManager timeManager;
    [SerializeField] EndOfDayManager endOfDayManager;
    [SerializeField] BarManager barManager;
    [SerializeField] EndOfDaySummaryManager endOfDaySummaryManager;
    [SerializeField] BattleReportManager battleReportManager;
    [SerializeField] RumorBoardUI rumorBoard;
    [SerializeField] PauseManager pauseManager;

    [SerializeField] MusicManager musicManager;

    [SerializeField]
    InputManager inputManager;


    private void Start()
    {
        mapManager.DEBUGUnlockAllLocations();
        musicManager.initMusicManager();
        //registerSelfToMediator();

        mapManager.SetDirector(this);
        timeManager.SetDirector(this);
        endOfDayManager.SetDirector(this);
        barManager.SetDirector(this);
        endOfDaySummaryManager.SetDirector(this);
        battleReportManager.SetDirector(this);
        rumorBoard.SetDirector(this);
        inputManager.SetDirector(this);
        pauseManager.SetDirector(this);
        // musicManager.SendMessage(this);

        //// HACK: Requird so the end of day manager dosen't display unknown for the first day. 
        endOfDayManager.AllPatronsTheBartenderKnows = barManager.AquirepatronManagerInformationForEndOfDayManager();

    }

    public void EndPhase(Colleague sender) 
    {
        if (sender == barManager)
        {
            timeManager.FadeToEndDay();
            musicManager.setCurrentMusicState(musicManager.GetBarFading());
        }

        else if (sender == timeManager) // When the end of day manager has finsihed playing the fade out animation.
        {
            endOfDaySummaryManager.setEndOfDaySummaryBoardActive();
            barManager.JumpToStarterSeat();
            endOfDayManager.AllPatronsTheBartenderKnows = barManager.AquirepatronManagerInformationForEndOfDayManager();          
        }

        else if (sender == endOfDaySummaryManager)
        {
            barManager.setBarState(barManager.barManagement());
            barManager.DisableAllUi();
            endOfDayManager.pullUpEndOfDayScreen();
            timeManager.fadeInText();
            musicManager.setCurrentMusicState(musicManager.GetMenuSetting());
        }

        else if (sender == endOfDayManager)
        {
            Debug.Log("End of day manager should end phase");
            barManager.EnableAllUI();
            barManager.setBarState(barManager.noOneInteractedWith());
            mapManager.TimeProgressesForQuests();

            timeManager.incrementDayCount();

            for (byte i = 0; i < mapManager.QuestingPatrons.Count; i++)
            {
                if (!mapManager.QuestingPatrons[i].IsOnQuest)
                {
                    barManager.SendAdventurerHome(mapManager.QuestingPatrons[i]);
                    mapManager.removePatronFromQuestingList(mapManager.QuestingPatrons[i]);
                    i--;
                }
            }
            barManager.checkIfAdventurersShouldSpawn(mapManager.areThereAnyAdventuresForPatrons());
            

            //if (tutorial.IsTutorialOver)
            //{
            //    patronManager.preparePatronsForDay();
            //}

           // notifyObserver(Mediator.ActionIdentifiers.DAY_STARTED);
        }

        else if (sender == battleReportManager)
        {
            UnlockContent(battleReportManager.UnlockersToRedeem);
            barManager.setBarState(barManager.noOneInteractedWith());
            battleReportManager.clearUnlockLists();
            barManager.ClickPatron();
        }

        else if (sender == mapManager)
        {
            barManager.SelectedSeat.TalkWithPatron(); // hacky, tells our patron to deliver text after being sent out, I would rather see this in IbarStateManager, but it has to go here, because map obstructs text box. 
            barManager.SelectedSeat.patron.IsOnQuest = true;
            Debug.Log("The adventure map is closed");
            barManager.setBarState(barManager.dismissPatron());
        }

        else if (sender == rumorBoard)
        {
            barManager.setBarState(barManager.dismissPatron());
            SoundManager.Instance.AddCommand("SelectQuest1"); // TODO: Randomize this command  
           
        }

    }

    #region things I would like to RE Re factor

    public void GetQuestFromBoard(Quest questToAdd)
    {
        endOfDaySummaryManager.QuestChosenTransaction(barManager.SelectedSeat.patron.Name, rumorBoard.getRumorName(), questToAdd.QuestName);   //HACK
        mapManager.setQuestAtItsRegion(questToAdd);
        endOfDayManager.addToQuestArchive(questToAdd);
        barManager.SelectedSeat.TalkWithPatron();// HACK
    }

    public void OpenRumorBoard()
    {
        rumorBoard.activatePatronRumorBoard();
    }

    // Need to refactor this as well
    public void SendRumorToBoard(Rumor rumor)
    {
        rumorBoard.labelPatronRumorBoard(rumor);
    }

    // wonder If I can do this better. My major problem with this is no one else uses this. 
    public void BarManagerOpensBattleReport()
    {
        battleReportManager.ReadBattleReport(barManager.SelectedSeat.patron);
    }


    public void OpenMapFromBar(Patron patronToSend)
    {
        mapManager.mapOpenFromBar(patronToSend);
    }

     public void OpenTavernKeeperJournalFromBar()
    {
        endOfDayManager.openTavernKeeperJournal();
        pauseManager.StoreBarState(barManager.BarManagerState); // HERE
        barManager.setBarState(barManager.barIsPaused());
    }

    public void CloseTavernKeeperJournalFromBar()
    {
        if (pauseManager.getStoredBarState() is BarPaused)
        {
            barManager.setBarState(barManager.noOneInteractedWith());
        }
        else
        barManager.setBarState(pauseManager.getStoredBarState());   // barManager.noOneInteractedWith()
    }

    // For now, I just want to get this done, but I need to refactor this. 
    // Big trouble is that I want to enforce single responcibility, 
    // This guy now mediates and unlocks. 
    // I don't feel that there is currently a way I know how to do this elegantly. Will think...
    public void UnlockContent(List<Unlocker> UnlockersToRedeem)
    {
        foreach (Unlocker u in UnlockersToRedeem)
        {
            switch (u.WhatDoesThisUnlock)
            {

                case Unlocker.WhatUnlocks.PATRON:
                    {
                        barManager.PatronManager.unlockNewPatronAndAdd(u.NameOfThingToUnlock);
                        break;
                    }

                case Unlocker.WhatUnlocks.RUMOR:
                    {
                        barManager.RumorManager.unlockRumorForPatron(u.NameOfThingToUnlock, u.SpecifierForNameOfThingToUnlock);
                        break;
                    }

                case Unlocker.WhatUnlocks.LOCATION:
                    {
                        mapManager.unlockRegion(u.NameOfThingToUnlock);
                        break;
                    }
                case Unlocker.WhatUnlocks.CONVERSATION:
                    {
                        barManager.ConversationWarehouse.unlockNewConversations(u.NameOfThingToUnlock, u.SpecifierForNameOfThingToUnlock);
                        Debug.Log("We just unlocked a conversation! It's name is: " + u.SpecifierForNameOfThingToUnlock);
                        break;
                    }

                case Unlocker.WhatUnlocks.INSTANTRUMOR:
                    {
                        Rumor rumorToLabelBordWith = barManager.RumorManager.unlockRumor(u.NameOfThingToUnlock);
                        rumorBoard.labelPatronRumorBoard(rumorToLabelBordWith);
                        rumorBoard.activatePatronRumorBoard();
                        Debug.Log("Board should be active");
                        barManager.setBarState(barManager.barIsPaused());
                        break;
                    }
            }
        }
    }

    public void ActivateMapManagerCommand(Command commandToActivate)
    {
        Debug.Log(commandToActivate);
        commandToActivate.Execute(mapManager);
    }

    public void ActivateBarManagerCommand(Command commandToActivate)
    {
        Debug.Log(commandToActivate);
        commandToActivate.Execute(barManager);
    }

    public void ActivateEndOfDayManagerCommand(Command commandToActivate)
    {
        Debug.Log(commandToActivate);
        commandToActivate.Execute(endOfDayManager);
    }

    public void ActivateEndOfDaySummaryCommand(Command commandToActivate)
    {
        Debug.Log(commandToActivate);
        commandToActivate.Execute(endOfDaySummaryManager);
    }

    public void ActivateRumorBoardUICommand(Command commandToActivate)
    {
        Debug.Log(commandToActivate + " From UI Command" );
        commandToActivate.Execute(rumorBoard);
    }

    public void ActivateBattleReportCommand(Command commandToActivate)
    {
        Debug.Log(commandToActivate);
        commandToActivate.Execute(battleReportManager);
    }

    public void ActivatePauseManagerCommand(Command commandToActivate)
    {
        commandToActivate.Execute(pauseManager);
    }

    public void PullUpExitMenu()
    {
        pauseManager.OpenExitGamePopUp(); 
        pauseManager.StoreBarState(barManager.BarManagerState);
        barManager.setBarState(barManager.barIsPaused());
    }

    public void LeaveExitMenu()
    {
        pauseManager.ResumeGame();

        if (pauseManager.getStoredBarState() is BarPaused)  // DUPE CODE please refactor;
        { 
            barManager.setBarState(barManager.noOneInteractedWith());
        }
        else
            barManager.setBarState(pauseManager.getStoredBarState()); 

    }

    // EndOfDay Summary Section

    public void ReportOnPatronSentOnQuest(Patron patronSentOnQuest)
    {
        endOfDaySummaryManager.RecordAdventureTransaction(patronSentOnQuest);
    }

    public void ReportOnPatronReturning(Patron returningPatron)
    {
        endOfDaySummaryManager.RecordPatronReturnTransaction(returningPatron);
    }

    public void RecordPatronLevelUp(Patron patronThatLeveledUp)
    {
        endOfDaySummaryManager.RecordPatronLevelUp(patronThatLeveledUp);
    }

    // concludes end of day summary ducttape;

    #endregion


}
