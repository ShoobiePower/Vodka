using UnityEngine;
using System.Collections;

public class OpenAndClosePatronInfoSheet : CommandWithUndo
{
    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<MapManager>();
        if (target is MapManager)
        {
            //HACK
            target.deployStateScript.togglePatronCheatSheet();
        }
        base.Execute(Bar);
    }
}
