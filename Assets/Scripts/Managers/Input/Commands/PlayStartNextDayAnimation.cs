using UnityEngine;
using System.Collections;

public class PlayStartNextDayAnimation : CommandWithUndo
{


public PlayStartNextDayAnimation() : base()
    {
   
    }
  
public override void Execute(Colleague Bar)
{
    var target = Bar.GetComponent<EndOfDaySummaryManager>();

    if (target is EndOfDaySummaryManager)
    {
       target.EndPhase();
    }
    base.Execute(Bar);
}
}