using UnityEngine;
using System.Collections;

public class SwapEndOfDayStateToPatronBio : CommandWithUndo
{



    public SwapEndOfDayStateToPatronBio() : base()
    {

    }

public override void Execute(Colleague director)
{
    var target = director.GetComponent<EndOfDayManager>();
    if (target is EndOfDayManager)
    {
            target.FlipToPatronBioPage();
    }
    base.Execute(director);
}
}
