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

    [SerializeField]
    RectTransform GrownSize;

    [SerializeField]
    RectTransform ShrunkSize;

    [SerializeField]
    float TransitionSpeed;

    private Vector2 DefaultSize;

    private Vector2 DesiredSize;


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

    private void Start()
    {
        DefaultSize = this.gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    public void Update()
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(this.gameObject.GetComponent<RectTransform>().sizeDelta, DesiredSize,TransitionSpeed);
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

    public void labelQuestOption()
    {
        deactivateAllRewardIcons();

        QuestName.text = QuestContainedInOption.QuestName;
        QuestDescription.text = QuestContainedInOption.QuestDescription;
    }

    public void GrowQuestOption()
    {
        DesiredSize = GrownSize.sizeDelta;
    }

    public void ShrinkQuestOption()
    {
        DesiredSize = ShrunkSize.sizeDelta;
    }

    public void MakeQuestOptionSizeNormal()
    {
        DesiredSize = DefaultSize;
    }
}
