using UnityEngine;
using System.Collections;
using System;
using System.Text;

public interface ISeatStates 
{

    void FillSeat(Patron patronToSit);
    void TalkWithPatron();
    void ConsumeBeverage();
    void NoMoreQuestsAvailable();
    void PatronWantsToGoOnAdventure(); 
    void PatronSharesARumor();
    void PatronTalksAboutWaitingInBar();
    void PatronReturnsFromQuest();
   
}

public class NoOneSeated: ISeatStates
{ 
Seat seatToKeepTrackOf;

public NoOneSeated(Seat SeatToKeepTrackOf)
{
    seatToKeepTrackOf = SeatToKeepTrackOf;
}

    public void ConsumeBeverage()
    {
        // cant consume a beverage when no one is there
    }

    public void FillSeat(Patron patronToSit)
    {
        seatToKeepTrackOf.patron = patronToSit;
        seatToKeepTrackOf.GetComponent<SpriteRenderer>().sprite = ApperanceManager.instance.HowThisPatronLooks(patronToSit.Name); //patronToSit.ID
        seatToKeepTrackOf.barToken.sprite = ApperanceManager.instance.ThisPatronsBarToken(patronToSit.ID);
        seatToKeepTrackOf.GetComponent<BoxCollider2D>().enabled = true;
        seatToKeepTrackOf.setSeatState(seatToKeepTrackOf.SeatIsFilled());
    }

    public void TalkWithPatron()
    {

    }

    public void PatronSharesARumor()
    {

    }

    public void PatronWantsToGoOnAdventure()
    {
        throw new NotImplementedException();
    }

    public void PatronReturnsFromQuest()
    {
        throw new NotImplementedException();
    }

    public void NoMoreQuestsAvailable()
    {
        throw new NotImplementedException();
    }

    public void PatronTalksAboutWaitingInBar()
    {
        throw new NotImplementedException();
    }
}


public class PatronSeated : ISeatStates
{
    Seat seatToKeepTrackOf;

    public PatronSeated(Seat SeatToKeepTrackOf)
    {
        seatToKeepTrackOf = SeatToKeepTrackOf;
    }

    public void ConsumeBeverage()
    {
        // cannot drink unless they have ordered
        Debug.Log("Cannot drink unless they have ordered");
    }


    public void FillSeat(Patron patronToSit)
    {
        // can't sit if already seated;
    }

    public void TalkWithPatron()
    {
        if (seatToKeepTrackOf.patron.CurrentConversation == null || seatToKeepTrackOf.patron.CurrentConversation.IsConversationOver)
        {
            seatToKeepTrackOf.GetComponent<SpriteRenderer>().sprite = ApperanceManager.instance.HowThisPatronLooks(seatToKeepTrackOf.patron.Name);
            StringBuilder sb = new StringBuilder(JsonDialogueLoader.Instance.dioOut(seatToKeepTrackOf.patron.OrderThePatronWants.getKindOfOrder(), seatToKeepTrackOf.patron.ID));
            sb.Replace("{DRINK}", seatToKeepTrackOf.patron.OrderThePatronWants.describeOrder());
            seatToKeepTrackOf.patronSays(sb.ToString());
            seatToKeepTrackOf.setSeatState(seatToKeepTrackOf.orderHasBeenTaken());
            seatToKeepTrackOf.FadingText.SignalEndOfConversation();
        }

        else
        {
            seatToKeepTrackOf.GetComponent<SpriteRenderer>().sprite = ApperanceManager.instance.HowThisPatronLooks(seatToKeepTrackOf.patron.Name);

            if (seatToKeepTrackOf.patron.CurrentConversation.emoteOut() != string.Empty)
            {
                SoundManager.Instance.AddCommand(seatToKeepTrackOf.patron.Name + seatToKeepTrackOf.patron.CurrentConversation.emoteOut());
                seatToKeepTrackOf.GetComponent<SpriteRenderer>().sprite = ApperanceManager.instance.HowThisPatronLooks(seatToKeepTrackOf.patron.Name+ '_'  +seatToKeepTrackOf.patron.CurrentConversation.emoteOut());
                Debug.Log(seatToKeepTrackOf.patron.Name + seatToKeepTrackOf.patron.CurrentConversation.emoteOut());
            }

         seatToKeepTrackOf.patronSays(seatToKeepTrackOf.patron.CurrentConversation.dioOut());

        }

    }

    public void PatronSharesARumor()
    {
        throw new NotImplementedException();
    }

    public void PatronWantsToGoOnAdventure()
    {
        seatToKeepTrackOf.setSeatState(seatToKeepTrackOf.patronWouldLikeToGoOnAdventure());
    }

    public void PatronReturnsFromQuest()
    {
        seatToKeepTrackOf.setSeatState(seatToKeepTrackOf.patronIsReturningFromAdventure());
    }

    public void NoMoreQuestsAvailable()
    {
        throw new NotImplementedException();
    }

    public void PatronTalksAboutWaitingInBar()
    {
        throw new NotImplementedException();
    }
}

public class PatronOrdered : ISeatStates
{
    Seat seatToKeepTrackOf;

    public PatronOrdered(Seat SeatToKeepTrackOf)
    {
        seatToKeepTrackOf = SeatToKeepTrackOf;
    }

    public void ConsumeBeverage()  
    {
        seatToKeepTrackOf.CanDrink = false;
        seatToKeepTrackOf.patronsMug.showMug();

        seatToKeepTrackOf.FadingText.SignalEndOfConversation();

        if (seatToKeepTrackOf.patron.currentActivity == Patron.whatDoTheyWantToDo.ADVENTURE)
        {
            seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.GOQUEST, seatToKeepTrackOf.patron.ID));
        } 
        else if (seatToKeepTrackOf.patron.currentActivity == Patron.whatDoTheyWantToDo.RUMOR)
        {
            seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.RUMOR, seatToKeepTrackOf.patron.ID));
        }                                                                  

    }


    public void FillSeat(Patron patronToSit)
    {
        // can't sit if already seated;
    }

    public void TalkWithPatron()
    {
        seatToKeepTrackOf.FadingText.SignalEndOfConversation();
        seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.TALK, seatToKeepTrackOf.patron.ID));
    }

    public void PatronSharesARumor()
    {
        seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.RUMOR, seatToKeepTrackOf.patron.ID));
    }

    public void PatronWantsToGoOnAdventure()
    {
        seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.GOQUEST, seatToKeepTrackOf.patron.ID));
    }

    public void PatronReturnsFromQuest()
    {
        throw new NotImplementedException();
    }

    public void NoMoreQuestsAvailable()
    {
    seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.NOMOREQUEST, seatToKeepTrackOf.patron.ID));

    }

    public void PatronTalksAboutWaitingInBar()
    {
        seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.WAITINBAR, seatToKeepTrackOf.patron.ID));
    }
}


   public class PatronReturningFromAdventure : ISeatStates
    {
        Seat seatToKeepTrackOf;

        public PatronReturningFromAdventure(Seat SeatToKeepTrackOf)
        {
            seatToKeepTrackOf = SeatToKeepTrackOf;
        }

        public void ConsumeBeverage()
        {
            // may introduce a lower price of quest???
        }

        public void FillSeat(Patron patronToSit)
        {
            // cannot fill seat if patron is already sitting here
        }

        public void TalkWithPatron()
        {

        seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.QUESTRETURN, seatToKeepTrackOf.patron.ID));
        seatToKeepTrackOf.setSeatState(seatToKeepTrackOf.SeatIsFilled()); // here 
        }

        public void PatronSharesARumor()
        {
            throw new NotImplementedException();
        }

        public void PatronWantsToGoOnAdventure()
        {
            throw new NotImplementedException();
        }

    public void PatronReturnsFromQuest()
    {
        throw new NotImplementedException();
    }

    public void NoMoreQuestsAvailable()
    {
        throw new NotImplementedException();
    }

    public void PatronTalksAboutWaitingInBar()
    {
        throw new NotImplementedException();
    }
}





