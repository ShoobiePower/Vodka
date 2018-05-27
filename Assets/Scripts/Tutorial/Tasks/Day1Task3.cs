using UnityEngine;
using System.Collections;

public class Day1Task3 : TutorialTask
{
    public Day1Task3(Tutorial tutorial) : base (tutorial)
    {
        TutorialReactions.Clear();
        tutorial.invokeJimAtSeatNumber(1);
        tutorial.forceSeatToHaveSpecificConversation(1, "Jim Explains Rumors");
        tutorial.ResetBarState();
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, LeaveJimLeave);
    }

    void LeaveJimLeave()
    {
        tutorial.ResetBarState();
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, ArtieComesInForQuest);
    }

    void ArtieComesInForQuest()
    {
        tutorial.forcePatronIntoBarToSitAt("Artie", 2);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.MAP_OPEN, OpenMap);

    }

    void OpenMap()
    {
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, GoHome);
    }

    void GoHome()
    {
        TutorialReactions.Clear();
        tutorial.SetCurrentTask(new Day1Task4(tutorial));
    }
}
