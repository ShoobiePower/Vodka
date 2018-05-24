using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day7College : TutorialTask {

    Quest targetQuest;

    public Day7College(Tutorial _tutorial) : base(_tutorial)
    {
        TutorialReactions.Clear();
        OnDayStart();
    }

    System.Action routeToGo;

    void OnDayStart()
    {
        TutorialReactions.Clear();
        targetQuest = FindQuest();
        if (targetQuest == null)
        {
            tutorial.SetTimer(3f);
            TutorialReactions.Add(Mediator.ActionIdentifiers.COUNTDOWN_ENDED, CorporealRoute);

        }

        else
        {
            if (targetQuest.QuestName == "Support the Corporeal" || targetQuest.QuestName == "Aid the Corporeal")
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
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, SendPatronHome);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterJim);
    }

    void CollegeRoute()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Mavis", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, SendPatronHome);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterJim);

    }

    private void EnterJim()
    {
        TutorialReactions.Clear();
        tutorial.invokeJimAtSeatNumber(1);
        if (routeToGo == CorporealRoute)
        {
            tutorial.forceSeatToHaveSpecificConversation(1, "Day7CollegeCorporeal");
        }
        else
        {
            tutorial.forceSeatToHaveSpecificConversation(1, "Day7CollegeCollege");
        }

        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, EndDay);
    }

    void EndDay()
    {
        TutorialReactions.Clear();
        tutorial.forceEndOfDay();
        TutorialReactions.Add(Mediator.ActionIdentifiers.END_DAY_FADE_OUT, ExitTutorial);
        
    }

    void SendPatronHome()
    {
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
    }


    Quest FindQuest() 
    { 
        Quest questToReturn;

        questToReturn = tutorial.GetPatron("Deidre Downton").QuestToCompleete;
        if (questToReturn != null) { return questToReturn; }

        questToReturn = tutorial.GetPatron("Nell").QuestToCompleete;
        if (questToReturn != null) { return questToReturn; }

        questToReturn = tutorial.GetPatron("Artie").QuestToCompleete;
        if (questToReturn != null) { return questToReturn; }

        questToReturn = tutorial.GetPatron("Old Man Horace").QuestToCompleete; // CHECK
        if (questToReturn != null) { return questToReturn; }

        Debug.Log("Fallthrough on quest to return");
        return null;


    }

    void ExitTutorial()
    {
        tutorial.endTutorial();
    }
}
