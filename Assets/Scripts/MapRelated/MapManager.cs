using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapManager : Colleague, ISubject //This doesn't have to be a Monobehaviour.  Update code to improve this!
{

    IMapStates deployState;
    IMapStates endOfDayMapState;

    IMapStates mapMode;

    [SerializeField] List<RegionAndKey> allLocations;

    [SerializeField] QuestInfoPanel questInfoPanel;
    public QuestInfoPanel getQuestInfoPanel { get { return questInfoPanel; } }
   
    [SerializeField] RectTransform MapImage;

    [SerializeField]
    MapVisualEffects mapVisualEffectScript;
    public MapVisualEffects MapVisualEffectScript { get { return mapVisualEffectScript; } }

    private Patron patronToGoOnAdventure;
    public Patron PatronToGoOnAdventure { get { return patronToGoOnAdventure; } }


    public DeployState deployStateScript;

    [SerializeField]
    MapOpen endOfDayMapStateScript;

    private List<Patron> questingPatrons = new List<Patron>();
    public List<Patron> QuestingPatrons { get { return questingPatrons; } }

    [System.Serializable]
    struct RegionAndKey
    {
        public Region region;
        public Region.LocationIdentifier key;
    }

    /// <summary>
    /// Sets a quest to a location by name of the quest.  The quest's name must match the name given in the inspector!
    /// </summary>
    /// <param name="targetQuest"></param>
    /// <returns>Returns true if the quest was added to the location successfully</returns>
    /// 

    public void openFromEndOfDay()
    {
        if (endOfDayMapState == null)
        {
            initEndOfDayState();
        }
        mapMode = endOfDayMapState;
        MapImage.gameObject.SetActive(true);
        mapMode.openMapProps();
    }

    public void mapOpenFromBar(Patron inPatronOnAdventure)   
    {
        if (deployState == null)
        {
            initDeployState();
        }
        mapMode = deployState;
        patronToGoOnAdventure = inPatronOnAdventure;
        MapImage.gameObject.SetActive(true);
        mapMode.openMapProps();
        SoundManager.Instance.AddCommand("OpenMap");
        notifyObserver(Mediator.ActionIdentifiers.MAP_OPEN);
    }

    public void closeMapProps()
    {
        MapImage.gameObject.SetActive(false);
        mapMode.closeMapProps();
        questInfoPanel.HideSelf();
    }

    public void regionClicked(Region nodesLocation) 
    {
        mapMode.regionClicked(nodesLocation);
    }

    public void ShowQuestInfoPanel()
    {
        mapMode.ShowQuestInfoPanel();
    }

    public void HideQuestInfoPanel()
    {
        mapMode.HideQuestInfoPanel();
    }

    public void FinishTaskOnMap()
    {
        mapMode.FinishTaskOnMap();
    }

    public void unlockRegion(string nameOfTargetLocation)
    {
        findRegionFromDictionary(nameOfTargetLocation).unlockLocation();
    }

    public void setQuestAtItsRegion(Quest targetQuest)
    {
        findRegionFromDictionary(targetQuest).AddNewQuestToThisLocation(targetQuest);
    }

    private void removeQuestFromRegion(Quest targetQuest)
    {
        findRegionFromDictionary(targetQuest).removeQuestFromLocation(targetQuest);
    }




    public void TimeProgressesForQuests()
    {
        foreach (Patron p in questingPatrons)
        {
            patronAttemptsQuestTrials(p);
            p.IsOnQuest = false;
            p.currentActivity = Patron.whatDoTheyWantToDo.TURNIN;
        }
    }

    private Region findRegionFromDictionary(Quest q)
    {
        for (int i = 0; i < allLocations.Count; i++)
        {
            if (allLocations[i].key.ToString() == q.QuestLocation)
            {
                return allLocations[i].region;
            }
        }

        Debug.Log("Could Not find location" + q.QuestLocation);
        return null;
    }

    private Region findRegionFromDictionary(string locationName)
    {
        for (int i = 0; i < allLocations.Count; i++)
        {
            if (allLocations[i].key.ToString() == locationName)
            {
                return allLocations[i].region;
            }
        }

        Debug.Log("Could Not find location" + locationName);
        return null;
    }

    private void initDeployState()
    {
        registerSelfToMediator(); // I don't know if this is the best place to put this, I might just bite the bullet and throw in a start. 
        deployState = deployStateScript;
    }

    private void initEndOfDayState()
    {
        endOfDayMapState = endOfDayMapStateScript;
        Debug.Log("This has been hit" + endOfDayMapStateScript);
    }

    public bool areThereAnyAdventuresForPatrons()
    {
        foreach (RegionAndKey LAK in allLocations)
        {
            if (LAK.region.QuestCountAtLocation > 0)
                return true;
        }
        return false;
    }

    public int CountHowManyQuestsAreAvailable()
    {
        int intToReturn = 0;
        foreach (RegionAndKey LAK in allLocations)
        {
            if (LAK.region.QuestCountAtLocation > 0)
            {
               foreach(Quest q in LAK.region.QuestsAtLocation)
                {
                    if (q.getQuestStatus() == Quest.questStatus.PENDING)
                    {
                        intToReturn++;
                    }
                }
            }
            else
            {
                continue;
            }
        }

        return intToReturn;
    }

    private void patronAttemptsQuestTrials(Patron patronToAtemptTrials)
    {
        for (int i = 0; i < patronToAtemptTrials.QuestToCompleete.TrialsOfTheQuest.Count; i++)
        {
            patronToAtemptTrials.QuestToCompleete.TrialsOfTheQuest[i].IsTrialPassed = patronToAtemptTrials.checkForSkill(patronToAtemptTrials.QuestToCompleete.TrialsOfTheQuest[i].skillToCheckFor);
        }

        patronToAtemptTrials.QuestToCompleete.ChangeQuestStatusToPass(); 
        removeQuestFromRegion(patronToAtemptTrials.QuestToCompleete);
    }

    public void sendPatronOnQuest(Patron patronToAdd)
    {
        Director.ReportOnPatronSentOnQuest(patronToAdd);
        questingPatrons.Add(patronToAdd);
        findRegionFromDictionary(patronToAdd.QuestToCompleete).addPatronsToLocation(patronToAdd);
    }

    public void removePatronFromQuestingList(Patron p)
    {
        findRegionFromDictionary(p.QuestToCompleete).removePatronFromLocation(p);
        questingPatrons.Remove(p);
    }

    public override void EndPhase()
    {
        FinishTaskOnMap();
        closeMapProps();
        Director.EndPhase(this);
    }

    #region observerStuff

    List<IObserver> observers = new List<IObserver>();

    public void registerSelfToMediator()// Nathan C added this observerStuff
    {
        Mediator.Register(this);
    }

    public void unregisterSelfToMediator()
    {
        Mediator.Unregister(this);
    }

    public void registerObserver(IObserver observerToAdd)
    {
        observers.Add(observerToAdd);
    }

    public void unregisterObserver(IObserver observerToRemove)
    {
        observers.Remove(observerToRemove);
    }

    public void notifyObserver(Mediator.ActionIdentifiers ActionIdentifier)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].notifyObserver(ActionIdentifier);
        }
    }
    #endregion


    /* NOTE:
     * I feel like I should use the state pattern?
     *      -> When the player clicks on a location, they should ONLY be able to interact with the "select a quest at this location" panel
     */

}
