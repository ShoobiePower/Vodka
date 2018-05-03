using UnityEngine;
using System.Collections;

public class Day6Corporeal : TutorialTask
{
    public Day6Corporeal(Tutorial _tutorial) : base(_tutorial)
    {
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, BothPatronsLeftCheck);
    }

    int numPatronsLeft;
    void BothPatronsLeftCheck()
    {
        numPatronsLeft++;
        if (numPatronsLeft >= 2)
        {
            EnterGaius();
        }
    }

    void EnterGaius()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Gaius", 2);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, MakeSeat2Leave);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterDeidre);
    }

    void EnterDeidre()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Deidre Downton", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, MakeSeat1Leave);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterMavis);
    }

    void EnterMavis()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Mavis", 0);
        tutorial.forceSeatToHaveSpecificJob(0, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterArtie);
    }

    void EnterArtie()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Artie", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EndDay);
    }

    void EndDay()
    {
        TutorialReactions.Clear();
        tutorial.forceEndOfDay();
        //tutorial.SetCurrentTask(new Day7Corporeal(tutorial));
    }




    void MakeSeat1Leave()
    {
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
    }

    void MakeSeat2Leave()
    {
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.GOHOME);
    }
}
