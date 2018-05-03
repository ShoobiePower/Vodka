using UnityEngine;
using System.Collections;

public class Ingredient : StoreableItem {

    public enum ingredientColor {red, yellow, green, blue, LENGTH }
    private ingredientColor thisIngredentsColor;
    public ingredientColor ThisIngredentsColor { get { return thisIngredentsColor; } set { thisIngredentsColor = value; } }
    private string[] NamesForIngredent = { "<color=#FF0000>Firehops</color>", "<color=#FFFF00>Skyburst</color>", "<color=#00A86B>Everdeep</color>", "<color=#498FFF>Beryluna</color>" }; 
    Sprite howDoILook;

	public Ingredient(ingredientColor ThisIngredentsColor)
    {
        thisIngredentsColor = ThisIngredentsColor;
    }

    public override string sayName()
    {
        return NamesForIngredent[(byte)thisIngredentsColor];
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
