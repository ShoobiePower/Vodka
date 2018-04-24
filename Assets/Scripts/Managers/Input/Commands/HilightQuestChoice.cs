using UnityEngine;
using System.Collections;

public class HilightQuestChoice : CommandWithUndo
{

    private int index;
    public HilightQuestChoice(int i) : base()
    {
        index = i; 
    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<RumorBoardUI>();
        if (target is RumorBoardUI)
        {
            target.HilightQuestOption(index);
        }
        base.Execute(Bar);
    }
}
