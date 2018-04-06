using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConversationWarehouse
{
    //singleton;
    List<Conversation>[] charactersConversations;
    ConversationLoader conversationLoader;
    byte stanzaIterator;

    public void initConversationWarehouse(byte numberOfPatronsInTheGame)
    {
        charactersConversations = new List<Conversation>[numberOfPatronsInTheGame];
        conversationLoader = new ConversationLoader();
        conversationLoader.init();
        for (byte i = 0; i < numberOfPatronsInTheGame; i++)
        {
            charactersConversations[i] = conversationLoader.loadPatronConversationsBasedOnId(i);
        }
    }

    public Conversation getRandomConversationBasedOnPatronID(byte patronID)
    {
        int conversationIndexer = Random.Range(0, charactersConversations[patronID].Count);
        Conversation conversationToReturn = charactersConversations[patronID][conversationIndexer];
        conversationToReturn.resetConversation();
        if (conversationToReturn.IsOneShot)
        {
            charactersConversations[patronID].RemoveAt(conversationIndexer);
        }
        return conversationToReturn;
    }

    public void unlockNewConversations(string patronName, string nameOfConversation)
    {
        sbyte ourPatronsID = conversationLoader.findIndexOfName(patronName);
        charactersConversations[ourPatronsID].Add(conversationLoader.getSpecificLockedConversation((byte)ourPatronsID, nameOfConversation));
    }

    public Conversation getSpecificConversationFromLoader(byte patronID, string nameOfConversation)
    {
       Conversation conversationToReturn = conversationLoader.getSpecificLockedConversation(patronID, nameOfConversation);
        return conversationToReturn;
    }

    public Conversation getPatronAdventureConversationFromLoader(byte patronID)
    {
        return getSpecificConversationFromLoader(patronID, "Adventure");
    }
}

