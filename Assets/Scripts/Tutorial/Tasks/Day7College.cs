using System;
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
            if (targetPatron.QuestToCompleete.QuestName == "Support the Corporeal" || targetPatron.QuestToCompleete.QuestName == "Aid the Corporeal")
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
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Gaius", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterJim);
    }

    void CollegeRoute()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Mavis", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterJim);
    }

    private void EnterJim()
    {
        TutorialReactions.Clear();
        tutorial.invokeJimAtSeatNumber(1);
        if (routeToGo == CorporealRoute)
        {
            tutorial.forceSeatToHaveSpecificConversation(1, "Day7TeaserCorporeal");
        }
        else
        {
            tutorial.forceSeatToHaveSpecificConversation(1, "Day7TeaserCollege");
        }

        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, EndDay);
    }

    void EndDay()
    {
        TutorialReactions.Clear();
        tutorial.forceEndOfDay();
        tutorial.endTutorial();
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
