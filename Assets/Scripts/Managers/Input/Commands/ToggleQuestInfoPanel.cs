using UnityEngine;
using System.Collections;

public class ToggleQuestInfoPanel : CommandWithUndo
{
    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<MapManager>();
        if (target is MapManager)
        {
            //HACK
            target.getQuestInfoPanel.HideSelf();
        }
        base.Execute(Bar);
    }
}
