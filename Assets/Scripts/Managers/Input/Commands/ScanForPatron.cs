using UnityEngine;
using System.Collections;

public class ScanForPatron : CommandWithUndo
{

    
    public ScanForPatron() : base()
    {
        
    }

    public override void Execute(Colleague Bar)
    {

        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.ClickPatron();
        }
        base.Execute(Bar);
    }
}