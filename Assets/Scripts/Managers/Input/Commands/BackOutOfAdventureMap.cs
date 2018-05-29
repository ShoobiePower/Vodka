using UnityEngine;
using System.Collections;

public class BackOutOfAdventureMap : CommandWithUndo
{

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.setBarState(target.noOneInteractedWith());
           // target.BackOutOfAdventureMap();
        }
        base.Execute(Bar);
    }

}
