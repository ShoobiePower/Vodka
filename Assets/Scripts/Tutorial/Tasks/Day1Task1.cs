using UnityEngine;

public class Day1Task1 : TutorialTask
{
    public Day1Task1(Tutorial tutorial) : base(tutorial)
    {
        //set up all action and actionIdentifiers in here

        //Jim sits in the middle seat and gives his introduction
        tutorial.forceSpecificReactionFromSpecificPatron(JsonDialogueLoader.responceType.ABOUTTOLEAVE, 0, "Jim");
        TutorialReactions.Add(Mediator.ActionIdentifiers.COUNTDOWN_ENDED, timerDone);
        tutorial.SetTimer(2f);
    }

    void timerDone()
    {
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.COUNTDOWN_ENDED, WhatAreYouDoing);
        tutorial.SetTimer(10f);

        tutorial.invokeJimAtSeatNumber(1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        //tutorial.forceSeatToHaveSpecificConversation(1, "Introduction");
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, continueTalking);
    }

    void WhatAreYouDoing()
    {
        tutorial.forceSpecificReactionFromSpecificPatron(JsonDialogueLoader.responceType.ABOUTTOLEAVE, 2, "Jim");
    }

    //make whatever methods you like below
    void continueTalking()
    {
        //Jim orders a dragonbite
        TutorialReactions.Clear();

        tutorial.pauseSeatAtIndex(1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        tutorial.forceSeatToHaveSpecificConversation(1, "Now You Know Me");

        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, servedADragonbite);
    }

    void servedADragonbite()
    {
        TutorialReactions.Clear();

        tutorial.forceSpecificReactionFromSpecificPatron(JsonDialogueLoader.responceType.ABOUTTOLEAVE, 1, "Jim");
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);

        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, WaitForJimToLeave);
    }

    void WaitForJimToLeave()
    {
        TutorialReactions.Clear();
        tutorial.SetCurrentTask(new Day1Task2(tutorial));
    }
}
