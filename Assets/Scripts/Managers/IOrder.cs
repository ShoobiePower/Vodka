using UnityEngine;
using System.Collections;
using System;

public interface IOrder {

    string describeOrder();
    bool checkAccuracy(Drink drinkToCheck);
}

public class OrderByName : IOrder
{
    private Drink drinkDesired;

    public OrderByName(Drink DrinkDesired)
    {
        drinkDesired = DrinkDesired;
    }

    public bool checkAccuracy(Drink drinkToCheck)
    {
       for (int i = 0; i < drinkToCheck.DrinkIngredents.Length; i++)
        {
            if (drinkToCheck.DrinkIngredents[i] != drinkDesired.DrinkIngredents[i])
                return false;
        }
        return true;
    }

    public string describeOrder()
    {
        return " a " + drinkDesired.DrinkName;
    }
}

public class OrderByFlavor : IOrder
{
    Drink.flavor desiredFlavor;
    
    public OrderByFlavor(Drink.flavor DesiredFlavor)
    {
        desiredFlavor = DesiredFlavor;
    }

    public bool checkAccuracy(Drink drinkToCheck)
    {
        return (drinkToCheck.ThisDrinksFlavor == desiredFlavor);
    }

    public string describeOrder()
    {
        return " a " + desiredFlavor.ToString() + " drink";
    }

    
}

public class OrderByIngredent : IOrder
{
    Ingredient.ingredientColor ingredientToInclude;

    
    
    public OrderByIngredent(Ingredient.ingredientColor colorOfRequest)
    {
        ingredientToInclude = colorOfRequest;
    }

    public bool checkAccuracy(Drink drinkToCheck)
    {
        return (drinkToCheck.DrinkIngredents[(int)ingredientToInclude] != 0);
    }

    public string describeOrder()
    {
       // string preloadedString = string.Format(" a drink that has <color=red>{0}</color> ", ingredentToInclude);
        return System.String.Format(" a drink that has <color={0}>{0}</color> ", ingredientToInclude);
    }
}

public class OrderByLackOfIngredient : IOrder
{
    Ingredient.ingredientColor ingredientToAvoid;

    public OrderByLackOfIngredient()
    {
        ingredientToAvoid = (Ingredient.ingredientColor)UnityEngine.Random.Range((int)Ingredient.ingredientColor.red, (int)Ingredient.ingredientColor.LENGTH);
    }

    public bool checkAccuracy(Drink drinkToCheck)
    {
        return (drinkToCheck.DrinkIngredents[(int)ingredientToAvoid] == 0);
    }

    public string describeOrder()
    {
        return System.String.Format(" a drink without <color={0}>{0}</color> ", ingredientToAvoid);
    }
}