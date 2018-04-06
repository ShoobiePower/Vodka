using UnityEngine;
using System.Collections;

public class Day3Task1 : TutorialTask
{
    public Day3Task1(Tutorial tutorial) : base(tutorial)
    {
        TutorialReactions.Add(Mediator.ActionIdentifiers.DAY_STARTED, OnDayBegin);
    }

    void OnDayBegin()
    {
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, JimExplains);
    }

    void JimExplains()
    {
        tutorial.invokeJimAtSeatNumber(1);
        tutorial.ResetBarState();
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, ByeAgainJim);
    }

    void ByeAgainJim()
    {
        //Have hoarce and artie come in both asking for a quest
        TutorialReactions.Clear();

        tutorial.forcePatronIntoBarToSitAt("Old Man Horace", 0);
        tutorial.forceSeatToHaveSpecificJob(0, Patron.whatDoTheyWantToDo.ADVENTURE);

        tutorial.forcePatronIntoBarToSitAt("Artie", 2);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.ADVENTURE);

        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, BothPatronsLeftCheck);
    }

    byte numPatronsLeft;
    void BothPatronsLeftCheck()
    {
        numPatronsLeft++;
        if(numPatronsLeft >= 2)
        {
            EndDay();
        }
        else
        {
            tutorial.hideDismissButton();
        }
    }

    void EndDay()
    {

        tutorial.forceEndOfDay();
        tutorial.SetCurrentTask(new Day4Task1(tutorial));

        //tutorial.forceEndOfDay();
        //tutorial.unregisterBarmanager();
        //tutorial.unregisterSelfFromMediator();
        //tutorial.endTutorial();
    }

    //void timerDone()
    //{
    //    TutorialReactions.Clear();

    //    tutorial.invokeJimAtSeatNumber(1);
    //    tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);
    //    //tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.CONVERSE);
    //    //tutorial.forceSeatToHaveSpecificConversation(1, "Introduction");
    //    TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, ArtieComesIn);
    //}

    //void ArtieComesIn()
    //{
    //    tutorial.forcePatronIntoBarToSitAt("Artie", 0);
    //    TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_SERVED, ServeDrink);
    //}

    //void ServeDrink()
    //{
    //    tutorial.forcePatronOutOfBarAtSeat(0);

    //    TutorialReactions.Clear();
    //    TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, HoraceComesInForAdventure);
    //}
    //void HoraceComesInForAdventure()
    //{
    //    TutorialReactions.Clear();
    //    TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, HoraceReadyForAdventure);

    //    tutorial.forcePatronIntoBarToSitAt("Old Man Horace", 1);
    //    tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.CONVERSE);
    //    tutorial.forceSeatToHaveSpecificConversation(1, "Horace Can Help");

    //    //tutorial.SetCurrentTask(new Day1Task3(tutorial));
    //}

    //void HoraceReadyForAdventure()
    //{
    //   // DELETE THIS SECTION AFTER PRESENTING IN CLASS
    //    TutorialReactions.Clear();
    //    tutorial.forcePatronOutOfBarAtSeat(1);
    //    tutorial.forceEndOfDay();
    //    tutorial.SetCurrentTask(new MapTask(tutorial)); // For proof of concept only
    //    tutorial.endTutorial();

    //    //UNCOMMENT AFTER WE PRESENT THE GAME IN CLASS TUESDAY
    //    //TutorialReactions.Clear();
    //    //tutorial.resetOrderAtSpecificSeat(1);
    //    //tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
    //}
}
