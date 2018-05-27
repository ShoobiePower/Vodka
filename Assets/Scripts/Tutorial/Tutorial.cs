using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * There are two steps to making a fragment of a tutorial
 * 
 * 1) Do something (send Jim into the bar)
 * 2) Wait for the player to make a particular action
 *      - If the action is correct, continue with tutorial
 *      - If the action is incorrect, correct the player
 * 3) Continue with tutorial, Do something again
 * 
 * 
 * Waiting for an action to be made is easy. We use the Observer to notify the Tutorial whenever an action is made.
 *      If the action is deemed important, we do something. Otherwise, we ignore it (depending on what state the Tutorial is in?)
 *      
 * The tricky part is the "doing something"
 *      - One thing we could do is make the Tutorial do everything on its own (the floating head, talking to Jim, etc), but it'll have to know about a lot of things:
 *          * Each seat (placing Jim in the bar and controlling what patrons go where)
 *          * DioOut (ability to talk)
 *          * OrderManager (ability to make an order)
 *          * Some special UI indications
 *          * Whether the day has ended (action, this is easy)
 *          * Locking and unlocking things? Maybe, hopefully, not.
 *          
 *      - Reflecting upon the detailed tutorial, what we REALLY need to be able to do is:
 *          * Force conversations  X
 *          * Force orders (can this be done through a conversation?)  X
 *          * Force adventure (can this be done through a conversation?)
 *          * Force patrons to sit at particular seats
 *          * Force particular patrons to come into the bar
 *          * Unlock conversations, Unlock first rumor
 *          * Trigger floating heads when something is opened for the first time
 *          * Show / Hide UI elements
 *          
 * Some thoughts on how to go about this:
 *      - This'll be somewhat messy, but should the Tutorial class have references to, say, the BarManager? Or at least have access to a whole lot of stuff.         
 *          
*/

public class Tutorial : MonoBehaviour, IObserver
{
    BarManager barManager;  //NOTE: I'm not sure if I like this because, technically, I should only be able to access a LITTLE from the BarManager... not everything

    [SerializeField]
    TalkingHead talkingHead;

    [SerializeField]
    GameObject endOfTutorialPage;

    TutorialTask currentTask;

    public bool IsTutorialOver { get; private set; } 



    public void Start()
    {
        IsTutorialOver = false;
        regiesterSelfToMediator();
        SetCurrentTask(new Day1Task1(this));
    }

    public void Init(BarManager barManager)
    {
        this.barManager = barManager;
        
    }

    //call an event based on the provided enum

    public void notifyObserver(Mediator.ActionIdentifiers actionIdentifier)
    {
        currentTask.DetermineAction(actionIdentifier);
    }

    public void regiesterSelfToMediator()
    {
        Mediator.Register(this);
    }

    public void unregisterSelfFromMediator()
    {
        Mediator.Unregister(this);
    }

    public void unregisterBarmanager()
    {
        Mediator.Unregister(barManager);
    }

    public void SetCurrentTask(TutorialTask desiredTask)
    {
        currentTask = desiredTask;
    }

    float timerCountdown;
    bool timerActive;
    public void SetTimer(float time)
    {
        timerCountdown = time;
        timerActive = true;
    }

    void Update()
    {
        if (timerActive)
        {
            timerCountdown -= Time.deltaTime;
            if (timerCountdown <= 0)
            {
                timerActive = false;
                notifyObserver(Mediator.ActionIdentifiers.COUNTDOWN_ENDED);
            }
        }

    }

    #region ForceCommands
    //From the desk of NATHAN C DEV: Some tools I built, Game can currently go between tutorial and gameplay, 
    // If any tools need to be built, anything is unclear or if anything catches fire, please contact me via text as I may be buisy. 
    // I wish you luck. 

    //NOTES: Please use seat numbers from 0 ( the left most seat) to 2 ( the right most seat)

    public void forceUnlockPatronOfName(string patronTAG) // unlocks patron and adds them to regulars and known patrons, Please use the patron's Tag for operation.
    {                                                      // please do not unlock JIM, rather uses the invoke Jim below. 
        barManager.PatronManager.unlockNewPatronAndAdd(patronTAG);
    }

    public void forcePatronIntoBarToSitAt(string patronTAG, byte seatNumber) // forces patron into the bar at a specific seat number
    {
        Patron p = barManager.PatronManager.drawAPatronByName(patronTAG); // draw patron by name is special, it looks for a patron in it's regular 
        barManager.Seats[seatNumber].FillSeat(p);                     // if the patron cannot be found in regulars, it pulls it from the json.
        SoundManager.Instance.AddCommand("EnterBarSound");

    }

    public void forceSeatToHaveSpecificJob(byte seatNumber, Patron.whatDoTheyWantToDo forcedJob)
    {

        if (barManager.Seats[seatNumber].patron != null)
            barManager.Seats[seatNumber].patron.currentActivity = forcedJob;
    }

    public void forceSeatToHaveSpecificConversation(byte seatNumber, string ConversationTag) // forces the patron at the seatNumber to have a specific conversation from it's collection of conversations in the loader
    {                                                                                         // use the name of the tag, not the thing labled conversation name.
        if (barManager.Seats[seatNumber].patron != null)
        {
            forceSeatToHaveSpecificJob(seatNumber, Patron.whatDoTheyWantToDo.CONVERSE);
            barManager.Seats[seatNumber].patron.CurrentConversation = barManager.ConversationWarehouse.getSpecificConversationFromLoader(barManager.Seats[seatNumber].patron.ID, ConversationTag);
        }
    }

    public void forceSeatToHaveSpecificOrderByName(byte seatNumber, string nameOfDrink)  // forces the patron to want a specific order, can only really be combined with Conversation, 
    {                                                                                     // As conversation is the only task that leads into drinking, 
                                                                                          // please use the name of the drink. 
        barManager.Seats[seatNumber].patron.OrderThePatronWants = new OrderByName(barManager.OrderManager.getDrinkByName(nameOfDrink));
    }

    public void invokeJimAtSeatNumber(byte seatToInvokeJimAt)  // use this whenever you want to call on Jim.
    {
        Patron p = barManager.PatronManager.getPatronOfNameFromLoader("Jim"); // draw patron by name is special, it looks for a patron in it's regular 
        barManager.Seats[seatToInvokeJimAt].FillSeat(p);                     // if the patron cannot be found in regulars, it pulls it from the json.
        SoundManager.Instance.AddCommand("EnterBarSound");                                                     
    }
    // If you want to change Jim's diolouge, please go to Conversations and scroll all the way to the bottom
    public void endTutorial()                                  // If this new diologue is for the Rumor Jim hands out, make sure the rumor's name ("Name": "Jim FPO"   ln 113)
    {
                                                      
        IsTutorialOver = true;
        endOfTutorialPage.SetActive(true);

    }



    public void forceReactionAtSeat(JsonDialogueLoader.responceType type, byte seatNumber)
    {
        string nameOfPatron = barManager.Seats[seatNumber].patron.Name;
        forceReactionFromSpecificPatron(type, nameOfPatron);
    }

    public void forceSpecificReactionFromSpecificPatron(JsonDialogueLoader.responceType type, byte indexOfReaction, string patronName)
    {
        barManager.Seats[0].patronSays(patronName, JsonDialogueLoader.Instance.specificDioOutByIndex(type, indexOfReaction, patronName));
    }

    public void forceReactionFromSpecificPatron(JsonDialogueLoader.responceType typeOfResponce, string patronName)
    {
        barManager.Seats[0].patronSays(patronName, JsonDialogueLoader.Instance.dioOut(typeOfResponce, patronName));
    }

    public void pauseSeatAtIndex(byte seatToPause)
    {
        barManager.Seats[seatToPause].pauseThisSeat(true);
    }

    public void unPauseSeatAtIndex(byte seatToUnpause)
    {
        barManager.Seats[seatToUnpause].pauseThisSeat(false);
    }

    public void resetOrderAtSpecificSeat(byte seatToTarget)
    {
        barManager.Seats[seatToTarget].setSeatState(barManager.Seats[seatToTarget].SeatIsFilled());
    }

    public void forceEndOfDay()
    {
        barManager.EndPhase();
    }

    public sbyte getCurrentTargetedSeatNumber()
    {
        return barManager.SeatIndexer;
    }

    public void ResetBarState()
    {
        barManager.setBarState(barManager.noOneInteractedWith());
    }

    public void SwapToBarPaused()
    {
        barManager.setBarState(barManager.barIsPaused());
    }

    // Sends a conversation that belongs to a patron to the talking head, this also activates it. 
    // The talking head terminates upon closing the last stanza in the conversation. 
    public void sendMessageToTalkingHead(string patronTag, string ConversationTag)
    {
        Patron patronToDeliverTalkingHead = barManager.PatronManager.drawAPatronByName(patronTag);
        Conversation conversationToConvayByTalingHead = barManager.ConversationWarehouse.getSpecificConversationFromLoader(patronToDeliverTalkingHead.ID, ConversationTag);
        talkingHead.sendMessageToTalkingHead(conversationToConvayByTalingHead, patronToDeliverTalkingHead.ID);
    }

    public void shakeTheCamera()
    {
        Camera.main.GetComponent<CameraManager>().shakeTheCamera();
        SoundManager.Instance.AddCommand("Earthquake");
    }

    public void stopShakingCamera()
    {
        Camera.main.GetComponent<CameraManager>().stopShakingCamera();
    }

    public Patron GetPatron(string patronTag)
    {
        for (int i = 0; i < barManager.PatronManager.AllKnownPatrons.Count; i++)
        {
            if (barManager.PatronManager.AllKnownPatrons[i].Name == patronTag)
                return barManager.PatronManager.AllKnownPatrons[i];
        }

        return null;
    }
    #endregion
}