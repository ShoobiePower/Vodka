using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Quest 
{
    public enum questStatus { PENDING, TAKEN,PASS};
    questStatus currentQuestStatus = questStatus.PENDING;

    private byte tokenNumber; 

    private byte daysToCompleteQuest;
    public byte DaysToCompleteQuest { get { return daysToCompleteQuest; } set { daysToCompleteQuest = value; } }

    private string questName;
    public string QuestName { get { return questName; } set { questName = @value; } }

    private string questDescription;
    public string QuestDescription { get { return questDescription; } set { questDescription = @value; } }

    private List<Check> trialsOfTheQuest = new List<Check>();
    public List<Check> TrialsOfTheQuest { get { return trialsOfTheQuest; } set { trialsOfTheQuest = value; } }

    private List<Unlocker> thingsToUnlock = new List<Unlocker>();
    public List<Unlocker> ThingsToUnlock { get { return thingsToUnlock; } set { thingsToUnlock = value; } }

    private List<string> banList = new List<string>();
    public List<string> BanList { get { return banList; } set { banList = value; } }

    private string questLocation;
    public string QuestLocation { get { return questLocation; } set { questLocation = value; } }

    private string[] personalResponcesForQuest;
    public string[] PersonalResponcesForQuest { set { personalResponcesForQuest = value; } }

    private string resolution;
    public string Resolution { get { return resolution; }}

    private string rumorStory;
    public string RumorStory { get { return rumorStory; } set { rumorStory = value; } }

    public void ChangeQuestStatusToPass()
    {
        currentQuestStatus = questStatus.PASS;
    }

    public void ChangeQuestStatusToTaken()
    {
        currentQuestStatus = questStatus.TAKEN;
    }

    public questStatus getQuestStatus()
    {
        return currentQuestStatus;
    }


    public void decrementDaysToCompleteQuest()
    {
        daysToCompleteQuest--;
    }

    public string convertChecksToString()
    {
        string stringToReturn = string.Empty;
        for (int i = 0; i < trialsOfTheQuest.Count; i++)
        {
            stringToReturn += trialsOfTheQuest[i].skillToCheckFor.ToString();
        }
        return stringToReturn;
    }

    public bool isPatronValidForQuest(string patronName)
    {
        string nameToCheck = patronName.ToLower();
        
        for (int i = 0; i < banList.Count; i++)
        {
            if (banList[i].ToLower() == nameToCheck) { return false; }
        }

        return true;
    }

    public string getPersonalResponceForQuest(byte i)
    {
        resolution = personalResponcesForQuest[i];
        return personalResponcesForQuest[i];
    }

    public void SetTokenNumberForQuest(byte i)
    {
        tokenNumber = i; 
    }

    public byte GetTokenNumberForQuest()
    {
        return tokenNumber;
    }

}
