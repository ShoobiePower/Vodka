using UnityEngine;
using System.Collections;

public class GetInfoFromEndOfDayButton : CommandWithUndo
{

    byte slotToFind;

    public GetInfoFromEndOfDayButton(byte invintorySlot) : base()
    {
        slotToFind = invintorySlot;
    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.ShowStatsOnPage(slotToFind);
        }
        base.Execute(Bar);
    }
}
