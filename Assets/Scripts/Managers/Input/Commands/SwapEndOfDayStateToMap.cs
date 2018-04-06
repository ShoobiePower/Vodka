using UnityEngine;
using System.Collections;

public class SwapEndOfDayStateToMap : CommandWithUndo
{



    public SwapEndOfDayStateToMap() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.FlipToMap();
        }
        base.Execute(Bar);
    }
}
