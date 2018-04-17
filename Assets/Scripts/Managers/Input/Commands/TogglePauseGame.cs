using UnityEngine;
using System.Collections;

public class TogglePauseGame : CommandWithUndo
{

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<PauseManager>();

        if (target is PauseManager)
        {
            target.TogglePauseGame();
        }
        base.Execute(Bar);
    }
}
