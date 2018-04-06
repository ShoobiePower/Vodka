using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RumorLoader :Loader
{
  
    enum rumorParserHelper { NAME,DESCRIPTION,QUESTS, UNLOCKFROMSTART }; // a more visual way to see the construction of the rumor

    private RumorWarehouse rumorWarehouse = new RumorWarehouse(); 
    private List<byte> allRumorNumbers; 


    public override void init()
    {
        loadJson("/JsonFiles/RumorsInGame.json");
    }

    private void declareSizeOfWarehouse()
    {
        rumorWarehouse.initRumorsByCharacterArray(jsonObject.Count);
    }

    public RumorWarehouse populateWarehouse()
    {
        declareSizeOfWarehouse();
        for (byte patronID = 0; patronID < jsonObject.Count; patronID++)
        {
            var rumorID = jsonObject[patronID].keys;
            foreach (string s in rumorID)
            {
               Rumor rumorToStore = rumorCreator(patronID, s);

                if (patronID < jsonObject.Count - 1)
                {
                    if (jsonObject[patronID][s][(int)rumorParserHelper.UNLOCKFROMSTART].b)
                        rumorWarehouse.storeRumor(rumorToStore);
                }

                else
                    rumorWarehouse.storeUnlockable(s, rumorToStore);
            }
        }

        return rumorWarehouse;
    }

    public Rumor unlockRumorFromJSON(string patronName, string rumorName)
    {
        byte patronId = (byte)jsonObject.keys.IndexOf(patronName);
        Rumor rumorToReturn = rumorCreator(patronId, rumorName);
        return rumorToReturn;
    }
  

    private Rumor rumorCreator(byte ownersID, string rumorIndexer)
    {
        List<string> namesToFind = new List<string>();
        JSONObject rumorToFind = jsonObject[ownersID][rumorIndexer];
        byte owningPatronsID = ownersID;
        string RumorName = rumorToFind[(int)rumorParserHelper.NAME].str;
        string RumorDescription = rumorToFind[(int)rumorParserHelper.DESCRIPTION].str;

        for (int i = 0; i < rumorToFind[(int)rumorParserHelper.QUESTS].Count; i++) // adds the names of the quests we would like to attach to this rumor, these will be found in RUMORQUESTCOMBINER.
        {
           namesToFind.Add(rumorToFind[(int)rumorParserHelper.QUESTS][i].str);
        }
        Rumor rumorToReturn = new Rumor(RumorName, RumorDescription, owningPatronsID, namesToFind);
        return rumorToReturn; 
    }
}
