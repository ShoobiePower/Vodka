using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5CollegeTransition : TutorialTask
{
    Patron targetPatron;
    public Day5CollegeTransition(Tutorial _tutorial) : base(_tutorial)
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
            if (targetPatron.QuestToCompleete.QuestName == "Disclose the College's Request")
            {
                routeToGo = CorporealRoute;
            }
            else
            {
                routeToGo = CollegeRoute;
            }
        }


        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, routeToGo);


    }

    void CorporealRoute()
    {
        tutorial.SetCurrentTask(new Day5CollegeRouteCorp(tutorial));
    }

    void CollegeRoute()
    {
        tutorial.SetCurrentTask(new Day5CollegeRouteCollege(tutorial));
    }

    Patron findReturningPatron()
    {
        Patron patronToReturn;

        patronToReturn = tutorial.GetPatron("Deidre Downton");
        if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }

        patronToReturn = tutorial.GetPatron("Nell");
        if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }
        return patronToReturn;


    }
}
