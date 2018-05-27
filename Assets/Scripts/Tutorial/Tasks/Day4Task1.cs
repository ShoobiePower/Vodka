using UnityEngine;
using System.Collections;

public class Day4Task1 : TutorialTask
{
    Patron targetPatron;

    public Day4Task1(Tutorial _tutorial) : base(_tutorial)
    {
        TutorialReactions.Add(Mediator.ActionIdentifiers.DAY_STARTED, OnDayStart);
    }

    System.Action routeToGo;

    void OnDayStart()
    {
        targetPatron = findReturningPatron();
        if (targetPatron.QuestToCompleete == null)
        {
            tutorial.forceUnlockPatronOfName("Gaius");
            tutorial.SetTimer(3f);
            TutorialReactions.Add(Mediator.ActionIdentifiers.COUNTDOWN_ENDED, CorporealRoute);
            
        }

        else
        {
            //if you chose the quest that favored the Corporeal, have Mavis come in
            //if you chose the quest that favored the College, have Gaius come 
            if (targetPatron.QuestToCompleete.QuestName == "Hold the Line!")
            {
                //patronToCall = "Mavis";
                routeToGo = CorporealRoute;
            }
            else
            {
                //patronToCall = "Gaius";
                routeToGo = CollegeRoute;
            }

            TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, routeToGo);
        }


    }

    void CorporealRoute()
    {
        tutorial.SetCurrentTask(new Day4Gaius1(tutorial));
    }

    void CollegeRoute()
    {
        tutorial.SetCurrentTask(new Day4Mavis1(tutorial));
    }


    Patron findReturningPatron()
    {
        Patron patronToReturn;

        patronToReturn = tutorial.GetPatron("Artie");
        if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }

        patronToReturn = tutorial.GetPatron("Old Man Horace");
        if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }

        return patronToReturn;

    }
}
