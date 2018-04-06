using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rumor
{

    public Rumor(string name, string description, byte _ownersID, List<string> inQuestsToFind)
    {
        rumorName = name;
        rumorDescription = description;
        ownersID = _ownersID;
        questsToFind = inQuestsToFind;
    }
    private string rumorName;
    public string RumorName { get { return rumorName; } }

    private string rumorDescription;
    public string RumorDescription { get { return rumorDescription; } }

    private byte ownersID;
    public byte OwnersID { get { return ownersID; }  }

    private List<string> questsToFind = new List<string>();
    public List<string> QuestsToFind { get { return questsToFind; }  }

    private List<Quest> questsForThisRumor = new List<Quest>();
    public List<Quest> QuestForThisRumor { get { return questsForThisRumor; }  }

}
