using UnityEngine;
using System.Collections;

public class BackOutOfDeployChoice : CommandWithUndo
{

    public override void Execute(Colleague Bar)
    {
        var target = Bar.GetComponent<BarManager>();
        if (target is BarManager)
        {
            target.SelectedSeat.IsPatronGoesOnQuestDeciderActive(false);
            target.SelectedSeat.PatronWantsToTalkAboutWaitingInABar();
            //target.setBarState(target.pauseAtTheEndOfCancelAdventure());
            //target.setBarState(target.patronIsConversing());
             target.setBarState(target.noOneInteractedWith());
           // target.ClickPatron();
            //target.BackOutOfAdventureMap();
        }
        base.Execute(Bar);
    }

}
