using UnityEngine;
using System.Collections;

public class Stanza 
{
    string characterDiolouge;
    public enum stanzaEmotion { NONE, INQUISITIVE, CHEERFUL, SPECIAL };
    public stanzaEmotion thisStanzasEmotion; 
    // emotion emphasis for stanza;
 

    public Stanza(string characterDiolouge,  stanzaEmotion arguedInEmotion) 
    {
        this.characterDiolouge = characterDiolouge;
        thisStanzasEmotion = arguedInEmotion;
    }

    public string stanzaSays()
    {
        return characterDiolouge;
    }

}

