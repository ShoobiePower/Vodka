using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EndOfDayManager : Colleague {

    public GameObject endOfDayScreen;
  
    private List<Patron> allPatronsTheBartenderKnows = new List<Patron>();
    public List<Patron> AllPatronsTheBartenderKnows {get { return allPatronsTheBartenderKnows; } set { allPatronsTheBartenderKnows = value; } }

    //dirty for now, will refactor to it's own special thing after gdc.
    private List<Quest> questArchive = new List<Quest>();
    public List<Quest> QuestArchive { get { return questArchive; } }

    public Image backDropForEndOfDayManager;

    [SerializeField]
    Button OpenBarButton;

    [SerializeField]
    EndOfDayBook endOfDayBook;

    [SerializeField]
    EndOfDayMap endOfDayMap;

    [SerializeField]
    EndOfDayIdle endOfDayIdleScript;

    [SerializeField]
    PatronStatScreen patronStatScreenOpenedScript;

    [SerializeField]
    PatronBioScreenOpen patronBioScreenOpenedScript;

    [SerializeField]
    QuestPageOpen questPageOpenScript;

    [SerializeField]
    MapOpen mapOpenScript;

    [SerializeField]
    Button[] menuButtons;

    [SerializeField]
    Image[] menuTokens;

    IEndOfDayStates endOfDayIdle; 
    IEndOfDayStates patronStatScreenOpened;
    IEndOfDayStates patronBioScreenOpened;
    IEndOfDayStates questPageOpen;
    IEndOfDayStates mapOpen;

    IEndOfDayStates currentManagementState;


    public void Awake()
    {
        setUpAllEndOfDayStates();
        areEndOfDayPropsActive(false);
    }

    public void openTavernKeeperJournal()
    {
        endOfDayScreen.SetActive(true);
        setManagerState(swapToPatronStatScreen());
        areEndOfDayPropsActive(false);
    }

    public void openEndOfDayMap()
    {
        setManagerState(swapToMapOpenScreen());
        areEndOfDayPropsActive(false);
    }

    public void addToQuestArchive(Quest questToArchive)
    {
        questArchive.Add(questToArchive);
    }

    public void pullUpEndOfDayScreen()
    {
        setManagerState(swapToIdle());
        currentManagementState.ShowPresetAssets();
    }

    public void setManagerState(IEndOfDayStates newEndOfDayState)
    {
        currentManagementState.HidePresetAssets();
        currentManagementState = newEndOfDayState;
        currentManagementState.ShowPresetAssets();
    }

    public void ShowPresetAssets()
    {
        currentManagementState.ShowPresetAssets();
    }
    #region CommandPatternHooks

    public void FlipToEndOfDayIdle()
    {
        setManagerState(swapToIdle());
    }

    public void FlipToPatronStatPage()
    {
        setManagerState(swapToPatronStatScreen());
    }

    public void FlipToPatronBioPage()
    {
        setManagerState(swapToPatronBioScreen());
    }

    public void FlipToQuestPage()
    {
        setManagerState(swapToQuestScreenOpen());
    }

    public void FlipToMap()
    {
        setManagerState(swapToMapOpenScreen());
    }

    public void ScrollUp()
    {
        currentManagementState.ScrollUp();
    }

    public void ScrollDown()
    {
        currentManagementState.ScrollDown();
    }

    public void ShowStatsOnPage(byte index)
    {
       currentManagementState.ShowStatsOnPage(index);
    }

    #endregion

    public IEndOfDayStates swapToIdle()
    {
        areEndOfDayPropsActive(true);
        return endOfDayIdle;
    }

    public IEndOfDayStates swapToPatronStatScreen()
    {
        return patronStatScreenOpened;
    }

    public IEndOfDayStates swapToPatronBioScreen()
    {
        return patronBioScreenOpened;
    }

    public IEndOfDayStates swapToQuestScreenOpen()
    {
        return questPageOpen;
    }

    public IEndOfDayStates swapToMapOpenScreen()
    {
        return mapOpen;
    }

    private void setUpAllEndOfDayStates()
    {
        endOfDayIdle = endOfDayIdleScript;
        patronStatScreenOpened = patronStatScreenOpenedScript;
        patronBioScreenOpened = patronBioScreenOpenedScript;
        questPageOpen = questPageOpenScript;
        mapOpen = mapOpenScript;

        // for each child of this game object
        foreach (Transform child in transform)
        {
            if (child.GetComponent<IEndOfDayStates>() != null)
            {
                child.GetComponent<IEndOfDayStates>().passRefrenceToEndOfDayManager(this);
            }

            if (child.GetComponent<AbstBookStates>() != null)
            {
                child.GetComponent<AbstBookStates>().setButtonArray(menuButtons);
                child.GetComponent<AbstBookStates>().setTokenArray(menuTokens);
            }

            currentManagementState = endOfDayIdle;
        }
    }

    public void areEndOfDayPropsActive(bool yesNo)
    {
        OpenBarButton.gameObject.SetActive(yesNo);
        endOfDayBook.GetComponent<BoxCollider2D>().enabled = yesNo;
        endOfDayMap.GetComponent<BoxCollider2D>().enabled = yesNo;
    }

    public override void EndPhase()
    {
        areEndOfDayPropsActive(false);
        Director.EndPhase(this);
    }
}
