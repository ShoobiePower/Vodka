using UnityEngine;
using System.Collections;
using System;
using System.Text;

public interface ISeatStates 
{

    void FillSeat(Patron patronToSit);
    void TalkWithPatron();
    void ConsumeBeverage(); 
    void PatronWantsToGoOnAdventure(); 
    void PatronSharesARumor();
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
        seatToKeepTrackOf.GetComponent<SpriteRenderer>().sprite = ApperanceManager.instance.HowThisPatronLooks(patronToSit.ID);
        seatToKeepTrackOf.barToken.sprite = ApperanceManager.instance.ThisPatronsBarToken(patronToSit.ID);
        seatToKeepTrackOf.GetComponent<BoxCollider2D>().enabled = true;
        seatToKeepTrackOf.makeTextBoxClickable();
        seatToKeepTrackOf.setSeatState(seatToKeepTrackOf.SeatIsFilled());
    }

    public void TalkWithPatron()
    {
        // Ghosts can't order... yet!
    }

    public void PatronSharesARumor()
    {
        // Napstablook isn't feeling up to gossip. 
    }

    public void PatronWantsToGoOnAdventure()
    {
        throw new NotImplementedException();
    }

    public void PatronReturnsFromQuest()
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
            StringBuilder sb = new StringBuilder(JsonDialogueLoader.Instance.dioOut(seatToKeepTrackOf.patron.OrderThePatronWants.getKindOfOrder() ,seatToKeepTrackOf.patron.ID));
            sb.Replace("{DRINK}", seatToKeepTrackOf.patron.OrderThePatronWants.describeOrder());
            seatToKeepTrackOf.patronSays(sb.ToString()); 
            seatToKeepTrackOf.setSeatState(seatToKeepTrackOf.orderHasBeenTaken());
        }

        else
        {
            if (seatToKeepTrackOf.patron.CurrentConversation.emoteOut() != string.Empty)
                SoundManager.Instance.AddCommand(seatToKeepTrackOf.patron.Name + seatToKeepTrackOf.patron.CurrentConversation.emoteOut());

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
}

public class PatronOrdered : ISeatStates
{
    Seat seatToKeepTrackOf;

    public PatronOrdered(Seat SeatToKeepTrackOf)
    {
        seatToKeepTrackOf = SeatToKeepTrackOf;
    }

    public void ConsumeBeverage()  //Drink drinkToConsume
    {
        seatToKeepTrackOf.CanDrink = false;
        seatToKeepTrackOf.patronsMug.showMug(); 
          
        if (seatToKeepTrackOf.patron.currentActivity == Patron.whatDoTheyWantToDo.ADVENTURE)
        {
            seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.GOQUEST, seatToKeepTrackOf.patron.ID));
        } 
        else if (seatToKeepTrackOf.patron.currentActivity == Patron.whatDoTheyWantToDo.RUMOR)
        {
            seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.RUMOR, seatToKeepTrackOf.patron.ID));
        }                                                                  
        
        seatToKeepTrackOf.setSeatState(seatToKeepTrackOf.patronIsdrinking()); // forces it to a plane of existance that it can't return from untill it's timer runs out. 
    }


    public void FillSeat(Patron patronToSit)
    {
        // can't sit if already seated;
    }

    public void TalkWithPatron()
    {
        StringBuilder sb = new StringBuilder(JsonDialogueLoader.Instance.dioOut(seatToKeepTrackOf.patron.OrderThePatronWants.getKindOfOrder(), seatToKeepTrackOf.patron.ID));
        sb.Replace("{DRINK}", seatToKeepTrackOf.patron.OrderThePatronWants.describeOrder());
        seatToKeepTrackOf.patronSays(sb.ToString());  // JsonDialogueLoader.Instance.dioOut(seatToKeepTrackOf.patron.OrderThePatronWants.describeOrder().responceType.DRINK, seatToKeepTrackOf.patron.ID) +
    }

    public void PatronSharesARumor()
    {
        seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.RUMOR, seatToKeepTrackOf.patron.ID));
    }

    public void PatronWantsToGoOnAdventure()
    {
        throw new NotImplementedException();
    }

    public void PatronReturnsFromQuest()
    {
        throw new NotImplementedException();
    }
}

public class PatronIsDrinking : ISeatStates
{
    Seat seatToKeepTrackOf;

    public PatronIsDrinking(Seat SeatToKeepTrackOf)
    {
        seatToKeepTrackOf = SeatToKeepTrackOf;
    }

    public void ConsumeBeverage()
    {
        // patron is already drinking
    }


    public void FillSeat(Patron patronToSit)
    {
        // no one can take this patrons spot yet
    }

    public void TalkWithPatron()
    {
       
        seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.TALK, seatToKeepTrackOf.patron.ID));
    }

    public void PatronSharesARumor()
    {
        
    }

    public void PatronWantsToGoOnAdventure()
    {
       
    }

    public void PatronReturnsFromQuest()
    {
        throw new NotImplementedException();
    }
}

public class PatronIsDrunk : ISeatStates    // IGNORE THIS FOR NOW
{
    Seat seatToKeepTrackOf;

    public PatronIsDrunk (Seat SeatToKeepTrackOf)
    {
        seatToKeepTrackOf = SeatToKeepTrackOf;
    }

    public void ConsumeBeverage()
    {
        // patron is already drinking
    }


    public void FillSeat(Patron patronToSit)
    {
        // no one can take this patrons spot yet
    }

    public void TalkWithPatron()
    {
        // May change to talk 
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
}

public class PatronWantsAdventure : ISeatStates
{

    Seat seatToKeepTrackOf;

    public PatronWantsAdventure(Seat SeatToKeepTrackOf)
    {
        seatToKeepTrackOf = SeatToKeepTrackOf;
        // this seat, display icon. 
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
        if (!seatToKeepTrackOf.patron.CurrentConversation.IsConversationOver)
       seatToKeepTrackOf.patronSays(seatToKeepTrackOf.patron.CurrentConversation.dioOut());
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
        //if (seatToKeepTrackOf.patron.WasQuestSucessful)
        //seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.TRIUMPH, seatToKeepTrackOf.patron.thisPatronsDisposition));
        //else
        //    seatToKeepTrackOf.patronSays(JsonDialogueLoader.Instance.dioOut(JsonDialogueLoader.responceType.FAIL, seatToKeepTrackOf.patron.thisPatronsDisposition));
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
}





