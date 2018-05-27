using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;

public class BattleReportManager : Colleague
{

    [Tooltip("The Panel for our battle report page")]
    [SerializeField]
    Image BattleReportPannel;

    [Tooltip("The paper scroll background for our battle report page")]
    [SerializeField]
    Image BattleReportBackground;

    [Tooltip("Text for the title of our quest")]
    [SerializeField]
    Text QuestTitle;

    [Tooltip("The Description for our quest on both pages, either the description for the quest or for the person who compleeted it")]
    [SerializeField]
    Text DescriptionOfQuest;



    #region Props For Report page

    // The props for the Mission Report page of the battle report

    [Tooltip("The game object that holds all of the props so I don't have to activate/deactivate each one")]
    [SerializeField]
    GameObject PropsForReportPage;

    [Tooltip("The list of skills our adventurer has")]
    [SerializeField]
    Text AdventurerSkillList;

    [Tooltip("A list of required skills the quest has")]
    [SerializeField]
    Text QuestSkillCheckList;

    [Tooltip("The token for our patron in the battle report for the mission report page")]
    [SerializeField]
    Image BattleReportPatronToken;

    [Tooltip("our arrow button that switches us to the next page")]
    [SerializeField]
    Button nextPageButton;

    [SerializeField]
    BondSlider bondSlider;

    [SerializeField]
    Text DrinkSkill;

    [SerializeField]
    Text bondGainText;

    #endregion

    #region Props For Summary

    // The props needed for our summary page
    [Tooltip("The game object that holds all of our props for the summary page")]
    [SerializeField]
    GameObject BattleReportSummaryPageProps;

    [SerializeField]
    Text PatronNameOnBattleReport;

    [SerializeField]
    Text PatronBondLevelOnBattleReport;

    [SerializeField]
    Text BattleReportRewardsList;

    [SerializeField]
    Image BattleReportPatronTokenSummaryPage;

    #endregion


    private List<StoreableItem> rewardsToRedeem = new List<StoreableItem>();
    public List<StoreableItem> RewardsToRedeem { get { return rewardsToRedeem; } }
    private List<Unlocker> unlockersToRedeem = new List<Unlocker>();
    public List<Unlocker> UnlockersToRedeem { get { return unlockersToRedeem; } }
    public enum ReportPages { REPORT, LEVELUP, SUMMARY, CLOSED };
    public ReportPages nextPageToTurnTo;
    private Quest questToReportOn;
    private Patron patronCarryingReport;


    public void ReadBattleReport(Patron patronCarryingPaystub)
    {
        BattleReportPannel.gameObject.SetActive(true);
        nextPageToTurnTo = ReportPages.REPORT;
        patronCarryingReport = patronCarryingPaystub;
        questToReportOn = patronCarryingPaystub.QuestToCompleete;
        checkForUnlockables(patronCarryingPaystub.QuestToCompleete);
       // bondSlider.SetSliderDefautValue(patronCarryingPaystub.BondPoints, patronCarryingReport.ThresholdToNextBondLevel); // HACK
        flipPageInBattleReport();
    }

    public void flipPageInBattleReport()
    {
        closePages();
        switch (nextPageToTurnTo)
        {
            case ReportPages.REPORT:
                {
                    flipToReportPage();
                    break;
                }

            case ReportPages.LEVELUP:
                {
                    FlipToLevelUp();
                    break;
                }

            case ReportPages.SUMMARY:
                {
                    FlipToSummaryPage();
                    break;
                }

            case ReportPages.CLOSED:
                {
                    EndPhase();
                    break;
                }
        }
    }

    private void flipToReportPage()
    {
        PropsForReportPage.SetActive(true);
        nextPageToTurnTo = ReportPages.SUMMARY;
        //cashBond(questToReportOn.TrialsOfTheQuest);
        nextPageButton.gameObject.SetActive(true);
        QuestTitle.text = questToReportOn.QuestName;
        DescriptionOfQuest.gameObject.SetActive(false);
        AdventurerSkillList.text = patronCarryingReport.convertSkillsListToString();
        QuestSkillCheckList.text = questToReportOn.convertChecksToString();
        BattleReportPatronToken.sprite = ApperanceManager.instance.ThisPatronsToken(patronCarryingReport.ID);
        checkIfBondSliderNeedsToMove();
        DrinkSkill.text = patronCarryingReport.SkillGrantedByDrink.ToString();
        
    }

    private void FlipToLevelUp()
    {
        nextPageToTurnTo = ReportPages.SUMMARY;
        DescriptionOfQuest.gameObject.SetActive(true);
        QuestTitle.text = "Level Up";
        DescriptionOfQuest.text = patronCarryingReport.Name + " has leveled up!";
        if (patronCarryingReport.HasAnyMoreSkills())
        {
            Debug.Log("level up text hit");
            DescriptionOfQuest.text += " \n They have gained the new attribute " + patronCarryingReport.PatronSkills[patronCarryingReport.Level - 1] + "!";
        }
        Director.RecordPatronLevelUp(patronCarryingReport);
    }

    private void FlipToSummaryPage()
    {
        BattleReportSummaryPageProps.SetActive(true);
        nextPageToTurnTo = ReportPages.CLOSED;
        nextPageButton.gameObject.SetActive(false);
        DescriptionOfQuest.gameObject.SetActive(true);
        PatronNameOnBattleReport.text = patronCarryingReport.Name;
        QuestTitle.text = questToReportOn.QuestName;
        DescriptionOfQuest.text = questToReportOn.getPersonalResponceForQuest(patronCarryingReport.ID);
        PatronBondLevelOnBattleReport.text = patronCarryingReport.Level.ToString();
        BattleReportRewardsList.text = listUnlockables();
        BattleReportPatronTokenSummaryPage.sprite = ApperanceManager.instance.ThisPatronsToken(patronCarryingReport.ID);
    }

    private void closePages()
    {
        PropsForReportPage.SetActive(false);
        BattleReportSummaryPageProps.SetActive(false);
    }

    // might need to incorporate this with the bar slider. 
    private void cashBond(List<Check> checksToCash)
    {
        if (!patronCarryingReport.IsMaxLevel())
        {
            foreach (Check c in checksToCash)
            {

                if (c.IsTrialPassed)
                {
                    patronCarryingReport.BondGainedOnThisQuest = 5;
                }
                else
                {
                    patronCarryingReport.BondGainedOnThisQuest = 0;
                }
                patronCarryingReport.convertGainedBondToTotalBond();
                if (patronCarryingReport.HasLeveledUp)
                {
                    nextPageToTurnTo = ReportPages.LEVELUP;
                    if (patronCarryingReport.IsMaxLevel())
                    {
                        bondSlider.IsPatronAtMaxLevel = true;
                    }
                }
            }
        } 
    }

    private void showExpGain()
    {
        bondSlider.cashNextCheck();
    }

    private string formatText(string textToFormat)  // TODO give public accessTo designer to change how many characters before cut off. And make this a util Tool;
    {
        StringBuilder strBuilder = new StringBuilder(textToFormat);
        int i = 0;
        for (int j = 0; j < textToFormat.Length; j++)
        {
            if (i >= 50 && strBuilder[j] == ' ')
            {
                strBuilder[j] = '\n';
                i = 0;
            }
            i++;
        }
        strBuilder.Replace("{NAME}", patronCarryingReport.Name);
        textToFormat = strBuilder.ToString();
        return textToFormat;
    }

    private void checkForUnlockables(Quest compleetedQuest)
    {
        Unlocker.WhenToUnlock unlockState = Unlocker.WhenToUnlock.LOSE;

        if (compleetedQuest.getQuestStatus() == Quest.questStatus.PASS)
        {
            unlockState = Unlocker.WhenToUnlock.WIN;
        }

        for (int i = 0; i < compleetedQuest.ThingsToUnlock.Count; i++) //each (Unlocker u in compleetedQuest)
        {
            Debug.Log("Quest Unlocker:" + compleetedQuest.ThingsToUnlock[i].WhenIsThisUnlocked + " Circumstance:" + unlockState);
            if (compleetedQuest.ThingsToUnlock[i].WhenIsThisUnlocked == unlockState || compleetedQuest.ThingsToUnlock[i].WhenIsThisUnlocked == Unlocker.WhenToUnlock.WINORLOSE)
            {
                Debug.Log("We just unlocked" + compleetedQuest.ThingsToUnlock[i].NameOfThingToUnlock);
                unlockersToRedeem.Add(compleetedQuest.ThingsToUnlock[i]);
            }
        }
    }

    private string listUnlockables()
    {
        string stringToReturn = string.Empty;
        for (int i = 0; i < unlockersToRedeem.Count; i++)
        {
            switch (unlockersToRedeem[i].WhatDoesThisUnlock)
            {
                case Unlocker.WhatUnlocks.INSTANTRUMOR:
                    {
                        stringToReturn += "RUMOR : " + unlockersToRedeem[i].NameOfThingToUnlock + "\n";
                        break;
                    }
                case Unlocker.WhatUnlocks.RUMOR:
                    {
                        stringToReturn += unlockersToRedeem[i].WhatDoesThisUnlock + " : " + unlockersToRedeem[i].SpecifierForNameOfThingToUnlock + "\n";
                        break;
                    }
                case Unlocker.WhatUnlocks.LOCATION:
                    {
                        stringToReturn += unlockersToRedeem[i].WhatDoesThisUnlock + " : " + unlockersToRedeem[i].NameOfThingToUnlock + "\n";
                        break;
                    }

                case Unlocker.WhatUnlocks.PATRON:
                    {
                        stringToReturn += unlockersToRedeem[i].WhatDoesThisUnlock + " : " + unlockersToRedeem[i].NameOfThingToUnlock + "\n";
                        break;
                    }

            }
        }
        return stringToReturn;
    }

    public void clearUnlockLists()
    {
        rewardsToRedeem.Clear();
        unlockersToRedeem.Clear();
    }

    private void checkIfBondSliderNeedsToMove()
    {
        bondSlider.LoadListOfChecks(questToReportOn.TrialsOfTheQuest);

        if (!patronCarryingReport.IsMaxLevel())
        {
            bondSlider.SetSliderDefautValue(patronCarryingReport.BondPoints, patronCarryingReport.ThresholdToNextBondLevel);
            cashBond(questToReportOn.TrialsOfTheQuest);
            showExpGain();
            //cashBond(questToReportOn.TrialsOfTheQuest);
        }
        else
        {
            bondSlider.SetSliderDefautValue(patronCarryingReport.ThresholdToNextBondLevel, patronCarryingReport.ThresholdToNextBondLevel);
            bondGainText.text = "Bond Has Reached Max Level!";
        }
    }

    public override void EndPhase()
    {
        BattleReportPannel.gameObject.SetActive(false);
        Director.EndPhase(this);
    }

}


