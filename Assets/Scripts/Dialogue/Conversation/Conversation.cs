using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversation : ISubject
{
    Unlocker thingToBeUnlockedAfterConversation;
    public Conversation(string nameOfConversation, bool _isOneShot)
    {
        nameOfThisConversation = nameOfConversation;
        isOneShot = _isOneShot;
        isConversationOver = false;
        registerSelfToMediator();
    }
    //stores stanzas
    List<Stanza> stanzasInConversation = new List<Stanza>();
    List<Unlocker> thingsThisConversationUnlocks = new List<Unlocker>();
    public List<Unlocker> ThingsThisConversationUnlocks{ get { return thingsThisConversationUnlocks; } set { thingsThisConversationUnlocks = value; } }

    // index of conversation
    byte stanzaNumber;
    public byte DEBUGstanzaNumber { get { return stanzaNumber; } }

    public byte DEBUGstanzaCount { get { return (byte)stanzasInConversation.Count; } }

    //Check if conversation is over
    private bool isConversationOver;
    public bool IsConversationOver{get { return isConversationOver; }}

    // a conversation should have a string identifier if we need to force pull it up.
    // such as for rumor related. 
    private string nameOfThisConversation;
    public string NameOfThisConversation { get { return nameOfThisConversation; } }

    public IOrder forcedOrder { get; private set; }

    // Does this go away after used
    private bool isOneShot;
    public bool IsOneShot { get { return isOneShot; } }

    // shares a string
    public string dioOut()
    {
        string stanzaToReturn = stanzasInConversation[stanzaNumber].stanzaSays();
        incrementStanza();
        return stanzaToReturn;
    }

    // increment stanza
    private void incrementStanza()
    {
        stanzaNumber++;
        checkIfConversationIsOver();
    }

    private void checkIfConversationIsOver()
    {
        if (stanzaNumber == stanzasInConversation.Count)
        {
            isConversationOver = true;
            notifyObserver(Mediator.ActionIdentifiers.CONVERSATION_ENDED);
        }
    }
    
    // Load strings
    public void addStanza(Stanza stanzaToLoad)
    {
        stanzasInConversation.Add(stanzaToLoad);
    }

    public void addUnlocker(Unlocker unlockerToAdd)
    {
        thingsThisConversationUnlocks.Add(unlockerToAdd);
    }

    public void resetConversation()
    {
        stanzaNumber = 0;
        isConversationOver = false;
    }

    public Unlocker shareThingToBeUnlocked()
    {
        return thingToBeUnlockedAfterConversation;
    }

    #region Observer Pattern

    List<IObserver> observers = new List<IObserver>();

    public void registerObserver(IObserver observerToAdd)
    {
        observers.Add(observerToAdd);
    }

    public void unregisterObserver(IObserver observerToRemove)
    {
        observers.Remove(observerToRemove);
    }

    public void notifyObserver(Mediator.ActionIdentifiers ActionIdentifier)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].notifyObserver(ActionIdentifier);
        }
    }

    public void registerSelfToMediator()
    {
        Mediator.Register(this);
    }

    public void unregisterSelfToMediator()
    {
        Mediator.Unregister(this);
    }
    #endregion
}

