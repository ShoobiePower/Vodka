using UnityEngine;
using System.Collections;

public class Day7CorporealTransition : TutorialTask
{
    System.Action firstPatron;
    System.Action secondPatron;

    public Day7CorporealTransition(Tutorial _tutorial) : base(_tutorial)
    {
        TutorialReactions.Clear();
        OnDayStart();
    }

    void OnDayStart()
    {
        TutorialReactions.Clear();
        if (tutorial.GetPatron("Artie").QuestToCompleete == null)
        {
            tutorial.SetTimer(3f);
            firstPatron = EnterMavis;
            secondPatron = EnterGaius;
            TutorialReactions.Add(Mediator.ActionIdentifiers.COUNTDOWN_ENDED, EnterGaius);

        }

        else
        {
            if (tutorial.GetPatron("Artie").QuestToCompleete.QuestName == "Emphasize Caution")
            {
                firstPatron = EnterMavis;
                secondPatron = EnterGaius;
            }
            else
            {
                firstPatron = EnterGaius;
                secondPatron = EnterMavis;
            }
        }


        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, firstPatron);


    }

    void EnterGaius()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Gaius", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, SendPatronHome);

        if(secondPatron != EnterGaius)
        {
            TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, secondPatron);
        }
        else
        {
            TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterJim);
        }
    }

    void EnterMavis()
    {
        TutorialReactions.Clear();
        tutorial.forcePatronIntoBarToSitAt("Mavis", 1);
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.ADVENTURE);
        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, SendPatronHome);

        if (secondPatron != EnterMavis)
        {
            TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, secondPatron);
        }
        else
        {
            TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EnterJim);
        }
    }

    void EnterJim()
    {
        TutorialReactions.Clear();
        tutorial.invokeJimAtSeatNumber(1);
        if (firstPatron == EnterGaius)
        {
            tutorial.forceSeatToHaveSpecificConversation(1, "Day7TeaserCollege");
        }
        else
        {
            tutorial.forceSeatToHaveSpecificConversation(1, "Day7TeaserCorporeal");
        }

        TutorialReactions.Add(Mediator.ActionIdentifiers.CONVERSATION_ENDED, SendPatronHome);
        TutorialReactions.Add(Mediator.ActionIdentifiers.PATRON_LEFT, EndDay);
    }

    void EndDay()
    {
        TutorialReactions.Clear();
        tutorial.forceEndOfDay();
        TutorialReactions.Add(Mediator.ActionIdentifiers.END_DAY_FADE_OUT, ExitTutorial);
    }



    void SendPatronHome()
    {
        tutorial.forceSeatToHaveSpecificJob(1, Patron.whatDoTheyWantToDo.GOHOME);
    }

    void ExitTutorial()
    {
        tutorial.endTutorial();
    }
}
