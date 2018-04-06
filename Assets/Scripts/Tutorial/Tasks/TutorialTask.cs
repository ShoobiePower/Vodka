using System;
using System.Collections.Generic;

public class TutorialTask
{
    protected Tutorial tutorial;
    protected Dictionary<Mediator.ActionIdentifiers, Action> TutorialReactions;

    public TutorialTask(Tutorial _tutorial)
    {
        tutorial = _tutorial;
        TutorialReactions = new Dictionary<Mediator.ActionIdentifiers, Action>();
    }

    public void DetermineAction(Mediator.ActionIdentifiers actionIdentifier)
    {
        //run a check to see if we're waiting for any of these actions / commands
        Action desiredEvent;
        if (TutorialReactions.TryGetValue(actionIdentifier, out desiredEvent))
        {
            //if we found the action we're waiting for, trigger the methods in the delegate
            desiredEvent();
        }
    }
}
