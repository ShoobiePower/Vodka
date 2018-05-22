using UnityEngine;
using System.Collections;

public class Day2Task2 : TutorialTask
{
    public Day2Task2(Tutorial tutorial) : base(tutorial)
    {
        //For Nathan C: So, here's the issue: Jim needs to give his "Day 2 Intro"
        tutorial.forcePatronIntoBarToSitAt("Deidre Downton", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.MAP_OPEN, OpenMap);
    }

    void OpenMap()
    {
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, JimExplainsQuests);
    }

    void JimExplainsQuests()
    {
        tutorial.invokeJimAtSeatNumber(1);
        tutorial.forceSeatToHaveSpecificConversation(1, "After Sending Deidre");
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, ByeAgainJim);
    }

    void ByeAgainJim()
    {
        tutorial.ResetBarState();
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, HoraceEnter);
    }

    void HoraceEnter()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Old Man Horace", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, HoraceExit);
    }

    void HoraceExit()
    {
        TutorialReactions.Clear();
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, ArtieEnter);
    }

    void ArtieEnter()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Artie", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, ArtieExit);
    }

    void ArtieExit()
    {
        TutorialReactions.Clear();
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EndThatDay);
    }

    void EndThatDay()
    {
        tutorial.forceEndOfDay();
        tutorial.SetCurrentTask(new Day3Task1(tutorial));
    }
}
