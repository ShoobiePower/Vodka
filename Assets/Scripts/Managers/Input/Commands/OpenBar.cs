using UnityEngine;
using System.Collections;

public class OpenBar : CommandWithUndo
{
    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.EndPhase();
        }
        base.Execute(Bar);
    }
}

