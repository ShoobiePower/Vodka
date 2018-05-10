using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day6CollegeRouteCorporeal : TutorialTask {

    public Day6CollegeRouteCorporeal(Tutorial _tutorial) : base(_tutorial)
    {
        EnterGaius();
    }

    private void EnterGaius()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Mavis", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, TellPatronToGo);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterMavis);
    }

    private void EnterMavis()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Mavis", 2);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterGaius);
    }

    private void EnterTheGang()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Nell", 0);
        tutorial.forceSeatToHaveSpecificJob(0, Patron.whatDoTheyWantToDo.ADVENTURE);

        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Old Man Horace", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);

        tutorial.forcePatronIntoBarToSitAt("Deidre Downton", 2);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, AllPatronsLeftCheck);
    }

    void TellPatronToGo()
    {
        tutorial.forceSeatToHaveSpecificJob((byte)tutorial.getCurrentTargetedSeatNumber(), Patron.whatDoTheyWantToDo.GOHOME);
    }

    int numPatronsLeft;
    void AllPatronsLeftCheck()
    {
        numPatronsLeft++;
        if (numPatronsLeft >= 3)
        {
            EndDay();
        }
    }

    void EndDay()
    {
        TutorialReactions.Clear();
        tutorial.forceEndOfDay();
        tutorial.SetCurrentTask(new Day7College(tutorial));
    }
}
