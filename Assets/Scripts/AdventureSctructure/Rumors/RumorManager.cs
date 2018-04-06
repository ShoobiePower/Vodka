using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RumorManager : MonoBehaviour
{
   // List<Rumor> activeRumors = new List<Rumor>();

    RumorWarehouse rumorWarehouse;
    RumorLoader rLoader = new RumorLoader();

    public void initRumorWarehouse()
    {
        rLoader.init();
        rumorWarehouse = rLoader.populateWarehouse();
        rumorWarehouse.initRumorWarehouse();
    }

    public Rumor getRandomRumorFromWarehouseByCharacter(byte patronID)
    {
        Rumor rumorToReturn = rumorWarehouse.giveRandomRumorBasedOnCharacter(patronID);
        return rumorToReturn;
    }

    public Rumor unlockRumor(string questToUnlock)
    {
       return rumorWarehouse.unlockSpecificRumorBasedOnName(questToUnlock);
    }

   public void unlockRumorForPatron(string patronsName, string rumorName)
    {
        Rumor rumorToStore = rLoader.unlockRumorFromJSON(patronsName, rumorName);
        rumorWarehouse.storeRumor(rumorToStore);
    }

    public int getNumberOfRumorsLeftInCharacter(byte patronID)
    {
       return rumorWarehouse.getNumberOfRumorsRemainingInCharacter(patronID);
    }
}

