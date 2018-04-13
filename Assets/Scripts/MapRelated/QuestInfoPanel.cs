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
        //QuestInfoRightArrow.gameObject.SetActive(false);
        //QuestInfoLeftArrow.gameObject.SetActive(false);
        ChooseQuestButton.gameObject.SetActive(false);

        questMenu.activateQuestMenu();
        questMenu.formatButtonsForThisPage(targetLocation.QuestsAtLocation);

        //if (targetLocation.QuestCountAtLocation > 0)          //If there is at least one quest at this location...
        //{
        //    questIndex = 1;
        //}
        //else
        //{
        //    questIndex = 0;
        //}
        questIndex = 0;
        displayQuestInfo(questIndex); // HERE 2/12
        //UpdateQuestSelectArrows();

        //pull up a panel, showing all the quests at this location
        //Most of the info about the quest should be displayed in this panel.  Maybe it appears to the right of the screen?
        //Should display name, rewards, description, tasks, and faction alignment
        //      /\ AKA: this could be an exact copy of the card used when choosing the quest in the rumor mill /\

        //When the player clicks on a quest, 'highlightedQuest' is set equal to that quest
        //When the player clicks 'Add Quest,' the quest is added 
    }

    public Quest GetQuestFromLocation()
    {
       return targetLocation.findQuestAtThisLocationByIndex(questIndex);   //Index 0 is "no quest", but the list of quests at the location starts counting at 0.  So, subtract one.  There's probably a more intuitive way of doing this
    }

    //void UpdateQuestSelectArrows()
    //{
    //    QuestInfoLeftArrow.gameObject.SetActive(questIndex - 1 >= 0);                                       //set the left arrow active if there is at least one more element (previous)
    //    QuestInfoRightArrow.gameObject.SetActive(questIndex + 1 <= targetLocation.QuestCountAtLocation);    //set the right arrow active if there is at least one more elemnt in the list of quests (+1 slot for "no quest")
    //}

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
