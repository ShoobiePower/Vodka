using UnityEngine;
using System.Collections;

public class AcceptAdventure : CommandWithUndo
{



    public AcceptAdventure() : base()
    {
        
    }

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<MapManager>();
        if (target is MapManager)
        {
            //target. send guy out, close book, mark quest in book for trash. 
            target.EndPhase();
        }
        base.Execute(Bar);
    }
}