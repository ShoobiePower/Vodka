using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5CollegeRouteCorp : TutorialTask {

    public Day5CollegeRouteCorp(Tutorial _tutorial) : base(_tutorial)
    {
        EnterGaius();
    }

    void EnterGaius()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Gaius", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterDeidreAndArtie);
    }

    private void EnterDeidreAndArtie()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Deidre Downton", 0);
        tutorial.forceSeatToHaveSpecificJob(0, Patron.whatDoTheyWantToDo.ADVENTURE);

        tutorial.forcePatronIntoBarToSitAt("Artie", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, BothPatronsLeftCheck);
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
        //tutorial.SetCurrentTask(new Day6Task1(tutorial));
    }
}
