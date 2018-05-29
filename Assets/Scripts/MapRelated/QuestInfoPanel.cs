using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestInfoPanel : MonoBehaviour
{
    [SerializeField] Text QuestNameTextBox;
    [SerializeField] Text QuestDescriptionTextBox;
    [SerializeField]
    Text DaysLeftOnQuest;
    [SerializeField]
    Text SkillsNeededForQuest;
    //[SerializeField] RectTransform QuestInfoRightArrow;
    //[SerializeField] RectTransform QuestInfoLeftArrow;
    [SerializeField]
    Button ChooseQuestButton;
    [SerializeField]
    QuestMenu questMenu;


    private Patron pendingPatron;
    Region targetLocation;
    byte questIndex = 0;

    public void InitializeQuestInfoPanel(Region _targetRegion, Patron patronToGoOnAdventure)
    {
        targetLocation = _targetRegion;

        pendingPatron = patronToGoOnAdventure;
        ChooseQuestButton.gameObject.SetActive(false);

        questMenu.activateQuestMenu();
        questMenu.formatButtonsForThisPage(targetLocation.QuestsAtLocation);

        questIndex = 0;
        displayQuestInfo(questIndex); 

    }

    public void ShowQuestInfoForRegion(Region _targetRegion) // new 
    {

        targetLocation = _targetRegion;
        ChooseQuestButton.gameObject.SetActive(false);

        questMenu.activateQuestMenu();
        questMenu.formatButtonsForThisPage(targetLocation.QuestsAtLocation);

        questIndex = 0;
        displayQuestInfo(questIndex);
    }

    public Quest GetQuestFromLocation()
    {
       return targetLocation.findQuestAtThisLocationByIndex(questIndex);   
    }

    public void displayInfoBasedOnMenuChoice(byte index)
    {
        Debug.Log("Base index: " +index);
        byte indexFromTopOfList = questMenu.getTopOfList(index);
        questIndex = index;
        displayQuestInfo(indexFromTopOfList);
    }

    private void displayQuestInfo(byte index)
    {

        if (targetLocation.QuestCountAtLocation == 0)
        {
            QuestNameTextBox.text = targetLocation.Name;

            QuestDescriptionTextBox.text = "There are no quests at the " + targetLocation.Name.ToLower();//"FPO: Description for " + targetLocation.Name + " Should be here";

            DaysLeftOnQuest.text = string.Empty;

            SkillsNeededForQuest.text = string.Empty;

            ChooseQuestButton.gameObject.SetActive(false);
        }
        else
        {
            Quest questInfoToDisplay = targetLocation.findQuestAtThisLocationByIndex(index);
            QuestNameTextBox.text = questInfoToDisplay.QuestName;
            QuestDescriptionTextBox.text = questInfoToDisplay.QuestDescription;
            DaysLeftOnQuest.text = "Days Left:" + questInfoToDisplay.DaysToCompleteQuest.ToString();
            SkillsNeededForQuest.text = listSkillsNeededForQuest(questInfoToDisplay);
            ChooseQuestButton.gameObject.SetActive(questInfoToDisplay.getQuestStatus() != Quest.questStatus.TAKEN);

        }

    }


    public void HideSelf() 
    {
        questMenu.deactivateQuestMenu();
        gameObject.SetActive(false);
    }

    public void DisplayLocationDescription()
    {

    }


    private string listSkillsNeededForQuest(Quest q)
    {
        string skillListToReturn = "Skills: ";

        for(int i = 0; i < q.TrialsOfTheQuest.Count; i++)
        {
            skillListToReturn += q.TrialsOfTheQuest[i].skillToCheckFor.ToString();
        }
        return skillListToReturn;
    }

}
