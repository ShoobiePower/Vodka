using UnityEngine;
using System.Collections;

public class Day4Gaius1 : TutorialTask
{
    public Day4Gaius1(Tutorial _tutorial) : base(_tutorial)
    {
        EnterNell();
    }


    void EnterNell()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Nell", 1);

        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, EnterGaius);
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, ByeNell);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, AllowHoraceAndArtieToEnter); //(Gaius leaves)
    }

    void EnterGaius()
    {
        tutorial.forcePatronIntoBarToSitAt("Gaius", 0);
        tutorial.forceSeatToHaveSpecificConversation(0, "PositiveIntroduction");
    }

    void ByeNell()
    {
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
        //tutorial.ResetBarState();
    }

    //When Gaius leaves, this will be called
    void AllowHoraceAndArtieToEnter()
    {
        TutorialReactions.Clear();
        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, KickGaiusOut);
    }

    void KickGaiusOut()
    {
        TutorialReactions.Clear();
        tutorial.forceSeatToHaveSpecificJob(0, Patron.whatDoTheyWantToDo.GOHOME);
        //tutorial.ResetBarState();
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterHoraceAndArtie);
    }

    void EnterHoraceAndArtie()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Artie", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);

        tutorial.forcePatronIntoBarToSitAt("Old Man Horace", 2);
        tutorial.forceSeatToHaveSpecificJob(2, Patron.whatDoTheyWantToDo.ADVENTURE);

        TutorialReactions.Add(Mediator.ActionIdentifiers.DRINK_SERVED, TellPatronToGo);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, BothPatronsLeftCheck);
    }

    byte numPatronsLeft;
    void BothPatronsLeftCheck()
    {
        numPatronsLeft++;
        if (numPatronsLeft >= 2)
        {
            GaiusReturns();
        }
    }

    void TellPatronToGo()
    {
        tutorial.forceSeatToHaveSpecificJob((byte)tutorial.getCurrentTargetedSeatNumber(), Patron.whatDoTheyWantToDo.GOHOME);
    }

    void GaiusReturns()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Gaius", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.RUMOR);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterDeidre);
    }

    void EnterDeidre()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Deidre Downton", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EndDay);
    }

    void EndDay()
    {
        TutorialReactions.Clear();
        tutorial.forceEndOfDay();
        tutorial.SetCurrentTask(new Day5CorporealTransition(tutorial));
    }



    //byte seatIndex;
    //void EnterGaius()
    //{
    //    TutorialReactions.Clear();
    //    seatIndex = (byte)tutorial.getCurrentTargetedSeatNumber();

    //    tutorial.forcePatronIntoBarToSitAt("Gaius", seatIndex);

    //    tutorial.forceSeatToHaveSpecificJob(seatIndex, Patron.whatDoTheyWantToDo.ADVENTURE);
    //    TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, SendPatronHome);
    //}
}
