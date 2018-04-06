using UnityEngine;
using System.Collections;

public class SelectQuestOption : CommandWithUndo {

    byte indexer;
    public SelectQuestOption(byte indexer) : base()
    {
        this.indexer = indexer;
    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<RumorBoardUI>();
        if (target is RumorBoardUI)
        {
            Debug.Log("HIT THE WHEEL");
            target.SelectQuestFromOptions(indexer);
        }
        base.Execute(Bar);
    }
}
