using UnityEngine;
using System.Collections;

public class DismissAdventurer :CommandWithUndo
{

    public DismissAdventurer() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.dismissAdventurer();
        }
        base.Execute(Bar);
    }
}
