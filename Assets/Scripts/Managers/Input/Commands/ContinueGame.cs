using UnityEngine;
using System.Collections;

public class ContinueGame : CommandWithUndo
{

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<PauseManager>();
        if (target is PauseManager)
        {
           // target.resumeGame();
        }
        base.Execute(Bar);
    }
}

