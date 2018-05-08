using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day7College : TutorialTask {

    Patron targetPatron;
    public Day7College(Tutorial _tutorial) : base(_tutorial)
    {
        TutorialReactions.Clear();
        OnDayStart();
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
            if (targetPatron.QuestToCompleete.QuestName == "Send Additional Troops" || targetPatron.QuestToCompleete.QuestName == "Lay a Trap")
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

    }

    void CollegeRoute()
    {

    }

    Patron findReturningPatron()
    {
        Patron patronToReturn;

        patronToReturn = tutorial.GetPatron("Deidre Downton");
        if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }

        patronToReturn = tutorial.GetPatron("Nell");
        if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }

        patronToReturn = tutorial.GetPatron("Artie");
        if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }
        return patronToReturn;


    }
}
