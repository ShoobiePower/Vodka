using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using System.Collections.Generic;

public class Seat : ABSTFadableObject, ISubject
{
    #region fields
    public Patron patron;
    public Mug patronsMug;
    public IconPack patronWantsIcons;
    public Transform seatsCameraTarget;

    [SerializeField]
    Transform toastStartPoint;
    public Transform ToastStartPoint {get { return toastStartPoint; }}

    [SerializeField]
     FadingText fadingText;
    public FadingText FadingText { get { return fadingText; } }
    

    [SerializeField]
    float timeToFadeInOut;

    [SerializeField]
    Image barTokenBackground;

    public Image barToken;

    private SpriteRenderer patronArt;
    private float waitTimer;
    private bool textTimerHasBeenCutOff;
    #endregion

    #region properties

    public enum seatRespawnState {SEATED,NEEDSRESPAWN,NEEDSCLEAR,EMPTY}; // a step toward cleaning up, the goal is to merge this with seat state, but baby steps!
    private seatRespawnState thisSeatsRespawnState;
    public seatRespawnState ThisSeatsRespawnState { get { return thisSeatsRespawnState; } set { thisSeatsRespawnState = value; } }

    float drinkTimer;
    public float DrinkTimer { set { drinkTimer = value; } }

    bool canDrink;
    public bool CanDrink { get { return canDrink; } set { canDrink = value; } }

    bool isTimerPaused;
    public bool IsTimerPaused { set { isTimerPaused = value; } }

  
    #endregion

    #region ISeatStates
    ISeatStates noOneSeated;
    ISeatStates patronSeated;
    ISeatStates patronOrdered;
    ISeatStates patronIsDrinking;
    ISeatStates patronWantsAdventure;
    ISeatStates patronReturningFromAdventure;

    ISeatStates seatState;

    #endregion


    private void Awake() // start
    {
        registerSelfToMediator();
        noOneSeated = new NoOneSeated(this);
        patronSeated= new PatronSeated(this);
        patronOrdered = new PatronOrdered(this);
        patronIsDrinking = new PatronIsDrinking(this);
        patronWantsAdventure = new PatronWantsAdventure(this);
        patronReturningFromAdventure = new PatronReturningFromAdventure(this);
   
       
        isTimerPaused = false;
        this.GetComponent<BoxCollider2D>().enabled = false;

        patronWantsIcons.initIconsToDisplay();
        patronWantsIcons.initFadeTime(timeToFadeInOut);

        patronsMug.initMug();

        patronArt = this.GetComponent<SpriteRenderer>(); 
        assignObjectToFade(patronArt);
        setFadeTime(timeToFadeInOut);
        seatState = noOneSeated;
    }

    private void Update()
    {
        if (currentAnimationState == animationStates.ENTER)
        {
            runEnterAnimation();
        }

        if (currentAnimationState == animationStates.EXIT)
        {
            runExitAnimation();
        }

        if (currentAnimationState == animationStates.EXITEND)
        {
            startSeatCoolDown();
        }

        if (seatState == patronIsDrinking)
        {
            drinkCountdown();
        }
        else
            respawnCountdown();
    }

    public void setSeatState(ISeatStates newSeatState)
    {
        seatState = newSeatState;
    }

    public void FillSeat(Patron patronToSit)
    {
        canDrink = true;
        seatState.FillSeat(patronToSit);
        startEnterAnimation();
        patronWantsIcons.clearNeedsIcons();
        patronWantsIcons.startEnterAnimation();

        switch (patron.currentActivity)
          {
            case Patron.whatDoTheyWantToDo.RUMOR:
                {
                    patronWantsIcons.patronWantsToTellYouSomething(); //shows an icon for I have a rumor, the rumory bits are actually handled in bar manager because it knows what a quest manager is.
                    break;
                }

            case Patron.whatDoTheyWantToDo.TURNIN:
                {
                    seatState.PatronReturnsFromQuest();
                    patronWantsIcons.patronIsReturningFromQuest();
                    break;
                }

            case Patron.whatDoTheyWantToDo.ADVENTURE:
                {
                    patronWantsIcons.PatronWantsToGoOnAnAdventure(); // displays icon for I want to adventure. 
                    break;
                }
          }
    }

    public void ConsumeBeverage()
    {
        seatState.ConsumeBeverage();

    }

    public void GrantPatronBuff(Patron.SkillTypes buff)
    {
        patron.SkillGrantedByDrink = buff;
    }

     public void PatronSharesARumor()
    {
        seatState.PatronSharesARumor();
    }


    #region SwapFunctions

    public ISeatStates ClearSeat()
    {
        Debug.Log( patron.Name + "Seat Should be clear");
        barToken.sprite = ApperanceManager.instance.GetEmptySeatToken();
        patron = null;
        thisSeatsRespawnState = seatRespawnState.NEEDSCLEAR;
        return noOneSeated;
    }

    public ISeatStates SeatIsFilled()
    {
        thisSeatsRespawnState = seatRespawnState.SEATED;
        return patronSeated;
    }

    public ISeatStates orderHasBeenTaken()
    {
   
        return patronOrdered;
    }

    public ISeatStates patronIsdrinking()
    {
        
        return patronIsDrinking;
    }

    public ISeatStates patronWouldLikeToGoOnAdventure()
    {
        return patronWantsAdventure;
    }

    public ISeatStates patronIsReturningFromAdventure()
    {
        return patronReturningFromAdventure;
    }

    #endregion

    #region TimerRelated
    public void setSeatTimer(float timeToSet)
    {
        waitTimer = timeToSet;
    }

    public void drinkCountdown() // This might be antiquated by what we are doing now. 
    {
        if (!isTimerPaused)
        {
            drinkTimer -= Time.deltaTime;
            if (drinkTimer <= 0)
            {
               // patronPreparesToLeave();
            }
        }
    }

    public void respawnCountdown()
    {
        if (!isTimerPaused)  
       {
           if (waitTimer > 0)
          {
               waitTimer -= Time.deltaTime;
           }
           else if (seatState == noOneSeated)
          {
            thisSeatsRespawnState = seatRespawnState.NEEDSRESPAWN;
          }
        }
    }

    public void startSeatCoolDown()
    {
        setSeatTimer(5);
        setSeatState(ClearSeat());
        currentAnimationState = animationStates.NONE;
        notifyObserver(Mediator.ActionIdentifiers.PATRON_LEFT);
    }

    public void pauseThisSeat(bool yesNo)
    {
        this.isTimerPaused = yesNo;
        this.pauseAnimation(yesNo);
        this.patronsMug.pauseAnimation(yesNo);
        this.patronWantsIcons.pauseAnimation(yesNo);
    }
    #endregion

    #region Speach Related

    public void TalkWithPatron()
    {
        activateConversationMarkerOnStartConversation();
        seatState.TalkWithPatron();
        textTimerHasBeenCutOff = false;
        checkIfConversationContinues();
    }

    public void patronSays(string thingToSay)
    {
        patronSays(patron.Name, thingToSay);
    }

    public void patronSays(string patronName, string thingToSay)
    {
       fadingText.sendWhatToSay(patronName + " : ", thingToSay);
    }

   private void activateConversationMarkerOnStartConversation()
    {
        fadingText.ConversationMarkerOn();
    }

    private void checkIfConversationContinues()
    {
        if (patron.CurrentConversation.IsConversationOver)
        {
            fadingText.SignalEndOfConversation();
        }

    }

    public void cutOffPatronsSentence()
    {
        fadingText.cutOff(0);
        textTimerHasBeenCutOff = true;
    }


    #endregion

    public void patronPreparesToLeave()
    {
        startExitAnimation();
        this.GetComponent<BoxCollider2D>().enabled = false;
        patronWantsIcons.startExitAnimation();
        patronsMug.fadeMug(timeToFadeInOut);
        if (!textTimerHasBeenCutOff)
            fadingText.cutOff(timeToFadeInOut);
    }

    public void hilightBarToken()
    {
        barTokenBackground.sprite = ApperanceManager.instance.getHilightedBarSeatToken();
    }

    public void unHilightBarToken()
    {
        barTokenBackground.sprite = ApperanceManager.instance.getUnHilightedBarSeatToken();
    }

    #region Observer Pattern

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

