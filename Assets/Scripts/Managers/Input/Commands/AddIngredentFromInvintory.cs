using UnityEngine;
using System.Collections;

public class AddIngredientFromInventory : CommandWithUndo
{

    byte slotToFind;


    public AddIngredientFromInventory(byte invintorySlot) : base()
    {
        slotToFind = invintorySlot;
    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.MakeDrink(slotToFind);
        }

        base.Execute(Bar);
    }
}
