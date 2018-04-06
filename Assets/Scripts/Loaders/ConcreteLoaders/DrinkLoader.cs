using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DrinkLoader : Loader  {

    private string FallThroughHelper;  // used to help me find JSON typos
                                       // private static patronLoader instance = null;
                                       // private static readonly object padloc = new object();

    private enum drinkjsonHelper {NAME,RED,YELLOW,BLUE,GREEN,FLAVOR,CORRECT,MIXUP,BUFF};   

    public override void init()
    {
        loadJson("/JsonFiles/DrinkList.json");
    }

    public List<Drink> populateDrinkCollection()
    {
        List<Drink> drinkListToReturn = new List<Drink>();
        for (int i = 0; i < jsonObject.Count; i++)
        {
           drinkListToReturn.Add(DrinkCreator(i));
        }
        return drinkListToReturn;
    }



    private Drink DrinkCreator(int drinkIndexer)
    {
        Drink drinkToCreate = new Drink();
        drinkToCreate.DrinkName = jsonObject[drinkIndexer][(int)drinkjsonHelper.NAME].str; 
        drinkToCreate.DrinkIngredents[0] = (byte)jsonObject[drinkIndexer][(int)drinkjsonHelper.RED].i;
        drinkToCreate.DrinkIngredents[1] = (byte)jsonObject[drinkIndexer][(int)drinkjsonHelper.YELLOW].i;
        drinkToCreate.DrinkIngredents[2] = (byte)jsonObject[drinkIndexer][(int)drinkjsonHelper.GREEN].i;
        drinkToCreate.DrinkIngredents[3] = (byte)jsonObject[drinkIndexer][(int)drinkjsonHelper.BLUE].i;
        drinkToCreate.CorrectPrice = (byte)jsonObject[drinkIndexer][(int)drinkjsonHelper.CORRECT].i;
        drinkToCreate.MixUpPrice = (byte)jsonObject[drinkIndexer][(int)drinkjsonHelper.MIXUP].i;
        drinkToCreate.ThisDrinksFlavor = drinkFlavorParser(jsonObject[drinkIndexer][(int)drinkjsonHelper.FLAVOR].str);
        drinkToCreate.NumberOfIngredentsInDrink = addAllIngredents(drinkToCreate);
        drinkToCreate.Buff = drinkBuffParser(jsonObject[drinkIndexer][(int)drinkjsonHelper.BUFF].str);
        return drinkToCreate;
    }

    private int addAllIngredents(Drink drinkWithIngredentsToCount)
    {
        int runningTotalToReturn = 0;

        for (int i = 0; i < drinkWithIngredentsToCount.DrinkIngredents.Length; i++)
        {
            runningTotalToReturn += drinkWithIngredentsToCount.DrinkIngredents[i];
        }

        return runningTotalToReturn;
    }



    private Drink.flavor drinkFlavorParser(string flavorToParse)
    {
        switch (flavorToParse.ToLower())
        {

            case "sweet":
                {
                    return Drink.flavor.SWEET;
                }
            case "bitter":
                {
                    return Drink.flavor.BITTER;
                }
            case "strong":
                {
                    return Drink.flavor.STRONG;
                }
            case "classic":
                {
                    return Drink.flavor.CLASSIC;
                }
            default:
                {
                    Debug.Log("Flavor Fall through:" + FallThroughHelper);
                    return Drink.flavor.STRONG;
                }

        }


    }


    private Patron.SkillTypes drinkBuffParser(string buffToParse)
    {
        switch (buffToParse.ToLower())
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
                    Debug.Log("Buff Fall through:" + FallThroughHelper);
                    return Patron.SkillTypes.STRONG;
                }

        }


    }
}
