using UnityEngine;
using System.Collections;

public class SwapEndOfDayStateToIdle : CommandWithUndo
{



    public SwapEndOfDayStateToIdle() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.FlipToEndOfDayIdle();
        }
        base.Execute(Bar);
    }
}
