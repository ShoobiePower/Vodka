using UnityEngine;
using System.Collections;

public class CloseAdventureMap : CommandWithUndo
{



    public CloseAdventureMap() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<MapManager>();
        if (target is MapManager)
        {
            target.closeMapProps();
        }
        base.Execute(Bar);
    }
}
