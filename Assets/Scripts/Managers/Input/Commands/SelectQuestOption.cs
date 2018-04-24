using UnityEngine;
using System.Collections;

public class SelectQuestOption : CommandWithUndo {

    public SelectQuestOption() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<RumorBoardUI>();
        if (target is RumorBoardUI)
        {
            target.SelectQuestFromOptions();
        }
        base.Execute(Bar);
    }
}
