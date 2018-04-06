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
    Ingredient.ingredientColor ingredentToInclude;

    
    
    public OrderByIngredent(Ingredient.ingredientColor colorOfRequest)
    {
        ingredentToInclude = colorOfRequest;
    }

    public bool checkAccuracy(Drink drinkToCheck)
    {
        return (drinkToCheck.DrinkIngredents[(int)ingredentToInclude] != 0);
    }

    public string describeOrder()
    {
       // string preloadedString = string.Format(" a drink that has <color=red>{0}</color> ", ingredentToInclude);
        return System.String.Format(" a drink that has {0} ", ingredentToInclude); 
    }
}

public class OrderByLackOfIngredent : IOrder
{
    Ingredient.ingredientColor ingredentToAvoid;

    public OrderByLackOfIngredent()
    {
        ingredentToAvoid = (Ingredient.ingredientColor)UnityEngine.Random.Range((int)Ingredient.ingredientColor.RED, (int)Ingredient.ingredientColor.LENGTH);
    }

    public bool checkAccuracy(Drink drinkToCheck)
    {
        return (drinkToCheck.DrinkIngredents[(int)ingredentToAvoid] == 0);
    }

    public string describeOrder()
    {
        return " a drink without " + ingredentToAvoid;
    }
}