using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class ConversationLoader : Loader
{
    enum ConversationParserHelper {NAME,ONESHOT,UNLOCKEDFROMSTART,STANZA, PATRONUNLOCK, RUMORUNLOCK, EVENTUNLOCK, LOCATIONUNLOCK, CONVERSATIONUNLOCK, LENGTH }
    UnlockerLoader unlockerLoader = new UnlockerLoader();

    public override void init()
    {
        loadJson("/JsonFiles/Conversations.json");
    }

    public List<Conversation> loadPatronConversationsBasedOnId(byte patronID)
    {
        List<Conversation> conversationsToReturn = new List<Conversation>();
        for(int i = 0; i < jsonObject[patronID].Count; i++)
        {
            if (jsonObject[patronID][i][(int)ConversationParserHelper.UNLOCKEDFROMSTART].b == true)
            {
                Conversation conversationToAdd = conversationConstructor(jsonObject[patronID][i]);
                conversationsToReturn.Add(conversationToAdd);
            }
        }
        return conversationsToReturn;
    }

    public Conversation getSpecificLockedConversation(byte patronID, string nameOfConversation)
    {
        Conversation conversationToReturn = conversationConstructor(jsonObject[patronID][nameOfConversation]);
        if (conversationToReturn == null) {
            Debug.Log("Warning, could not find conversation named" + nameOfConversation + " Defaulting to basic chatter");
            return conversationConstructor(jsonObject[patronID][0]);
        }

        else
            return conversationToReturn;
    }

    private Conversation conversationConstructor(JSONObject conversationToConstruct)
    {
        Conversation conversationToReturn = new Conversation(conversationToConstruct[(int)ConversationParserHelper.NAME].str, conversationToConstruct[(int)ConversationParserHelper.ONESHOT].b);
        for (int i = 0; i < conversationToConstruct[(int)ConversationParserHelper.STANZA].Count; i++)
        ;
            conversationToReturn.addStanza(stanzaToAdd);
        }
        AddAllKindsOfUnlocker(conversationToReturn, conversationToConstruct);
        return conversationToReturn;
    }

    private Stanza.stanzaEmotion getEmotionForStanza(string emotionToSearchFor)
   { 
       switch(emotionToSearchFor.ToLower())
     {
        case "none":
         {
           break;
         }
     }
   }

    private void AddAllKindsOfUnlocker( Conversation conversationToModify,JSONObject whatToAdd)
    {
        List<Unlocker> unlockersListToAdd = new List<Unlocker>();

        for (ConversationParserHelper i = ConversationParserHelper.PATRONUNLOCK; i < ConversationParserHelper.LENGTH; i++)
        {
            for (int j = 0; j < whatToAdd[(int)i].Count; j++)
            {
                JSONObject unlockerComponents = whatToAdd[(int)i][j];
                unlockersListToAdd.Add(unlockerLoader.createUnlocker(i.ToString(), unlockerComponents));
            }
        }
        conversationToModify.ThingsThisConversationUnlocks = unlockersListToAdd;
    }

    public sbyte findIndexOfName(string patronName)
    {
        for( sbyte i = 0; i < jsonObject.Count; i++)
        {
            if (jsonObject.keys[i] == patronName)
            {
                return i;
            }
        }

        return -1; 
    }


}

