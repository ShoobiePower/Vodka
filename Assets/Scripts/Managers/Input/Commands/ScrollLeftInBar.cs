using UnityEngine;
using System.Collections;

public class ScrollLeftInBar : CommandWithUndo{



    public ScrollLeftInBar() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.panBarLeft();
        }
        base.Execute(Bar);
    }
}
