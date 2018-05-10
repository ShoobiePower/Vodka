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
 * Here are the detailed steps to the tutorial:
 * 
 * #####  DAY 1  #####
 * 1) Calandar thing (stretch goal, don't worry about for now)
 * 2) Play conversation ("Well now, I didn’t expect you to pick up the reins so quickly!") to the dialogue
 * 3) Place Jim in the center window
 * 4) Play Jim's conversation
 * 5) Force Jim to order "Dragonbite" (have the drink making station appear as normal)
 * 6) An arrow points to recipe book / drink chart / drink guide thing, text appears above it saying "Click on the (RECIPE BOOK?!) to learn the ingredients"
 *      * Wait for player to click on book
 * 7) Page opens to Dragonbite (this is by default, since it's the first one in the book)
 * 8) Arrow appears pointing to ingredients with text reading: "These are the ingredients you’ll need. Each ingredient corresponds to a color on the taps."
 *          (NOTE: This step can possibly be removed with good UI, also these text bubbles can also be "floating heads," functionality-wise)
 * 9) Andrew has: a NEW (hooray) arrow points to the tap, with text above reading “Click on the tap twice to add two red ingredients to the drink.”
 *      For simplicity sake, I'm thinking to remove #9 and do this:
 *      * Wait for player to add ingredients
 *              -> if red, add +1 to "red counter"
 *              -> if not red (or red is greater than 2), Force a Jim dialogue: something about "the chart has two red ingredients. If you need to reset your drink, just throw it out" (indicate the sink / garbage / recycle button)
 * 10) After adding two red ingredients ("red counter" = 2): Force Jim dialogue: "That looks dandy! Why don’t you pass that on over so I can wet my lips."
 * 11) Highlight "serve drink" button, lever, spring, whatever it is
 *      * Wait for "Serve Drink" action
 * 12) Play Jim Conversation:   Jim: *sluuurp*
 *                              Jim: Not bad for your first drink!
 *                              Jim: (One more line for transition)
 * 13) Horace enters the bar, sits in the middle, and orders a Solarburst
 *      * Wait for Correct or Incorrect / Mixup actions
 *          -> if correct: serve drink and continue
 *          -> if incorrect: Force Horace to give a line: "Hmm… not quite what I ordered, but I couldn’t possibly be upset at such a fresh young face! Why don’t you give it another go?"
 * 14) Deidre enters the bar, sits in the middle, and orders a Goldfire
 *      * Wait for Correct or Incorrect / Mixup actions
 *          -> if correct: serve drink and continue
 *          -> if incorrect: Force Horace to give a line: "Do you think it's funny to play jokes on an woman like me? ... Hehehe! I do too. How about you try that order again, though?"
 *      * Wait for Correct or Incorrect / Mixup actions
 * 15) Artie enters the bar, sits on right right, and orders a Flare
 *          -> if correct: serve drink and continue
 *          -> if incorrect: Force Horace to give a line: "Hmm… not quite what I ordered, but I couldn’t possibly be upset at such a fresh young face! Why don’t you give it another go?"
 * 16) Day should have ended! After the last patron leaves, Play Jim conversation: "Looks like you had a fairly successful first day! I’m not sure if I’ll have time to stop in tomorrow, but I’m sure I’ll see you soon. Just keep doing what you’re doing!"
 * 17) Andrew wrote in to skip the "End of Day" screen, which I don't think is a bad idea. However, if it takes a bit of extra time to do this, we can skip it for now.
 *
 * #####  DAY 2  #####
 * 1) Jim enters bar, sits in middle seat, begin converstion
 * 2) Serve drinks like normal (these drinks can be failed). No rumors or anything yet.
 *      NOTE: This is the first time you'll be serving patrons random orders like usual and listening to conversations / introduction comments.
 * 3) Day ends: Jim has gives another conversation
 * 4) End of day Manager appears like normal (I don't know how much we need to explain this. It's mostly a "look around and figure it out yourself." So long as players know to click "Begin next day" (which they always do).
 *      NOTE: The only thing unlocked at this point is: "Patrons"
 *      
 * #####  DAY 3  #####
 * 1) Play another Jim conversation
 * 2) Jim gives you your first rumor
 * 3) Some time this day (doesn't have to be immediate), Horace will be given a conversation (and the conversation could prompt him to ask for an adventure)
 * 4) The first time the map is opened / first time you give a quest, Jim's floating head appears. Click through this converastion.
 *      * Wait for player to select the quest (just before hitting "send on quest")
 * 5) After all patrons are served like normal, day ends.
 * 6) The End of Day Manager now has the "map" on it
 * 
 * #####  DAY 4  #####
 * 1) Day begins like normal
 * 2) Horace returns like normal, complete quest
 * 3) The first time a player opens a quest summary page, Jim's head will appear yet again, explaining leveling and such
 * 4) The only other floating head here is recieving your first quest choice. BUT! WE might be able to avoid it by writing a better quest summary or dialogue (about how your choices will impact the future).
 *
 * #####  DAY 5  #####  NOTHING TO SEE (DO) HERE!
 * I BUG TO DIFFER AND THOSE BUG IS ANTZ!!! 
 */

public class Tutorial : MonoBehaviour, IObserver
{
    BarManager barManager;  //NOTE: I'm not sure if I like this because, technically, I should only be able to access a LITTLE from the BarManager... not everything.

    [SerializeField]
    GameObject DismissButton;

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

    //    - You should never child UI objects to one another unless they all move together(so, when you move the drink making station
    //    the drink chart background moves. Boo Or, when you stretch the entire drink dispencer, the nozzle stretches with the
    //    box. Yay!)

    //- Is it possible to have Jim(or another partron) say something after being served a drink?
    //- Is it possible to force lines of dialogue without a patron present?
    //- NOTE TO SELF: Replace Jim's 

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
        SoundManager.Instance.AddCommand("EnterBarSound");                                                     // If you want to change Jim's Drink order text, please go to Diolouge and scroll all the way to the bottom,
        //forcePatronIntoBarToSitAt("Jim", seatToInvokeJimAt);    // find Jim and switch out the text in Drink Request Line
    }
    // If you want to change Jim's diolouge, please go to Conversations and scroll all the way to the bottom
    public void endTutorial()                                  // If this new diologue is for the Rumor Jim hands out, make sure the rumor's name ("Name": "Jim FPO"   ln 113)
    {
                                                       // and the Diolouge's tag (   "Jim FPO": {  ln 454) 
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

    public void hideDismissButton()
    {
        DismissButton.SetActive(false);
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

    public void closeGame()
    {
        Application.Quit();
    }
    #endregion
}