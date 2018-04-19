using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class patronLoader : Loader
{
    private string FallThroughHelper;  // used to help me find JSON typos

   private enum jsonHelper {NAME,TOLERANCE,SKILLS,BS1,BS2,BS3, BIO };      

    private List<byte> indexingNumbers;
    private byte numberOfPatronsInGame;

    public override void init()
    {
        loadJson("/JsonFiles/CharactersInGame.json");
        indexingNumbers = new List<byte>();
        numberOfPatronsInGame = (byte)jsonObject[0].Count;
    }

    public Patron unlockSpecificPatron(string name)
    {
        var keyFinder = jsonObject[0].keys;
        byte index = 0;
        byte ID;
        Patron patronToReturn; 
        foreach (string o in keyFinder)
        {
            if (o == name)
            {
                break;
            }
            index++;
        }

        ID = (byte)(index);
        patronToReturn = spawnPatron(0, index, ID);
        return patronToReturn;
    }

    private Patron spawnPatron(byte lockLevel, byte patronIndex, byte patronID) // I dont know about this... parsing stuff. Not sure if its a pain or if its good. 
    {
        JSONObject randomlyCraftedPatron = jsonObject[lockLevel][patronIndex];
        string craftedName = randomlyCraftedPatron[(int)jsonHelper.NAME].str;
        FallThroughHelper = craftedName;
        Patron.SkillTypes[] patronSkills = packageSkills(randomlyCraftedPatron[(int)jsonHelper.SKILLS]);// package(randomlyCraftedPatron[(int)jsonHelper.SKILLS]);
        Patron.drinkLevel CraftedDrinkLevel = ToleranceParser(randomlyCraftedPatron[(int)jsonHelper.TOLERANCE].str);
        string craftedBIO = @randomlyCraftedPatron[(int)jsonHelper.BIO].str;
        Patron patronToReturn = new Patron(craftedName,patronSkills, CraftedDrinkLevel, craftedBIO);
        patronToReturn.ID = patronID;
        Debug.Log(craftedName + "Has been spawned");
        return patronToReturn;
    }


    private Patron.drinkLevel ToleranceParser(string levelToParse)
    {


        switch (levelToParse.ToLower())
        {

            case "low":
                {
                    return Patron.drinkLevel.LOW;
                }
            case "mid":
                {
                    return Patron.drinkLevel.MID;
                }
            case "high":
                {
                    return Patron.drinkLevel.HIGH;
                }
            case "none":
                {
                    return Patron.drinkLevel.NONE;
                }
            default:
                {
                    Debug.Log("Drink level Fall through:" + FallThroughHelper);
                    return Patron.drinkLevel.NONE;
                }
        }
    }

    private Patron.SkillTypes[] packageSkills(JSONObject statsToPackage)  // don't know if this is confusing, basicly I just wanted to pass all of the stats as an array and not by themselves. 
    {
        Patron.SkillTypes[] craftedSkills = new Patron.SkillTypes[statsToPackage.Count];
        for (int j= 0; j < statsToPackage.Count; j++)
        {
            craftedSkills[j] = lookUpSkillType(statsToPackage[j].str);
        }
        return craftedSkills;
    }

    private Patron.SkillTypes lookUpSkillType(string statToDetermine)
    {
        switch (statToDetermine.ToLower())
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
                    Debug.Log("Fall through on search of trial type, im in patron loader");
                    return Patron.SkillTypes.STRONG;
                }
        }
    }

    public byte getNumberOfPatronsInGame()
    {
        return numberOfPatronsInGame;
    }
}
