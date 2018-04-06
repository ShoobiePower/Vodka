using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatronManager : Colleague {

    private patronLoader pLoader = new patronLoader(); 
    // TODO, get the crier system working. If a quest fails by clock out, someone needs to report on it!

    public List<Patron> PatronsForTheDay = new List<Patron>(); // where we pull our patrons from to go to the bar
    public List<Patron> Regulars = new List<Patron>();
    public List<Patron> AllKnownPatrons = new List<Patron>();

    private byte numberOfPatronsInGame;
    public byte NumberOfPatronsInGame { get { return numberOfPatronsInGame; } }

    // all the junk pertaining to selecting a more fixed number of patrons with wants in the day, added by NC 10/23
    byte numberOfAdventurersToday;
    byte numberOfVisitingPatrons;
    const float percentOfAllPatronsIsAdventurer = .25f;  // one in four; 
    //const float percentOfAllPatronsIsQuestGiver = .3f; // one in three;

    private bool canWeSpawnAdventurers;
    public bool CanWeSpawnAdventurers { set { canWeSpawnAdventurers = value; } }

    public void init()
    {
        pLoader.init();
        loadRegulars();
        numberOfPatronsInGame = pLoader.getNumberOfPatronsInGame();
    }

    private void loadRegulars()
    {
        unlockNewPatronAndAdd("Old Man Horace");
        unlockNewPatronAndAdd("Nell"); 
        unlockNewPatronAndAdd("Artie");
        unlockNewPatronAndAdd("Deirdre Downton");
    }

    public Patron drawAPatron()
    {
        int patronIndexer = Random.Range(0, PatronsForTheDay.Count);
        Patron patronToReturn = PatronsForTheDay[patronIndexer];
        PatronsForTheDay.RemoveAt(patronIndexer);
        return patronToReturn;
    }

   

    private byte decideHowManyPatronsVisitToday()
    {
       float unrefinedNumberToReturn = Regulars.Count * Random.Range(0.7f, 1);
       return (byte)System.Math.Ceiling(unrefinedNumberToReturn);
    }

    public void preparePatronsForDay()
    {
        numberOfVisitingPatrons = decideHowManyPatronsVisitToday();
        decideHowManyAdventurersToday();
        //decideHowManyQuestGiversToday();
        for(byte i = numberOfVisitingPatrons; i > 0; i--)
        {
            PatronsForTheDay.Add(ReadyPatronForDay());
        }
    }


    private Patron ReadyPatronForDay()  // Readies a patron from our list of regulars by giving them their time in the bar based on their patience, and give them what they will want to do. 
    {
            int patronIndexer = Random.Range(0, Regulars.Count);
            Patron patronToReturn = Regulars[patronIndexer];
            Regulars.RemoveAt(patronIndexer);

        if (patronToReturn.currentActivity != Patron.whatDoTheyWantToDo.TURNIN)
            patronToReturn.currentActivity = chooseCurrentActivity();

            return patronToReturn;
    }

    private void decideHowManyAdventurersToday() // decides how many of the patrons that visit today will want to go on an adventure
    {
       float unRefinednumberOfAdventurersToday = (numberOfVisitingPatrons * percentOfAllPatronsIsAdventurer) + addAdventureNoise();
        numberOfAdventurersToday = (byte)System.Math.Ceiling(unRefinednumberOfAdventurersToday);
    }

    private int addAdventureNoise() // sorry about this, but for now, just calls a random, I didn't know if we wanted anything special for our noise generator. 
    {
        return Random.Range(0, 2);
    }

    private int addQuestGiverNoise()
    {
        return Random.Range(-1, 2);
    }

        private void resetDefaults(Patron patronToReset) // Resets our patron's defauts after they are done with a quest, this includes resting back to full strength and telling the end of day manager that they are no longer on a quest by changing the patrons IsOnQuest stat.
    {
        patronToReset.SkillGrantedByDrink = Patron.SkillTypes.NONE;
    }

    public void putAPatronBack(Patron returningPatron)
    {
        if (returningPatron.IsUnlocked)
        {
            if (returningPatron.currentActivity != Patron.whatDoTheyWantToDo.TURNIN)
            {
                Regulars.Add(returningPatron);
                returningPatron.CurrentConversation = null; // HERE

            }
            else
            {
                PatronsForTheDay.Add(returningPatron); // ensures our little quester shows up the next day.
            }
            resetDefaults(returningPatron);
            returningPatron.OrderThePatronWants = null;
            Debug.Log("patronReturning");
        }
    }

    public Patron.whatDoTheyWantToDo chooseCurrentActivity()
    {
           if (numberOfAdventurersToday > 0)
        {
            if (canWeSpawnAdventurers)
            {
                Debug.Log("PatronChoseAdventure");
                numberOfAdventurersToday--;
                return Patron.whatDoTheyWantToDo.ADVENTURE;
            }
            else
                return Patron.whatDoTheyWantToDo.RUMOR;
        }
        else
        {
            Debug.Log("PatronChoseConverse");
            return Patron.whatDoTheyWantToDo.RUMOR;
        }
    }

    public void unlockNewPatronAndAdd(string name)
    {
        Patron p = getPatronOfNameFromLoader(name);
        Regulars.Add(p);
        AllKnownPatrons.Add(p);
        p.confirmUnlock();
    }
    #region tutorialForceCommands
    public Patron getPatronOfNameFromLoader(string name)
    {
       return pLoader.unlockSpecificPatron(name);
    }

    public Patron drawAPatronByName(string name)
    {
        for (int i = 0; i < Regulars.Count; i++)
        {
            if (Regulars[i].Name == name)
            {
                Patron patronToReturn = Regulars[i];
                Regulars.RemoveAt(i);
                return patronToReturn;
            }
        }
        return getPatronOfNameFromLoader(name);
    }

    #endregion
}
