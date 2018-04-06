using UnityEngine;
using System.Collections;

public class Currency : StoreableItem
{
    private string currencyName;
    public string CurrencyName { get { return currencyName; } set { currencyName = value; } }

    public enum whosCurrencyIsThis { GOLD, AA, EYES, COLLEGE, CORPOREAL };
    private whosCurrencyIsThis HowDoesThisCurrencyLook;
    public whosCurrencyIsThis HowDoesThisItemLook { get { return HowDoesThisCurrencyLook; } set { HowDoesThisCurrencyLook= value; } }

    Sprite howDoILook;


    public override string sayName()
    {
        return currencyName; 
    }

    public override Sprite displayArt()
    {
        if (howDoILook == null)
        {
            howDoILook = ApperanceManager.instance.whatCurrencyLooksLike(HowDoesThisCurrencyLook);
        }
        return howDoILook;
    }
}
