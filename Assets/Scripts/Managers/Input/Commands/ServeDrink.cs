using UnityEngine;
using System.Collections;

public class ServeDrink : CommandWithUndo
{


    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.serveDrink();
            //target.prepareDrinkToBeServed(); 
        }
        base.Execute(Bar);
    }
}

