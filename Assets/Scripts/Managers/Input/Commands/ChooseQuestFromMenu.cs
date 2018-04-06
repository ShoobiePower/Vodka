using UnityEngine;
using System.Collections;

public class ChooseQuestFromMenu : CommandWithUndo
{

    byte index;
    public ChooseQuestFromMenu(int index) : base()
    {
        this.index = (byte)index;
    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<MapManager>();
        if (target is MapManager)
        {
            target.getQuestInfoPanel.displayInfoBasedOnMenuChoice(index);
        }
        base.Execute(Bar);
    }
}
