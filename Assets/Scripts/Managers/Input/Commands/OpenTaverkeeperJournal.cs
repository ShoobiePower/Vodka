using UnityEngine;
using System.Collections;

public class OpenTaverkeeperJournal : CommandWithUndo
{


    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<EndOfDayManager>();
        if (target is EndOfDayManager)
        {
            target.openTavernKeeperJournal();
        }
        base.Execute(Bar);
    }
}
