using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class BarManager : Colleague , ISubject
{
    
    #region Props
    // A prop is a dependancie called from the inspector, click and drag to slot
    public Seat[] Seats = new Seat[3];

    private Seat selectedSeat;
    public Seat SelectedSeat { get { return selectedSeat; } set { selectedSeat = value; } }

    #region dependancies
    
    private PatronManager patronManager;
    public PatronManager PatronManager { get { return patronManager; } }

    [SerializeField] OrderManager orderManager;
    public OrderManager OrderManager { get { return orderManager; } }

    private ConversationWarehouse conversationWarehouse;
    public ConversationWarehouse ConversationWarehouse { get { return conversationWarehouse; } }
   
    private RumorManager rumorManager;
    public RumorManager RumorManager { get { return rumorManager; } }

    [SerializeField]
    public DropDownToast DropDownToast; 

    [SerializeField] Tutorial tutorial;
    #endregion

    //A master switch for our ui, used for the purpose of the end of day manager rework.
    public GameObject UILayout;


    // these need to go somewhere else;
    public TapSystem theBarsTaps;

    [SerializeField]
    AudioCommand[] drinkSounds = new AudioCommand[4];
   
    public float pauseUntilDrinkIsServed;
    private float countdownUntilDrinkIsServed;
    private bool isDrinkServed = false;

    private sbyte seatIndexer; // which seat are we interacting with. 
    public sbyte SeatIndexer { get { return seatIndexer; } }

   
    #endregion


    #region IBarManagerStates
    IBarManagerState patronHighlighted;
    IBarManagerState barPaused;
    IBarManagerState patronConversing;
    IBarManagerState completePatronAction;
    IBarManagerState makingPatronADrink;
    IBarManagerState dismissingPatrons;
    IBarManagerState endOfDayBar;

   // IBarManagerState cancelAdventurePause; // This

    IBarManagerState barManagerState;
    public IBarManagerState BarManagerState { get { return barManagerState; } }
    #endregion


    [SerializeField]
    FillableMug bartendersMug;

    void Start()
    {

        rumorManager = new RumorManager();
        rumorManager.initRumorWarehouse();
        patronManager = new PatronManager();
        patronManager.init();
        conversationWarehouse = new ConversationWarehouse();
        conversationWarehouse.initConversationWarehouse(patronManager.NumberOfPatronsInGame);
        orderManager.init();
        orderManager.unlockNewDrinksBasedOnIngredients(Ingredient.ingredientColor.red);
        orderManager.unlockNewDrinksBasedOnIngredients(Ingredient.ingredientColor.yellow);
        orderManager.unlockNewDrinksBasedOnIngredients(Ingredient.ingredientColor.green);
        orderManager.unlockNewDrinksBasedOnIngredients(Ingredient.ingredientColor.blue);
        JumpToStarterSeat();
        tutorial.Init(this);
    }

    void Update()
    {    
        for (int i = 0; i < Seats.Length; i++)
        {
            if (Seats[i].ThisSeatsRespawnState == Seat.seatRespawnState.NEEDSRESPAWN)
            {
                replaceBarPeople(i);
            }
           else if (Seats[i].ThisSeatsRespawnState == Seat.seatRespawnState.NEEDSCLEAR)
            { 
                clearASeat(Seats[i]);
            }
        }

    }

    public void replaceBarPeople(int personToReplace)
    {
        if (patronManager.PatronsForTheDay.Count > 0)
        {
            Patron p = patronManager.drawAPatron();
            Seats[personToReplace].FillSeat(p);
            SoundManager.Instance.AddCommand("EnterBarSound");
        }
    }

    public void clearASeat(Seat seatToClear) 
    {
        seatToClear.ThisSeatsRespawnState = Seat.seatRespawnState.EMPTY;
    }

    public BarManager()
    {
        patronHighlighted = new PatronHighlighted(this);
        barPaused = new BarPaused(this);
        patronConversing = new PatronConversing(this);
        completePatronAction = new CompletePatronAction(this);
        makingPatronADrink = new MakePatronDrink(this);
        dismissingPatrons = new DismissPatron(this);
        endOfDayBar = new EndOfDayBar(this);

        //cancelAdventurePause = new NoAdventurePause(this); // This

        barManagerState = patronHighlighted;
    }

    // I don't really like this, I feel this guy needs to happen somewhere else.
    // Move the managers that unlock things? 
    public void UnlockContent(List<Unlocker> contentToUnlock)
    {
        Director.UnlockContent(contentToUnlock);
    }

    public void SendInfoToRumorBoard(Rumor rumor)
    {
        Director.SendRumorToBoard(rumor);
    }

    public void ClickPatron()
    {
        if (selectedSeat.patron != null)
        {
            if (SelectedSeat.FadingText.IsTextAnimating)
            {
                SelectedSeat.FadingText.AutoFillTextBox();
            }
            else
            barManagerState.ClickPatron();
        }
    }

    public void OpenBattleReport()
    {
        Director.BarManagerOpensBattleReport();
    }

    public void OpenRumorBoard()
    {
        Director.OpenRumorBoard();
    }

    public void OpenMapFromBar(Patron patronToSend)
    {
        byte patronsLeft = countHowManyPatronsAreLeft();
        Director.OpenMapFromBar(patronToSend, patronsLeft);
    }
    #region AdventureMapRelated


    public List<Patron> AquirepatronManagerInformationForEndOfDayManager() 
    {
        return patronManager.AllKnownPatrons;
    }

    public void dismissAdventurer()
    {
        selectedSeat.patron.currentActivity = Patron.whatDoTheyWantToDo.GOHOME;
        setBarState(dismissPatron());
    }

    //public void BackOutOfAdventureMap()
    //{
    //    // SeatState, repeat that you want to go on an adventure
    //    setBarState(noOneInteractedWith());
    //}


    public void SendAdventurerHome(Patron patronsToSendBack)
    {
       patronManager.putAPatronBack(patronsToSendBack);
       Director.ReportOnPatronReturning(patronsToSendBack);
    }

    public void checkIfAdventurersShouldSpawn(bool shouldAdventurersSpawn)
    {
        patronManager.CanWeSpawnAdventurers = shouldAdventurersSpawn;
    }

    #endregion

    #region swapCommands

    public void setBarState(IBarManagerState newBarManagerState)
    {
        barManagerState = newBarManagerState;
    }

    public IBarManagerState noOneInteractedWith()
    {
        areControlsActive(true);
        IsTimePaused(false);
        return patronHighlighted;
    }

    public IBarManagerState barIsPaused()
    {
        IsTimePaused(true);
        return barPaused;
    }

    public IBarManagerState patronIsConversing()
    {
        areControlsActive(false);
        IsTimePaused(true);
        return patronConversing;
    }

    public IBarManagerState patronPerformingAction()
    {
        areControlsActive(false);
        return completePatronAction;
    }

    public IBarManagerState prepareDrinkForPatron()
    {
        return makingPatronADrink;
    }

    public IBarManagerState dismissPatron()
    {
        areControlsActive(false);
        return dismissingPatrons;
    }

    public IBarManagerState barManagement()
    {
        return endOfDayBar;
    }

    //public IBarManagerState pauseAtTheEndOfCancelAdventure() // This
    //{
    //    Debug.Log("Switched to pause state");
    //    return cancelAdventurePause;
    //}


    #endregion

    #region TimeBased

    private void checkIfBarIsEmpty()
    {
        bool isBarEmpty = true;
        foreach (Seat s in Seats)
        {
            if (s.patron != null)
            {
                isBarEmpty = false;
                break;
            }
        }

        if (isBarEmpty)
        {
            EndPhase();
        }

    }

    private byte countHowManyPatronsAreLeft()
    {
        byte numberOfPatronsLeft = 0;
        for (int i = 0; i < Seats.Length; i++)
        {
            if (Seats[i].patron != null)
            {
                numberOfPatronsLeft++;
            }
        }
        return numberOfPatronsLeft;
    }

    public override void EndPhase()
    {
        Director.EndPhase(this);
    }

    #endregion


    #region cameraRelated

    private void moveCameraToNewLocation(Vector3 selectedSeatsLocation)
    {
        Camera.main.GetComponent<CameraManager>().PanToLocation(selectedSeatsLocation);
    }

    #endregion

    #region controlsRelated

    public void changeSelectedSeat()
    {
        selectedSeat = Seats[seatIndexer];
        Seats[SeatIndexer].hilightBarToken();
        moveCameraToNewLocation(selectedSeat.seatsCameraTarget.transform.position);
    }

    public void IsTimePaused(bool yesNo)
    {
        foreach (Seat s in Seats)
        {
            s.pauseThisSeat(yesNo);
        }
    }

    public void panBarLeft()   
    {
        if (Seats[0].barToken.GetComponent<Button>().enabled) //HACK
        {
            Seats[seatIndexer].unHilightBarToken();
            seatIndexer--;
            if (seatIndexer < 0)
            {
                seatIndexer = 0;
            }
            selectedSeat.cutOffPatronsSentence();
            changeSelectedSeat();
        }
    }

    public void panBarRight()
    {
        if (Seats[0].barToken.GetComponent<Button>().enabled)
        {
            Seats[seatIndexer].unHilightBarToken();
            seatIndexer++;
            if (seatIndexer > Seats.Length - 1)
            {
                seatIndexer = (sbyte)(Seats.Length - 1);
            }
            selectedSeat.cutOffPatronsSentence();
            changeSelectedSeat();
        }
    }

    public void jumpToSeatInBar(sbyte seatToJumpTo)
    {
        Seats[seatIndexer].unHilightBarToken();
        seatIndexer = seatToJumpTo;
        selectedSeat.cutOffPatronsSentence();
        changeSelectedSeat();
        SoundManager.Instance.AddCommand("Move");
    }

    public void JumpToStarterSeat()
    {
        for (int i = 0; i < Seats.Length; i++)
        {
            Seats[i].unHilightBarToken();
        }
        seatIndexer = 1;
        changeSelectedSeat();
    }

    private void areControlsActive(bool yesNo)
    {
        for (int i = 0; i < Seats.Length; i++)
        {
            Seats[i].barToken.GetComponent<Button>().enabled = yesNo;
            if (!yesNo)
            {
                Seats[i].setBarTokensToPauseGrey();
            }
            else
            {
                Seats[i].setBarTokensToUnPauseColor();
            }
        }
        SelectedSeat.setBarTokensToUnPauseColor();
    }
    #endregion

    #region DrinkRelated

    public void MakeDrink(byte itemSlotToUse)
    {
        barManagerState.MakeDrink(itemSlotToUse, bartendersMug);
        playRandomDrinkSound();
        theBarsTaps.unlockServeAndRecycleButtons();
        if (IsMugFull())
        {
            theBarsTaps.lockTapPulls();
        }
    }

    public bool IsMugFull()  // could also be a check within bartenders mug as both originate from there... when I am cleaning Hit this first. 
    {
        return (bartendersMug.DrinkInMug.countIngredentsInDrink() >= bartendersMug.MaxSizeOfMug);
    }

    public bool IsMugEmpty()
    {
        return (bartendersMug.DrinkInMug.countIngredentsInDrink() <= 0);
    }

    public void serveDrink()
    {
        sellDrink();
        CheckForDrinkBuff();
        SelectedSeat.ConsumeBeverage();
        bartendersMug.ClearIngredientInMug();
        theBarsTaps.lockTapSystem();
    }

    public void sellDrink()
    {
      OrderManager.orderAccuracy Accuracy = orderManager.determineDrinkPrice(selectedSeat.patron.OrderThePatronWants, bartendersMug.DrinkInMug);

        if (Accuracy == OrderManager.orderAccuracy.CORRECT) // Not really the right place for xp, move this to drink, if correct give ammount of xp from drink. 
        {
            selectedSeat.patron.BondGainedOnThisQuest = 2;
            DropDownToast.AddMessageToQueue("Correct drink: Bond +" + selectedSeat.patron.BondGainedOnThisQuest);
            selectedSeat.patron.convertGainedBondToTotalBond();
            if (selectedSeat.patron.HasLeveledUp)
            {
                DropDownToast.AddMessageToQueue("Bond level up!");
            }
        }

        tutorial.notifyObserver(Mediator.ActionIdentifiers.DRINK_SERVED);
    }

    void CheckForDrinkBuff()
    {
        Patron.SkillTypes buff = orderManager.GetDrinkBuffIfDrinkExistsAndIsUnlocked(bartendersMug.DrinkInMug);
        selectedSeat.GrantPatronBuff(buff);

        if(buff != Patron.SkillTypes.NONE)
        {
            string toastToShow = "Menu match! Buff gained: " + buff.ToString();
            DropDownToast.AddMessageToQueue(toastToShow);
        }
    }

    public void recycleDrink()
    {
        bartendersMug.ClearIngredientInMug();
        theBarsTaps.UnlockTapSystem();
    }

    public bool AreThereAnyActiveQuests()
    {
        return (Director.GetActiveQuestCount() > 0);
    }

    private void playRandomDrinkSound()
    {
        int rand = Random.Range(0, drinkSounds.Length - 1);
        SoundManager.Instance.AddCommand(drinkSounds[rand].CommandText);
    }

    #endregion

    #region uiRelated

     public void EnableAllUI()
    {
        UILayout.SetActive(true);
    }

    public void DisableAllUi()
    {
        UILayout.SetActive(false);
    }
    #endregion

    #region subjectInterface
    List<IObserver> observers = new List<IObserver>();

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

    public void registerSelfToMediator()
    {
        Mediator.Register(this);
    }

    public void unregisterSelfToMediator()
    {
        Mediator.Unregister(this);
    }

    #endregion
}
