using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drink  {


    private string drinkName;
    public string DrinkName { get { return drinkName; } set { drinkName = value; } }

    private Patron.SkillTypes buff;
    public Patron.SkillTypes Buff { get { return buff; } set { buff = value; } }

    private int numberOfIngredentsDrink;
    public int NumberOfIngredentsInDrink { get { return numberOfIngredentsDrink; } set { numberOfIngredentsDrink = value; } }
    const int MAX_INGREDIENTS_IN_CUP = 4;

    public enum flavor {SWEET,BITTER,STRONG,CLASSIC}
    private flavor thisDrinksFlavor;
    public flavor ThisDrinksFlavor { get { return thisDrinksFlavor; } set { thisDrinksFlavor = value; } }

   
    private byte[] drinkIngredents = new byte[MAX_INGREDIENTS_IN_CUP];
    public byte[] DrinkIngredents { get { return drinkIngredents; } set { drinkIngredents = value; } }

    private byte correctPrice;
    public byte CorrectPrice { get { return correctPrice; } set { correctPrice = value; } }

    private byte mixUpPrice;
    public byte MixUpPrice { get { return mixUpPrice; } set { mixUpPrice = value; } }

    private string drinkDescription;
    public string DrinkDescription { get { return drinkDescription; } set { drinkDescription = value; } }

    private string recipeForDrink;
    public string RecipeForDrink { get { return recipeForDrink; } set { recipeForDrink = value; } }

    public bool TryAddIngredentToDrink(Ingredient ingredentToAddToDrink)
    {
        if (ingredentToAddToDrink != null && numberOfIngredentsDrink < MAX_INGREDIENTS_IN_CUP)
        {
            drinkIngredents[(int)ingredentToAddToDrink.ThisIngredentsColor]++;
            numberOfIngredentsDrink++;
            return true;
        }
        else
        {
            Debug.Log("Drink: ingredient could not be added :'(");
        }

        return false;
    }

    public void clearIngredentsInDrink()
    {
        for(int i = 0; i < drinkIngredents.Length; i++)
        {
            drinkIngredents[i] = 0;
        }

        numberOfIngredentsDrink = 0;
    }

    public byte[] getIngredentsInDrink()
    {
        return drinkIngredents;
    }

    public int countIngredentsInDrink()
    {
        return numberOfIngredentsDrink;
    }

    //public string listIngredentsInDrink()
    //{
    //    string ing = string.Empty;
    //    for ( byte i = 0; i < MAX_INGREDIENTS_IN_CUP; i++)  // Ingredient.ingredientColor i = 0; i < Ingredient.ingredientColor.LENGTH ; i++
    //    {
    //        if (drinkIngredents[(byte)i] > 0)
    //        {
    //            ing += drinkIngredents[(byte)i] + "X" + i.ToString().ToLower() + "\n";
    //        }
    //    }
    //    return ing;
    //}

    //public Ingredent.ingredentColor scanIngredent(int ingredentNumber)
    //{
    //    return ingredentsInDrink[ingredentNumber].ThisIngredentsColor;
    //}
    //public int countIngredentsInDrink()
    //{
    //    return ingredentsInDrink.Count;
    //}

}
