using UnityEngine;
using System.Collections;

public class Day5CorporealRouteCollege : TutorialTask
{
    public Day5CorporealRouteCollege(Tutorial _tutorial) : base(_tutorial)
    {
        EnterMavis();
    }

    void EnterMavis()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Mavis", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterArtie);
    }

    void EnterArtie()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Artie", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterHoraceAndNell);
    }

    void EnterHoraceAndNell()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Nell", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);

        tutorial.forcePatronIntoBarToSitAt("Old Man Horace", 2);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.ADVENTURE);

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
