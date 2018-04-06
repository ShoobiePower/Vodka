using UnityEngine;
using System.Collections;

public class ScrollBarUp : CommandWithUndo
{



    public ScrollBarUp() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.ScrollUp();
        }
        base.Execute(Bar);
    }
}

