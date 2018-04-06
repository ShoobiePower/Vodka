using UnityEngine;
using System.Collections;

public class RecycleDrink : CommandWithUndo
{

   

    public RecycleDrink() : base()
    {
        
    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.recycleDrink();
        }
        base.Execute(Bar);
    }
}
