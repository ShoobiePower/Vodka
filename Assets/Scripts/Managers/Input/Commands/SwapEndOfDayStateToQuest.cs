using UnityEngine;
using System.Collections;

public class SwapEndOfDayStateToQuest :  CommandWithUndo
{



    public SwapEndOfDayStateToQuest() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.FlipToQuestPage();
        }
        base.Execute(Bar);
    }
}
