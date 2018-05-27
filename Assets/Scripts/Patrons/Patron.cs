using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Patron {

    private bool isOnQuest;
    public bool IsOnQuest { get { return isOnQuest; } set { isOnQuest = value; } }

    public bool IsUnlocked { get; private set; }

    public bool HasLeveledUp { get; private set; }

    private Quest questToCompleete;
    public Quest QuestToCompleete { get { return questToCompleete; } set { questToCompleete = value; } }

    private byte id;
    public byte ID { get { return id; } set { id = value; } }

    public enum SkillTypes { STRONG, SMART, SNEAK, SWAY, NONE, LENGTH }

    public enum whatDoTheyWantToDo { RUMOR, ADVENTURE, TURNIN, GOHOME, CONVERSE } 
    public whatDoTheyWantToDo currentActivity; 

    private string name;
    public string Name { get { return name; } set { name = value; } }

    private byte level;
    public byte Level { get { return level; } set { level = value; } }

    private int bondPoints;
    public int BondPoints { get { return bondPoints; } }

    private int bondGainedOnThisQuest;
    public int BondGainedOnThisQuest { get { return bondGainedOnThisQuest; } set { bondGainedOnThisQuest = value; } }

    private const int maxLevel = 3; // Hard number per design's request. I'll ask after rookies as to what we should do. 

    private SkillTypes[] patronSkillsToUnlock;

    private List<SkillTypes> patronSkills = new List<SkillTypes>();
    public List<SkillTypes> PatronSkills { get { return patronSkills; } }

    public SkillTypes SkillGrantedByDrink { get; set; }

    private int thresholdToNextBondLevel = 10; // Placeholder for points needed to level up bond. 
    public int ThresholdToNextBondLevel { get { return thresholdToNextBondLevel; } }

    private IOrder orderThePatronWants;
    public IOrder OrderThePatronWants {get { return orderThePatronWants; } set { orderThePatronWants = value; } }

    private string bio;
    public string Bio { get { return bio; } }

    private Conversation currentConversation;
    public Conversation CurrentConversation { get; set; }
    
    public Patron(string newName, SkillTypes[] patronSkillsToUnlock ,string bioIn)
    {
        name = newName;
        level = 1;
        this.patronSkillsToUnlock = patronSkillsToUnlock;
        bio = bioIn;
        unlockNewSkill();
    }

    private void levelUpBond()
    {
      bondPoints -= ThresholdToNextBondLevel;
      level++;
      unlockNewSkill();
      // unlock new conversations;
     // unlock new rumors
    // unlock new Art?

        //else display MAX in manager
    }

    private void unlockNewSkill()
    {
        if (patronSkillsToUnlock.Length >= level)
        patronSkills.Add(patronSkillsToUnlock[level - 1]);
    }

    public bool checkForSkill(SkillTypes skillToCheckFor)
    {
        foreach (SkillTypes skill in patronSkills)
        {
            if (skill == skillToCheckFor)
            {
                return true;
            }
        }
        if (SkillGrantedByDrink == skillToCheckFor)
        {
            return true;
        }

            return false;
    }

    public void convertGainedBondToTotalBond() 
    {
        if (level < maxLevel) // HERE
        {
            bondPoints += bondGainedOnThisQuest;
            checkForLevelUp();
        }
        bondGainedOnThisQuest = 0;
    }

    private void checkForLevelUp()
    {
        HasLeveledUp = false;

        if (bondPoints >= thresholdToNextBondLevel && level < maxLevel)
        {
            levelUpBond();
            HasLeveledUp = true;
        }
    }

    public string convertSkillsListToString()
    {
        string stringToReturn = string.Empty;
        for (int i = 0; i < patronSkills.Count; i++)
        {
            stringToReturn += patronSkills[i].ToString() + "\n";
        }
        return stringToReturn;
    }

    public void confirmUnlock()
    {
        IsUnlocked = true;
    }

    public bool IsMaxLevel()
    {
        return level == maxLevel;
    }

    public bool HasAnyMoreSkills()
    {
        return level <= patronSkillsToUnlock.Length;
    }
}


