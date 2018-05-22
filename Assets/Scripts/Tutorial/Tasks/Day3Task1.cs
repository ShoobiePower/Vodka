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
    }

    void EndDay()
    {
        tutorial.forceEndOfDay();
        tutorial.SetCurrentTask(new Day4Task1(tutorial));
    }
}
