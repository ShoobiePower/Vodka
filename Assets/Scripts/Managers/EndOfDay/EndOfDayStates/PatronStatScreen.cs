using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PatronStatScreen : AbstBookStates {


    [SerializeField]
    GameObject AllPropsForPatronStatScreen;

    [SerializeField]
    Text patronName;

    [SerializeField]
    Image patronImage;

    [SerializeField]
    Text[] patronSkillsTextFields;

    [SerializeField]
    Slider PatronBondMeter;

    [SerializeField]
    Text PatronLvlBondText;

    public override void ShowPresetAssets()
    {
        decideOnHowManyButtonsToShow(endOfDayManager.AllPatronsTheBartenderKnows.Count);
        base.ShowPresetAssets(AllPropsForPatronStatScreen);
    }

    public override void ShowStatsOnPage(byte index)
    {

        if (NumberOfActiveButtons > 0)
        {
            areAllPropsActiveForCurrentPage(AllPropsForPatronStatScreen.transform, true);
            Patron patronToInquireAbout = endOfDayManager.AllPatronsTheBartenderKnows[index + CurrentTopOfPage];
            patronName.text = patronToInquireAbout.Name;
            checkToDisplayBondLevelInfo(patronToInquireAbout);
            patronImage.sprite = ApperanceManager.instance.HowThisPatronLooks(patronToInquireAbout.Name); 
            writeOutPatronSkills(patronToInquireAbout);
            CurrentSelection = index;
        }
        else
        {
            areAllPropsActiveForCurrentPage(AllPropsForPatronStatScreen.transform, false);
            patronName.gameObject.SetActive(true);
            patronName.text = "You do not know any patrons. Come back once you talked to a few.";
        }
    }

    private void writeOutPatronSkills(Patron patronToInquireAbout)
    {
        setAllSkillsBlank();

        for (int i = 0; i <= patronToInquireAbout.PatronSkills.Count -1; i++)
        { 
            patronSkillsTextFields[i].text = patronToInquireAbout.PatronSkills[i].ToString();
        }
    }

    private void setAllSkillsBlank()
    {
        for (int i = 0; i < patronSkillsTextFields.Length; i++)
        {
            patronSkillsTextFields[i].text = "???";
        }
    }

    public override void HidePresetAssets()
    {
        patronName.gameObject.SetActive(false);
        base.HidePresetAssets(AllPropsForPatronStatScreen);
    }

    private void checkToDisplayBondLevelInfo(Patron patronToInquireAbout)
    {
        if (patronToInquireAbout.IsMaxLevel())
        {
            PatronLvlBondText.text = "Bond Level: Max";
            PatronBondMeter.maxValue = patronToInquireAbout.ThresholdToNextBondLevel;
            PatronBondMeter.value = patronToInquireAbout.ThresholdToNextBondLevel; ;
        }
        else
        {
            PatronLvlBondText.text = "Bond Level: " + patronToInquireAbout.Level + "\n";
            PatronLvlBondText.text += "Points till next level " + (patronToInquireAbout.ThresholdToNextBondLevel - patronToInquireAbout.BondPoints);
            PatronBondMeter.maxValue = patronToInquireAbout.ThresholdToNextBondLevel;
            PatronBondMeter.value = patronToInquireAbout.BondPoints;
        }
    }

}
