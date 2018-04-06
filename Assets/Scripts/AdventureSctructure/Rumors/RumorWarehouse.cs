using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RumorWarehouse
{
   // List<Rumor> commonKnowlage = new List<Rumor>();
    List<Rumor>[] rumorsByCharacter;
    Dictionary<string, Rumor> unlockableRumors = new Dictionary<string, Rumor>();
    QuestLoader questLoader = new QuestLoader(); 


    public void initRumorWarehouse()
    {
        questLoader.init();
    }

    public void initRumorsByCharacterArray(int charactersInGame)
    {
        rumorsByCharacter = new List<Rumor>[charactersInGame];

        for (int i = 0; i < rumorsByCharacter.Length; i++)
        {
            rumorsByCharacter[i] = new List<Rumor>();
        }
    }

    public void storeRumor(Rumor rumorToStore) //int characterID,
    {
        rumorToStore.QuestForThisRumor.Clear();
        rumorsByCharacter[rumorToStore.OwnersID].Add(rumorToStore);
    }

    public void storeUnlockable(string nameOfUnlockableRumor, Rumor rumorToStore)
    {
        unlockableRumors.Add(nameOfUnlockableRumor, rumorToStore);
    }

    public Rumor giveRandomRumorBasedOnCharacter(byte characterID)
    {
        Rumor rumorToReturn;

            int randy = Random.Range(0, rumorsByCharacter[characterID].Count);
            rumorToReturn = rumorsByCharacter[characterID][randy];
            rumorsByCharacter[characterID].RemoveAt(randy);
        Debug.Log("This is how many rumors " + characterID + " Has:" + rumorsByCharacter[characterID].Count);
            rumorToReturn = attachQuestsToRumor(rumorToReturn, characterID);
            return rumorToReturn;
    }

    public Rumor unlockSpecificRumorBasedOnName(string name)
    {
        Rumor RumorToAssign = unlockableRumors[name];
        attachQuestsToRumor(RumorToAssign, RumorToAssign.OwnersID);
        return RumorToAssign;
    }

    public int getNumberOfRumorsRemainingInCharacter(byte patronID)
    {
        return rumorsByCharacter[patronID].Count;
    }

    private Rumor attachQuestsToRumor(Rumor rumorToModify, byte characterID)
    {
        rumorToModify.QuestForThisRumor.Clear(); 

        foreach (string questName in rumorToModify.QuestsToFind)
        {
            Quest questToAdd = new Quest();
            questToAdd = questLoader.createQuestByName(questName);
            questToAdd.RumorStory = rumorToModify.RumorDescription;
            rumorToModify.QuestForThisRumor.Add(questToAdd);
        }
        return rumorToModify;
    }
}
