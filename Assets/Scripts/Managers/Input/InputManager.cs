using UnityEngine;
using System.Collections;

public class InputManager : Colleague {

    CommandWithUndo command;

   // public Colleague managerDirector;

    bool isMouseHeldDown = false;

    Vector2 touchPosition;
    const float buffer = 400f;

    // Update is called once per frame
    void Update()
    {
        command = null;
        scanForMouseDown();
        scanForMouseUp();
        scanForKeyboardInput();

        #if UNITY_IOS 
        CheckForMobileGestures();
        #endif
    }

    private void CheckForMobileGestures()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {

                Vector2 swipeDir = touch.position - touchPosition;


                //check if the direction of swipe is equal / greater than the buffer
                //find which direction was swiped more

                if (Mathf.Abs(swipeDir.x) > buffer && Mathf.Abs(swipeDir.x) > Mathf.Abs(swipeDir.y))
                {
                    if (swipeDir.x > 0)
                    {
                        //swiped right
                        scrollLeftInBar();
                    }
                    else
                    {
                        //swiped left
                        scrollRightInBar();
                    }
                }

            }

        }
    }


    private bool scanForInteractable() 
    {
        Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D[] col = Physics2D.OverlapPointAll(v);


        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
                if (c.GetComponent<Seat>() != null)
                {
                    interactWithPatron();
                }
                else if (c.GetComponent<EndOfDayBook>() != null) 
                {
                    openTavernkeeperJournal();
                }

                else if (c.GetComponent<EndOfDayMap>() != null) 
                {
                    openTavernkeeperMap();
                }
            }
        }
       return false;
    }


    private void scanForMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && isMouseHeldDown)
        {
            isMouseHeldDown = false;
        }
    }

    private void scanForMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !isMouseHeldDown)
        {
            isMouseHeldDown = true;
            scanForInteractable();
        }
    }

    public void interactWithPatron()
    {
        command = new ScanForPatron();
        Director.ActivateBarManagerCommand(command);
           
    }

    public void scanForKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PullUpQuitGameMenu();
        }
    }

   

    #region RumorPageChoiceCommands

    public void HilightQuestOption(int i)
    {
        command = new HilightQuestChoice(i);
        Director.ActivateRumorBoardUICommand(command);
    }

    public void selectQuestOption()
    {
        command = new SelectQuestOption();
        Director.ActivateRumorBoardUICommand(command);
    }

    #endregion

    #region DrinkCommands

    public void DispencerButtonPressed(int inventoryIndex)
    {
        command = new AddIngredientFromInventory((byte)inventoryIndex);
        Director.ActivateBarManagerCommand(command);
    }

    public void recycleDrinkButtonClicked()
    {
        command = new RecycleDrink();
        Director.ActivateBarManagerCommand(command);
    }

    public void serveDrink()
    {
        command = new ServeDrink();
        Director.ActivateBarManagerCommand(command);
    }

    #endregion

    #region adventureRelated
    public void acceptAdventure()
    {
        command = new AcceptAdventure();
        Director.ActivateMapManagerCommand(command);
    }

    //public void dismissAdventurer()
    //{
    //    command = new DismissAdventurer();
    //    Director.ActivateBarManagerCommand(command);
    //    closeAdventureMap();
    //}

    public void closeOutOfPatronDeployChoice()
    {
        command = new BackOutOfDeployChoice();
        Director.ActivateBarManagerCommand(command);
    }

    public void openAdventureMap()
    {
        command = new OpenAdventureMap();
        Director.ActivateBarManagerCommand(command);
    }

    public void backOutOfAdventureMap()
    {
        closeOutOfPatronDeployChoice();
        closeAdventureMap();
    }

    public void closeAdventureMap()
    {
        command = new CloseAdventureMap();
        Director.ActivateMapManagerCommand(command);
    }

    public void togglePatronCheatSheet()
    {
        command = new OpenAndClosePatronInfoSheet();
        Director.ActivateMapManagerCommand(command);
    }

    public void toggleQuestInfoPanel()
    {
        command = new ToggleQuestInfoPanel();
        Director.ActivateMapManagerCommand(command);
    }

    public void chooseQuestFromMenu(int questNumber)
    {
        command = new ChooseQuestFromMenu(questNumber);
        Director.ActivateMapManagerCommand(command);
    }

    public void scrollQuestMenuUp()
    {
        //
    }

    public void scrollQuestMenuDown()
    {
        //
    }
    #endregion

    #region EndOfDayManagementCommands


    public void openBar()
    {
        command = new OpenBar();
        Director.ActivateEndOfDayManagerCommand(command);
    }

    public void endOfDayButton(int buttonNum)
    {
        command = new GetInfoFromEndOfDayButton((byte)buttonNum);
        Director.ActivateEndOfDayManagerCommand(command);
    }

    public void openTavernkeeperJournal()
    {
        command = new OpenTaverkeeperJournal();
        Director.ActivateEndOfDayManagerCommand(command);

    }

    public void OpenTavernKeeperJournalFromBar()
    {
        Director.OpenTavernKeeperJournalFromBar();
    }

    public void openTavernkeeperMap()
    {
        command = new OpenEndOfDayMap();
        Director.ActivateEndOfDayManagerCommand(command);
    }
    public void scrollUpInEndOfDay()
    {
        command = new ScrollBarUp();
        Director.ActivateEndOfDayManagerCommand(command);
    }

    public void scrollDownInEndOfDay()
    {
        command = new ScrollEndOfDayMenuDown();
        Director.ActivateEndOfDayManagerCommand(command);
    }

    public void startNextDayAnimation()
    {
        command = new PlayStartNextDayAnimation();
        Director.ActivateEndOfDaySummaryCommand(command);
    }

    public void swapEndOfDayStateToIdle()
    {
        command = new SwapEndOfDayStateToIdle();
        Director.ActivateEndOfDayManagerCommand(command);
    }

    public void swapEndOfDayStateToPatronBio()
    {
        command = new SwapEndOfDayStateToPatronBio();
        Director.ActivateEndOfDayManagerCommand(command);
    }

    public void swapEndOfDayStateToPatronStat()
    {
        command = new SwapEndOfDayStateToPatronStat();
        Director.ActivateEndOfDayManagerCommand(command);
    }

    public void swapEndOfDayStateToQuest()
    {
        command = new SwapEndOfDayStateToQuest();
        Director.ActivateEndOfDayManagerCommand(command);
    }

    public void swapEndOfDayStateToMap()
    {
        command = new SwapEndOfDayStateToMap();
        Director.ActivateEndOfDayManagerCommand(command);
    }

    public void swapEndOfDayStateToDrinkMenu()
    {
        command = new SwapEndOfDayStateToDrinkMenu();
        Director.ActivateEndOfDayManagerCommand(command);
    }
    #endregion


    #region BarControlButtons 

    public void scrollLeftInBar()
    {
        command = new ScrollLeftInBar();
        Director.ActivateBarManagerCommand(command);
    }

    public void scrollRightInBar()
    {
        command = new ScrollRightInBar();
        Director.ActivateBarManagerCommand(command);
    }

    public void jumpToThisSeatInBar(int pies)
    {
        command = new JumpToSeat((sbyte) pies);
        Director.ActivateBarManagerCommand(command);
        SoundManager.Instance.AddCommand("UISound");
    }

    #endregion

    #region pauseMenuCommands

    public void TogglePauseMenu()
    {
        command = new TogglePauseGame();
        Director.ActivatePauseManagerCommand(command);
    }

    public void ContinueGame()
    {
        Director.LeaveExitMenu();
    }

    public void QuitGame()
    {
        command = new QuitToMainMenu();
        Director.ActivatePauseManagerCommand(command);
    }

    public void PullUpQuitGameMenu()
    {
        Director.PullUpExitMenu();
        
    }

    public void PullUpOptionsMenu()
    {
        Director.PullUpOptionsMenu();
    }

    public void CloseOptionsMenu()
    {
        Director.LeaveOptionsMenu();
    }
    #endregion

    public void TurnPageInBattleReport()
    {
        command = new TurnPageInBattleReport();
        Director.ActivateBattleReportCommand(command);
    }

    public void CloseEndOfDaySummary()
    {
        command = new CloseEndOfDaySummary();
        Director.ActivateEndOfDaySummaryCommand(command);
    }

}
