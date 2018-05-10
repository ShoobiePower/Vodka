using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class QuestPageOpen : AbstBookStates
{

    [SerializeField]
    GameObject propsForQuestPage;

    [SerializeField]
    Button statButton;

    [SerializeField]
    Button bioButton;

    [SerializeField]
    Text RumorStoryText;

    [SerializeField]
    Text QuestOptionText;

    [SerializeField]
    Text ResolutionText;


    public override void ShowPresetAssets()
    {
        decideOnHowManyButtonsToShow(endOfDayManager.QuestArchive.Count);
        CurrentSelection = 0;
        CurrentTopOfPage = 0;
        base.ShowPresetAssets(propsForQuestPage);
        statButton.enabled = false;
        bioButton.gameObject.SetActive(false);
        
    }

    public override void formatButtonsForThisPage()
    {
        //{ TODO: get the patron who is going on the quest to have their icon appear next to it.
        //    menuButtons[i].GetComponentInChildren<Image>().sprite = ApperanceManager.instance.ThisPatronsToken(endOfDayManager.AllPatronsTheBartenderKnows[i].ID);
        int i = CurrentTopOfPage; 
        foreach (Button menuButton in menuButtons)
        {
            if (menuButton.isActiveAndEnabled)
            {
                menuButtons[i - CurrentTopOfPage].GetComponentInChildren<Text>().text = endOfDayManager.QuestArchive[i].QuestName;
                menuTokens[i - CurrentTopOfPage].GetComponentInChildren<Image>().sprite = ApperanceManager.instance.getCoinImage();
                i++;
            }
        }
    }

    public override void HidePresetAssets()
    {
        base.HidePresetAssets(propsForQuestPage);
        statButton.enabled = true;
        bioButton.gameObject.SetActive(true);
        CurrentSelection = 0;
        CurrentTopOfPage = 0;
    }

    public override void ScrollDown()
    {
        Debug.Log("Press down");
        if (CurrentBottomOfPage < endOfDayManager.QuestArchive.Count)
        {
            CurrentTopOfPage++;
            CurrentBottomOfPage++;
            formatButtonsForThisPage();
        }
    
}

    public override void ScrollUp()
    {
        Debug.Log("Press up");
        if (CurrentTopOfPage > 0)
        {
            CurrentTopOfPage--;
            CurrentBottomOfPage--;
            formatButtonsForThisPage();
        }
    }

    public override void ShowStatsOnPage(byte index)
    {
        if (NumberOfActiveButtons > 0)
        {
            // Also to do, note if quest is taken and if so by whom. 
            RumorStoryText.text = endOfDayManager.QuestArchive[index + CurrentTopOfPage].RumorStory;

            QuestOptionText.text = endOfDayManager.QuestArchive[index + CurrentTopOfPage].QuestDescription;

            switch (endOfDayManager.QuestArchive[index + CurrentTopOfPage].getQuestStatus())
            {
                case Quest.questStatus.TAKEN:
                    {
                        //TODO, get the patron taking the quest to display name.
                        ResolutionText.text = "Quest in progress";
                        break;
                    }

                case Quest.questStatus.PENDING:
                    {
                        ResolutionText.text = string.Empty;
                        break;
                    }

                case Quest.questStatus.PASS:
                    {
                        ResolutionText.text = endOfDayManager.QuestArchive[index + CurrentTopOfPage].Resolution;
                        break;
                    }
            }
        }

        else
        {
            RumorStoryText.text = "You Currently do not have any quests to read about!";
            QuestOptionText.text = string.Empty;
            ResolutionText.text = string.Empty;
        }

        

    }

}
