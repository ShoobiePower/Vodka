using UnityEngine;
using System.Collections;

public class JumpToSeat : CommandWithUndo
{

    sbyte seatToJumpTo;
    public JumpToSeat(sbyte seatToJumpTo) : base()
    {
        this.seatToJumpTo = seatToJumpTo;
    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.jumpToSeatInBar(seatToJumpTo);
        }
        base.Execute(Bar);
    }
}
