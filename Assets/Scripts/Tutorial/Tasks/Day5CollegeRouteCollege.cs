using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5CollegeRouteCollege : TutorialTask {

    public Day5CollegeRouteCollege(Tutorial _tutorial) : base(_tutorial)
    {
        EnterMavis();
    }

    void EnterMavis()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Mavis", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, TellPatronToGo);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterHorace);
    }

    private void EnterHorace()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Old Man Horace", 0);
        tutorial.forceSeatToHaveSpecificJob(0, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, TellPatronToGo);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, BringBackMavis);
    }

    private void BringBackMavis()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Mavis", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterNellAndArtie);
    }

    private void EnterNellAndArtie()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Nell", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);

        tutorial.forcePatronIntoBarToSitAt("Artie", 2);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, BothPatronsLeftCheck);
    }

    void TellPatronToGo()
    {
        tutorial.forceSeatToHaveSpecificJob((byte)tutorial.getCurrentTargetedSeatNumber(), Patron.whatDoTheyWantToDo.GOHOME);
    }

    int numPatronsLeft;
    void BothPatronsLeftCheck()
    {
        numPatronsLeft++;
        if (numPatronsLeft >= 2)
        {
            EndDay();
        }
    }

    void EndDay()
    {
        TutorialReactions.Clear();
        tutorial.forceEndOfDay();
        tutorial.SetCurrentTask(new Day6CollegeTransition(tutorial));
    }
}
