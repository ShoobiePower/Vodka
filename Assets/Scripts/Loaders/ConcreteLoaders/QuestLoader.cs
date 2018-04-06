using UnityEngine;
using System.Collections.Generic;


public class QuestLoader : Loader {

    UnlockerLoader unlockerLoader;
    enum questParserHelper { NAME, DESCRIPTION, CHECKS , PATRONUNLOCK, RUMORONRETURN ,RUMORUNLOCK ,EVENTUNLOCK, LOCATIONUNLOCK , CONVERSATIONUNLOCK, LOCATION,DAYS,RESPONCES ,BANLIST , LENGTH}; // a more visual way to see the construction of the quest
 

    public override void init()
    {
        loadJson("/JsonFiles/QuestsInGame.json");
        unlockerLoader = new UnlockerLoader();
    }


    public Quest createQuestByName(string nameOfQuestToFind)
    {
        JSONObject QuestToFind = targetObjectFromJson(nameOfQuestToFind);
        Quest questToBuild = new Quest();
        questToBuild.QuestName = QuestToFind[(byte)questParserHelper.NAME].str;
        questToBuild.QuestDescription = QuestToFind[(byte)questParserHelper.DESCRIPTION].str;
        questToBuild.QuestLocation = QuestToFind[(byte)questParserHelper.LOCATION].str.ToUpper();
        questToBuild.DaysToCompleteQuest = (byte)QuestToFind[(byte)questParserHelper.DAYS].i;
        questToBuild.TrialsOfTheQuest = questCheckCreator(QuestToFind[(byte)questParserHelper.CHECKS]);
        questToBuild.PersonalResponcesForQuest = packageResponces(QuestToFind[(byte)questParserHelper.RESPONCES]);
        questToBuild.BanList = assembleBanList(QuestToFind[(byte)questParserHelper.BANLIST]);
        AddAllKindsOfUnlocker(questToBuild, QuestToFind);
        return questToBuild;
    }

   

    private List<Check> questCheckCreator(JSONObject allTrials)
    {
        List<Check> checksToReturn = new List<Check>();

        for (int i = 0; i < allTrials.Count; i++)
        {
            Patron.SkillTypes skillToCheck = determineSkillType(allTrials[i].str);
            Check trialToQueue = new Check(skillToCheck);
            checksToReturn.Add(trialToQueue);
        }
        return checksToReturn;
    }

    private List<string> assembleBanList(JSONObject patronsToBan)
    {
        List<string> namesToReturn = new List<string>();

        for (int i = 0; i <patronsToBan.Count; i++)
        {
            namesToReturn.Add(patronsToBan[i].str);
        }
        return namesToReturn;
    }

    private Patron.SkillTypes determineSkillType(string statToDetermine)
    {

        switch(statToDetermine.ToLower())
        {
            case "strong":
                {
                    return Patron.SkillTypes.STRONG;
                }

            case "smart":
                {
                    return Patron.SkillTypes.SMART;
                }

            case "sneak":
                {
                    return Patron.SkillTypes.SNEAK;
                }

            case "sway":
                {
                    return Patron.SkillTypes.SWAY;
                }

            default:
                {
                    Debug.Log("Fall through on search of trial type, im in quest loader");
                    return Patron.SkillTypes.STRONG;
                }
        }
    
    }

    private void AddAllKindsOfUnlocker(Quest questToAddTo, JSONObject whatToAdd)
    {
        List<Unlocker> unlockersListToAdd = new List<Unlocker>();
        
        for (questParserHelper i = questParserHelper.PATRONUNLOCK; i < questParserHelper.LOCATION; i++) 
        {
            for (int j = 0; j < whatToAdd[(int)i].Count; j++)
            {
                JSONObject unlockerComponents = whatToAdd[(int)i][j];
                unlockersListToAdd.Add(unlockerLoader.createUnlocker(i.ToString(), unlockerComponents));
            }
        }
        questToAddTo.ThingsToUnlock = unlockersListToAdd;
    }

    private string[] packageResponces(JSONObject responcesToPackage)
    {
        string[] arrayToReturn = new string[responcesToPackage.Count - 1];
        for (int i = 0; i < responcesToPackage.Count -1; i++)
        {
            arrayToReturn[i] = responcesToPackage[i].str;
        }
        return arrayToReturn;

    }
}

public class Check
{
    private bool isTrialPassed;
    public bool IsTrialPassed { get { return isTrialPassed; } set { isTrialPassed = value; } }
                 
    public Patron.SkillTypes skillToCheckFor;
    public Check(Patron.SkillTypes skillToCheckFor)
    {
        isTrialPassed = false;
        this.skillToCheckFor = skillToCheckFor;
    }

}
