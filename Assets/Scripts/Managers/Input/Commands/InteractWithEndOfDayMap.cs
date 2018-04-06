using UnityEngine;
using System.Collections;

public class InteractWithEndOfDayMap : CommandWithUndo
{



    public InteractWithEndOfDayMap() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {   
    var target = Bar.GetComponent<EndOfDayManager>();
      if (target is EndOfDayManager)
      {
            target.swapToMapOpenScreen();
      }
    base.Execute(Bar);
    }

}
