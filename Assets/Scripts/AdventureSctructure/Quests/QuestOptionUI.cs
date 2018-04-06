using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestOptionUI : MonoBehaviour {

    [SerializeField]
    Text QuestName;

    [SerializeField]
    Image[] RewardIcons;

    [SerializeField]
    Text QuestDescription;


    Quest questContainedInOption;
    public Quest QuestContainedInOption
    {
        get { return questContainedInOption; }

        set
        {
            questContainedInOption = value;
            activateQuestOption();
            labelQuestOption();
        }
    }

    public void activateQuestOption()
    {
        this.gameObject.SetActive(true);
    }

    public void deactivateQuestOption()
    {
        this.gameObject.SetActive(false);
    }

    private void activateRewardIcon(byte rewardIconToActivate)
    {
        RewardIcons[rewardIconToActivate].gameObject.SetActive(true);
    }

    private void deactivateRewardIcon(byte rewardIconToDeactivate)
    {
        RewardIcons[rewardIconToDeactivate].gameObject.SetActive(false);
    }

    private void deactivateAllRewardIcons()
    {
        for (byte i = 0; i < RewardIcons.Length; i++)
        {
            deactivateRewardIcon(i);
        }
    }

    //private void labelRewardIcon(byte whichImageToLable ,StoreableItem itemToDesplay)
    //{
    //    RewardIcons[whichImageToLable].sprite = itemToDesplay.displayArt();
    //}

    public void labelQuestOption()
    {
        deactivateAllRewardIcons();

        QuestName.text = QuestContainedInOption.QuestName;
        QuestDescription.text = QuestContainedInOption.QuestDescription;
        //for(byte i = 0; i < QuestContainedInOption.RewardsFromQuest.Count; i++)
        //{
        //    activateRewardIcon(i);
        //    labelRewardIcon(i, QuestContainedInOption.RewardsFromQuest[i]);
        //}
    }
}
