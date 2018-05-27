using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PatronBioScreenOpen : AbstBookStates
{

    [SerializeField]
    GameObject allPropsForBioScreen;

    [SerializeField]
    Text patronName;

    [SerializeField]
    Text PatronBioText;

    public override void ShowPresetAssets()
    {
        decideOnHowManyButtonsToShow(endOfDayManager.AllPatronsTheBartenderKnows.Count);
        base.ShowPresetAssets(allPropsForBioScreen);
    }

    public override void passRefrenceToEndOfDayManager(EndOfDayManager endOfDayManager)
    {
        base.passRefrenceToEndOfDayManager(endOfDayManager);
    }

    public override void ShowStatsOnPage(byte index)
    {
        if (NumberOfActiveButtons > 0)
        {
            areAllPropsActiveForCurrentPage(allPropsForBioScreen.transform,true);
            Patron patronToInquireAbout = endOfDayManager.AllPatronsTheBartenderKnows[index + CurrentTopOfPage];
            patronName.text = patronToInquireAbout.Name;
            PatronBioText.text = patronToInquireAbout.Bio;
            CurrentSelection = index;
        }
        else
        {
            areAllPropsActiveForCurrentPage(allPropsForBioScreen.transform, false);
            patronName.gameObject.SetActive(true);
            patronName.text = "You do not know any patrons. Come back once you talked to a few.";
        }

        //TODO, unlock more as patron levelsUp;
    }

    public override void HidePresetAssets()
    {
        base.HidePresetAssets(allPropsForBioScreen);
    }

}
