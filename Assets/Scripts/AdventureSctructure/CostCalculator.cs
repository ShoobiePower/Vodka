using UnityEngine;
using System.Collections;

public class CostCalculator  {

     int runningCostOfQuest;
    public int RunningCostOfQuest { get { return runningCostOfQuest; } }
    const byte BaseCost = 30;
    private int costOfTravelPerDay = 5;
    public int CostOfTravelPerDay { set { costOfTravelPerDay = value; } } // so we can do fun things like hazard pay, bandits, price os upplies going up
     
    

    public string priceOfTravelTime(int howManyDaysOnQuest)
    {
        int priceOfTravel = costOfTravelPerDay * howManyDaysOnQuest;
        runningCostOfQuest += priceOfTravel;
        return "Supplies for " + howManyDaysOnQuest + "Trip " + priceOfTravel + "\n";
    }

    //public string moralCodeChecker(Patron.Aligence patronsAligence, Patron.Aligence questAlligence)
    //{
    //    int factionModifier;
    //    if (patronsAligence == questAlligence)
    //    {
    //        factionModifier = -10;
    //        runningCostOfQuest += factionModifier; // for now a magic number, later detemined by who is in the lead ect; 
    //        return "Faction Bonus" + factionModifier + "\n";
    //    }

        //else check for enemies

    //    else
    //        return null;
    //}

    public string patronLevelModifier(byte patronLevel)
    {
        int priceOfLevel = patronLevel * 15; // again a magic number we can change; 
        runningCostOfQuest += priceOfLevel;
        return "Price based on adventurer experience" + priceOfLevel + "\n";
    }

    public string addBaseCost()
    {
        runningCostOfQuest += BaseCost;
        return "Base Cost" + BaseCost;
    }

    public void clearCost()
    {
        runningCostOfQuest = 0;
    }
}
