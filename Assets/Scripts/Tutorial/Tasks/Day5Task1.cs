using UnityEngine;
using System.Collections;

public class Day5Task1 : TutorialTask
{
    Patron targetPatron;
    string patronToCall;

    public Day5Task1(Tutorial _tutorial) : base(_tutorial)
    {
        TutorialReactions.Add(Mediator.ActionIdentifiers.DAY_STARTED, OnDayStart);
    }

    void OnDayStart()
    {
        targetPatron = findReturningPatron();
        if (targetPatron.QuestToCompleete == null)
        {
            tutorial.forceUnlockPatronOfName("Gaius");
            patronToCall = "Gaius";
            tutorial.SetTimer(3f);
            TutorialReactions.Add(Mediator.ActionIdentifiers.COUNTDOWN_ENDED, MavisOrGaius);
        } 

        else
        {
            if (targetPatron.QuestToCompleete.QuestName == "Hold the Line!")
            {
                patronToCall = "Mavis";
            }
            else
            {
                patronToCall = "Gaius";
            }

            TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, MavisOrGaius);
        }

      
    }

    byte seatIndex;
    void MavisOrGaius()
    {
        //if you chose the quest that favored the Corporeal, have Mavis come in
        //if you chose the quest that favored the College, have Gaius come in
        TutorialReactions.Clear();
        seatIndex = (byte)tutorial.getCurrentTargetedSeatNumber();

        tutorial.forcePatronIntoBarToSitAt(patronToCall, seatIndex);

        tutorial.forceSeatToHaveSpecificJob(seatIndex, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, SendPatronHome);
    }

    void SendPatronHome()
    {
        TutorialReactions.Clear();
        tutorial.forceSeatToHaveSpecificJob(seatIndex, Patron.whatDoTheyWantToDo.GOHOME);
        tutorial.ResetBarState();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, JimTeases);
    }

    void JimTeases()
    {
        TutorialReactions.Clear();
        tutorial.invokeJimAtSeatNumber(1);
        tutorial.forceSeatToHaveSpecificConversation(1, "GDC Tutorial End");
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, EndDay);
    }

    void EndDay()
    {
        TutorialReactions.Clear();
        tutorial.forceEndOfDay();
        TutorialReactions.Add(Mediator.ActionIdentifiers.END_DAY_FADE_OUT, EndThatTutorial);

        ////do whatever
        //tutorial.forceSeatToHaveSpecificJob(seatIndex, Patron.whatDoTheyWantToDo.GOHOME);
        //tutorial.ResetBarState();
        //Debug.Log("END... THAT... TUTORIAL");
    }

    void EndThatTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }




    Patron findReturningPatron()
    {
        Patron patronToReturn;

        patronToReturn = tutorial.GetPatron("Artie");
        if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }

        patronToReturn = tutorial.GetPatron("Old Man Horace");
        if (patronToReturn.currentActivity == Patron.whatDoTheyWantToDo.TURNIN) { return patronToReturn; }

        patronToReturn = tutorial.GetPatron("Deirdre Downton");
        return patronToReturn;

    }
}
