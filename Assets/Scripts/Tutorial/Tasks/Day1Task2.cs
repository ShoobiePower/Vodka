using UnityEngine;
using System.Collections;

public class Day1Task2 : TutorialTask
{
    public Day1Task2(Tutorial tutorial) : base(tutorial)
    {
        tutorial.forcePatronIntoBarToSitAt("Deirdre Downton", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, GoHome);
    }

    void GoHome()
    {
        TutorialReactions.Clear();
        tutorial.SetCurrentTask(new Day1Task3(tutorial));
    }
}
