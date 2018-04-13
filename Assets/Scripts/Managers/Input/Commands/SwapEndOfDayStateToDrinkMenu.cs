using UnityEngine;
using System.Collections;

public class SwapEndOfDayStateToDrinkMenu : CommandWithUndo
{
    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.FlipToDrinkMenu();
        }
        base.Execute(Bar);
    }
}
