using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FillableMug : MonoBehaviour 
{
 
    Drink drinkInMug = new Drink();

    private const int maxSizeOfMug = 2;
    public int MaxSizeOfMug { get { return maxSizeOfMug; } } 

    [SerializeField]
    Image[] ingredientSquares;

    public Drink DrinkInMug { get { return drinkInMug; } }

    public void AddIngredientToMug(Ingredient ingredentToAdd)
    {
        if (TryAddIngredentToDrink(ingredentToAdd))
        {
            displayIngredientInSquare(ingredentToAdd.ThisIngredentsColor);
        }
        else
        {
            //trigger some sort of "tink" sound / deactivate drink taps
            //play some sort of overflow animation
        }
    }

    private void displayIngredientInSquare(Ingredient.ingredientColor colorToDisplay)
    {
        ingredientSquares[drinkInMug.countIngredentsInDrink()-1].sprite = ApperanceManager.instance.getIngredientColor((byte)colorToDisplay);
    }

    private bool TryAddIngredentToDrink(Ingredient ingredentToAdd)
    {
        return drinkInMug.TryAddIngredentToDrink(ingredentToAdd);
    }

    public void ClearIngredientInMug()
    {
        clearAllIngredientSquares();
        clearIngredentsInDrink();
    }

    private void clearAllIngredientSquares()
    {
        for (int i = 0; i < ingredientSquares.Length; i++)
        {
            ingredientSquares[i].sprite = ApperanceManager.instance.getBlankIngredientColor();
        }
    }

    private void clearIngredentsInDrink()
    {
        drinkInMug.clearIngredentsInDrink();
    }

    // some functionallity to tell the player what is in the mug, 

    // some animation for deployment of mug

}
