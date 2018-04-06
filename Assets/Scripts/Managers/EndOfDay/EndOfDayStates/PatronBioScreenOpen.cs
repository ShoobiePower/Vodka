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
            Patron patronToInquireAbout = endOfDayManager.AllPatronsTheBartenderKnows[index + CurrentTopOfPage];
            patronName.text = patronToInquireAbout.Name;
            PatronBioText.text = patronToInquireAbout.Bio;
            CurrentSelection = index;
        }

        //TODO, unlock more as patron levelsUp;
    }

    public override void HidePresetAssets()
    {
        base.HidePresetAssets(allPropsForBioScreen);
    }
}
