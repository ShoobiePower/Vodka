using UnityEngine;
using System.Collections;

public class SwapEndOfDayStateToPatronStat :  CommandWithUndo
{



    public SwapEndOfDayStateToPatronStat() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.FlipToPatronStatPage();
        }
        base.Execute(Bar);
    }
}
