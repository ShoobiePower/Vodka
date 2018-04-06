using UnityEngine;
using System.Collections;

public class OpenEndOfDayMap : CommandWithUndo
{



    public OpenEndOfDayMap() : base()
    {

    }

    public override void Execute(Colleague Bar)
    {
    var target = Bar.GetComponent<EndOfDayManager>();
      if (target is EndOfDayManager)
      {
            target.openEndOfDayMap();
      }
    base.Execute(Bar);
    }
}
