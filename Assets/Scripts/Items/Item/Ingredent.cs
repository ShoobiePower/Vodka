using UnityEngine;
using System.Collections;

public class Ingredient : StoreableItem {

    public enum ingredientColor {red, yellow, green, blue, LENGTH }
    private ingredientColor thisIngredentsColor;
    public ingredientColor ThisIngredentsColor { get { return thisIngredentsColor; } set { thisIngredentsColor = value; } }
    private string[] NamesForIngredent = {"Firehops", "Skyburst", "Everdeep", "Beryluna" }; 
    Sprite howDoILook;

	public Ingredient(ingredientColor ThisIngredentsColor)
    {
        thisIngredentsColor = ThisIngredentsColor;
    }

    public override string sayName()
    {
        return NamesForIngredent[(byte)thisIngredentsColor]; //thisIngredentsColor.ToString(); 
    }

    public override Sprite displayArt()
    {
        if (howDoILook == null)
        {
            howDoILook = ApperanceManager.instance.whatDoesTheIngredentLookLike(thisIngredentsColor);
        }
        return howDoILook;
    }

    

}
