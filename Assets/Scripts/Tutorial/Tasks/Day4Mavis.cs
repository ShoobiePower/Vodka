using UnityEngine;
using System.Collections;

public class Day4Mavis1 : TutorialTask {

    byte seatIndex;

    public Day4Mavis1(Tutorial _tutorial) : base(_tutorial)
    {
    }

    void MavisOrGaius()
    {
        //if you chose the quest that favored the Corporeal, have Mavis come in
        //if you chose the quest that favored the College, have Gaius come in
        TutorialReactions.Clear();
        seatIndex = (byte)tutorial.getCurrentTargetedSeatNumber();

        tutorial.forcePatronIntoBarToSitAt("Mavis", seatIndex);

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
}
