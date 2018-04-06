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

    public enum Aligence { COLLEGE, CORPOREAL, AA, EYES, NONE };
    public Aligence thisPatronsAligence;

    public enum drinkLevel { NONE, LOW, MID, HIGH };
    public drinkLevel thisPatronsTolerance;

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

    private int maxLevel; 

    private SkillTypes[] patronSkillsToUnlock;

    private List<SkillTypes> patronSkills = new List<SkillTypes>();
    public List<SkillTypes> PatronSkills { get { return patronSkills; } }

    public SkillTypes SkillGrantedByDrink { get; set; }

    private int thresholdToNextBondLevel = 9; // Placeholder for points needed to level up bond. 
    public int ThresholdToNextBondLevel { get { return thresholdToNextBondLevel; } }

    private IOrder orderThePatronWants;
    public IOrder OrderThePatronWants {get { return orderThePatronWants; } set { orderThePatronWants = value; } }

    private string bio;
    public string Bio { get { return bio; } }

    private Conversation currentConversation;
    public Conversation CurrentConversation { get; set; }
    
    public Patron(string newName, SkillTypes[] patronSkillsToUnlock , drinkLevel newDrinkLevel,string bioIn)
    {
        name = newName;
        level = 1;
        thisPatronsTolerance = newDrinkLevel;
        this.patronSkillsToUnlock = patronSkillsToUnlock;
        maxLevel = patronSkillsToUnlock.Length;
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
        patronSkills.Add(patronSkillsToUnlock[level - 1]);
    }

    public bool checkForSkill(SkillTypes skillToCheckFor)
    {
        Debug.Log("Now checking for skill");
        foreach (SkillTypes skill in patronSkills)
        {
            Debug.Log(name + " : " + skill.ToString());
            if (skill == skillToCheckFor)
            {
                Debug.Log("We pass back true");
                return true;
            }
        }
        if (SkillGrantedByDrink == skillToCheckFor)
        {
            Debug.Log("We pass back true From drink");
            Debug.Log("our drink skill" + SkillGrantedByDrink);
            return true;
        }

         Debug.Log("We pass back false");
            return false;

    }

    public void convertGainedBondToTotalBond() 
    {
        bondPoints += bondGainedOnThisQuest;
        checkForLevelUp();
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
}


