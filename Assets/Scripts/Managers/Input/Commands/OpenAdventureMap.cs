using UnityEngine;
using System.Collections;

public class OpenAdventureMap : CommandWithUndo
{

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.OpenMapFromBar(target.SelectedSeat.patron);
        }
        base.Execute(Bar);
    }
}
