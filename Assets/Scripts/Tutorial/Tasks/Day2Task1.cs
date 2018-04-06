using UnityEngine;
using System.Collections;

public class Day2Task1 : TutorialTask
{
    //byte correctDrinksServed = 0;

    public Day2Task1(Tutorial tutorial) : base(tutorial)
    {
        TutorialReactions.Add(Mediator.ActionIdentifiers.DAY_STARTED, AfterQuestingPatronComesBack);
    }

    void AfterQuestingPatronComesBack()
    {
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, OnDayBegin);
    }

    void OnDayBegin()
    {
        tutorial.invokeJimAtSeatNumber(1);                                          //Jim sits down at seat 1
        tutorial.forceSeatToHaveSpecificConversation(1, "Day 2 Intro");             //Jim gives a conversation
        tutorial.ResetBarState();

        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, TriggerEarthquake);     // OnEarthquakeEnd  when the conversation ends, trigger the earthquake (CHANGE THIS TO "TriggerEarthquake")

    }

    void TriggerEarthquake()
    {
        tutorial.SwapToBarPaused();
        tutorial.shakeTheCamera();
        TutorialReactions.Add(Mediator.ActionIdentifiers.COUNTDOWN_ENDED, OnEarthquakeEnd);
        tutorial.SetTimer(2f);
        //stop talking and cause an EARTHQUAKE!!!

        //trigger "OnEarthquakeEnd" after a timer ends (OR after the earthquake ends... either one works)
    }

    void OnEarthquakeEnd() //Honestly, this method shouldn't be called. Instead, "EarthquakeRumor" should be called
    {
        TutorialReactions.Clear();
        tutorial.stopShakingCamera();
        tutorial.ResetBarState();
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);    //When the earthquake finishes, force Jim to give out your first rumor "The Commotion"
        //tutorial.resetOrderAtSpecificSeat(1);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, ByeJim); //This should be after you accept a rumor
    }

    void AcceptedRumor() //THIS HAS BEEN SKIPPED FOR NOW
    {
        TutorialReactions.Clear();
        //NOTE: I'm using the dialogue for this instead
        //tutorial.forceSpecificReactionFromSpecificPatron(JsonDialogueLoader.responceType.ABOUTTOLEAVE, 3, "Jim");   //Have Jim say, "Why don’t you hand that quest out to the next patron that steps into the bar? I’m sure they’d be just as interested in investigating all that ruckus as we are."
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, ByeJim);
    }

    void ByeJim()
    {
        tutorial.SetCurrentTask(new Day2Task2(tutorial));
    }
}