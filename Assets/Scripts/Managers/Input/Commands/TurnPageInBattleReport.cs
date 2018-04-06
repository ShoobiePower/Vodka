using UnityEngine;
using System.Collections;

public class TurnPageInBattleReport : CommandWithUndo
{ 

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BattleReportManager>();
        if (target is BattleReportManager)
        {
            target.flipPageInBattleReport();
        }
        base.Execute(Bar);
    }
}
