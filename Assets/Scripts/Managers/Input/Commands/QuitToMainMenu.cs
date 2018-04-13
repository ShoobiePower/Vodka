using UnityEngine;
using System.Collections;

public class QuitToMainMenu : CommandWithUndo
{

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<PauseManager>();
        if (target is PauseManager)
        {
            target.quitToMainMenu();
        }
        base.Execute(Bar);
    }
}

