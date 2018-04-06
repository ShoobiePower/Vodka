using UnityEngine;
using System.Collections;

public class ScrollRightInBar : CommandWithUndo
{



    public ScrollRightInBar() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.panBarRight();
        }
        base.Execute(Bar);
    }
}
