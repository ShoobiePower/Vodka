using UnityEngine;
using System.Collections;

public class Day4Task1 : TutorialTask
{
    public Day4Task1(Tutorial _tutorial) : base(_tutorial)
    {
        //TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterNell);

    }

    void EnterNell()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Nell", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EveryoneAsksForQuests);
    }

    void EveryoneAsksForQuests()
    {
        TutorialReactions.Clear();

        tutorial.forcePatronIntoBarToSitAt("Deirdre Downton", 0);
        tutorial.forcePatronIntoBarToSitAt("Old Man Horace", 1);
        tutorial.forcePatronIntoBarToSitAt("Artie", 2);

        tutorial.forceSeatToHaveSpecificJob(0, Patron.whatDoTheyWantToDo.ADVENTURE);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.ADVENTURE);

        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, countPatronsLeft);
    }

    int patronCount;
    void countPatronsLeft()
    {
        patronCount++;

        if(patronCount >= 3)
        {
            EndDay();
        }
    }

    void EndDay()
    {
        tutorial.forceEndOfDay();
        tutorial.SetCurrentTask(new Day5Task1(tutorial));
    }

}
