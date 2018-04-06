using UnityEngine;
using System.Collections;

public class Ingredient : StoreableItem {

    public enum ingredientColor {RED, YELLOW, GREEN, BLUE, LENGTH }
    private ingredientColor thisIngredentsColor;
    public ingredientColor ThisIngredentsColor { get { return thisIngredentsColor; } }
    Sprite howDoILook;

	public Ingredient(ingredientColor ThisIngredentsColor)
    {
        thisIngredentsColor = ThisIngredentsColor;
    }

    public override string sayName()
    {
        return thisIngredentsColor.ToString(); // an overrided to string may let us put in neat names for our ingredents
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
