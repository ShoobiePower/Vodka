using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour { 

    [SerializeField]
    GoldCounter goldCounter;
    private int gold;
    public int Gold { get { return gold; } set { gold = value; } }



    public void Start()
    {
       loadStarterPack();
    }
        
    
    public void loadStarterPack()
    {
        addGold(500);
    }

    public void addGold(int itemIsSoldFor)
    {
        gold += itemIsSoldFor;
        goldCounter.separateDigits(gold);
    }

    public void payGold(int goldYouPay)
    {
        gold -= goldYouPay;
    }


    public void addItemToInvintory(StoreableItem lootToAdd) // trying to refactor this to something else too!
    {
        if (lootToAdd is Currency)
        {
            Currency EquipmentToAdd = (Currency)lootToAdd;
            if (EquipmentToAdd.HowDoesThisItemLook == Currency.whosCurrencyIsThis.GOLD)
            {
                addGold(EquipmentToAdd.NumberLeft);
            }
        }
    }

   
}

   


