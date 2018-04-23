
using UnityEngine;
using System.Collections;

public class Day5CorporealTransition : TutorialTask
{
    Patron targetPatron;

    public Day5CorporealTransition(Tutorial _tutorial) : base(_tutorial)
    {
        TutorialReactions.Add(Mediator.ActionIdentifiers.DAY_STARTED, OnDayStart);
    }

    System.Action routeToGo;

    void OnDayStart()
    {
        TutorialReactions.Clear();
        targetPatron = findReturningPatron();
        if (targetPatron.QuestToCompleete == null)
        {
            tutorial.SetTimer(3f);
            TutorialReactions.Add(Mediator.ActionIdentifiers.COUNTDOWN_ENDED, CorporealRoute);

        }

        else
        {
            //if you chose the quest that favored the Corporeal, have Mavis come in
            //if you chose the quest that favored the College, have Gaius come 
            if (targetPatron.QuestToCompleete.QuestName == "Return it")
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
        tutorial.SetCurrentTask(new Day5CorporealRouteCorp(tutorial));
    }

    void CollegeRoute()
    {
        tutorial.SetCurrentTask(new Day5CorporealRouteCollege(tutorial));
    }


    Patron findReturningPatron()
    {
        //Patron patronToReturn;

        //patronToReturn = tutorial.GetPatron("Artie");
        //if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }

        //patronToReturn = tutorial.GetPatron("Old Man Horace");
        //if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }

        //patronToReturn = tutorial.GetPatron("Deirdre Downton");
        //return patronToReturn;

        return tutorial.GetPatron("Deirdre Downton");

    }
}
