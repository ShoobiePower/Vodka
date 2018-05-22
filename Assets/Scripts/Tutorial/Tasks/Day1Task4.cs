using UnityEngine;
using System.Collections;

public class Day1Task4 : TutorialTask
{
    public Day1Task4(Tutorial tutorial) : base(tutorial)
    {
        tutorial.forcePatronIntoBarToSitAt("Old Man Horace", 2);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, ServeCorrectDrink);
    }

    void ServeCorrectDrink()
    {
        TutorialReactions.Clear();
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.GOHOME);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, JimAgain);
    }

    void JimAgain()
    {
        tutorial.invokeJimAtSeatNumber(1);
        tutorial.forceSeatToHaveSpecificConversation(1, "EndDay1");
        tutorial.ResetBarState();
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, ByeJim);
    }

    void ByeJim()
    {
        tutorial.ResetBarState();
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EndDay);
    }

    void EndDay()
    {
        TutorialReactions.Clear();
        tutorial.forceEndOfDay();
        tutorial.SetCurrentTask(new Day2Task1(tutorial));
    }
}
