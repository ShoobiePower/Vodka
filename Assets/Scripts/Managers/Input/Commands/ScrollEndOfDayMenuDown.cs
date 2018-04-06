using UnityEngine;
using System.Collections;

public class ScrollEndOfDayMenuDown : CommandWithUndo
{



    public ScrollEndOfDayMenuDown() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.ScrollDown();
        }
        base.Execute(Bar);
    }
}