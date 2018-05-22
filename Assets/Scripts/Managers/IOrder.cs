using UnityEngine;
using System.Collections;
using System;


public interface IOrder {

    string describeOrder();
    bool checkAccuracy(Drink drinkToCheck);
    JsonDialogueLoader.responceType getKindOfOrder();
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

    public JsonDialogueLoader.responceType getKindOfOrder()
    {
        throw new NotImplementedException();
    }
}

//public class OrderByFlavor : IOrder
//{
//    Drink.flavor desiredFlavor;
    
//    public OrderByFlavor(Drink.flavor DesiredFlavor)
//    {
//        desiredFlavor = DesiredFlavor;
//    }

//    public bool checkAccuracy(Drink drinkToCheck)
//    {
//        return (drinkToCheck.ThisDrinksFlavor == desiredFlavor);
//    }

//    public string describeOrder()
//    {
//        return " a " + desiredFlavor.ToString() + " drink";
//    }

//    public JsonDialogueLoader.responceType getKindOfOrder()
//    {
//        throw new NotImplementedException();
//    }
//}

public class OrderByIngredent : IOrder
{
    Ingredient ingredentToInclude;

    public OrderByIngredent(Ingredient.ingredientColor colorOfRequest)
    {
        ingredentToInclude = new Ingredient(colorOfRequest);
    }

    public bool checkAccuracy(Drink drinkToCheck)
    {
        return (drinkToCheck.DrinkIngredents[(int)ingredentToInclude.ThisIngredentsColor] != 0);
    }

    public string describeOrder()
    {
       // string preloadedString = string.Format(" a drink that has <color=red>{0}</color> ", ingredentToInclude);
        return System.String.Format(ingredentToInclude.sayName()); 
    }

    public JsonDialogueLoader.responceType getKindOfOrder()
    {
        return JsonDialogueLoader.responceType.DRINKWITH;
    }
}

public class OrderByLackOfIngredient : IOrder
{
    Ingredient ingredentToAvoid;

    public OrderByLackOfIngredient()
    {
        ingredentToAvoid = new Ingredient((Ingredient.ingredientColor)UnityEngine.Random.Range((int)Ingredient.ingredientColor.red, (int)Ingredient.ingredientColor.LENGTH));
    }

    public bool checkAccuracy(Drink drinkToCheck)
    {
        return (drinkToCheck.DrinkIngredents[(int)ingredentToAvoid.ThisIngredentsColor] == 0);
    }

    public string describeOrder()
    {
        return System.String.Format(ingredentToAvoid.sayName());
    }

    public JsonDialogueLoader.responceType getKindOfOrder()
    {
        return JsonDialogueLoader.responceType.DRINKWITHOUT;
    }
}