using UnityEngine;
using System.Collections;

public class InteractWithBookOfPatrons : CommandWithUndo
{



    public InteractWithBookOfPatrons() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.swapToPatronBioScreen();
        }
        base.Execute(Bar);
    }
}
