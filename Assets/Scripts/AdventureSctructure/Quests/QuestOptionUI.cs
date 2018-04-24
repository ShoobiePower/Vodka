using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestOptionUI : MonoBehaviour {

    [SerializeField]
    Text QuestName;

    [SerializeField]
    Text QuestDescription;

    private
    Color desiredColor;

    [SerializeField]
    Color defaultColor;

    [SerializeField]
    Color GrowColor;

    [SerializeField]
    Color ShrinkColor;


    [SerializeField]
    float TransitionSpeed;

    [SerializeField]
    float GrowSize;

    [SerializeField]
    float ShrinkSize;

    // When there is one quest option
    [SerializeField]
    RectTransform position0;

    // When there are two quest options.
    [SerializeField]
    RectTransform TransformOfDefaultPosition;

    private Vector3 DefaultPosition;

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
        DefaultSize = new Vector2(1, 1);
        this.gameObject.GetComponent<RectTransform>().localScale = DefaultSize;
        DesiredSize = DefaultSize;
        desiredColor = defaultColor;
    }

    public void Update()
    {
        this.gameObject.GetComponent<RectTransform>().localScale = Vector2.Lerp(this.gameObject.GetComponent<RectTransform>().localScale, DesiredSize,(TransitionSpeed * Time.deltaTime));
        this.gameObject.GetComponent<Image>().color = Vector4.Lerp(this.gameObject.GetComponent<Image>().color, desiredColor, (TransitionSpeed * Time.deltaTime));
    }

    public void activateQuestOption()
    {
        this.gameObject.SetActive(true);
    }

    public void deactivateQuestOption()
    {
        this.gameObject.SetActive(false);
    }


    public void labelQuestOption()
    {
        QuestName.text = QuestContainedInOption.QuestName;
        QuestDescription.text = QuestContainedInOption.QuestDescription;
    }

    public void GrowQuestOption()
    {
        growButtonSize();
        fadeToGrownColor();
    }

    private void growButtonSize()
    {
        DesiredSize = new Vector2(GrowSize, GrowSize);
    }

    private void fadeToGrownColor()
    {
        desiredColor = GrowColor;
    }

    public void ShrinkQuestOption()
    {
        shrinkButton();
        fadeToShrunkColor();
    }

    private void shrinkButton()
    {
        DesiredSize = new Vector2(ShrinkSize, ShrinkSize);
    }

    private void fadeToShrunkColor()
    {
        desiredColor = ShrinkColor;
    }

    public void MakeQuestOptionSizeNormal()
    {
        this.gameObject.GetComponent<RectTransform>().transform.localScale = DefaultSize;
        DesiredSize = DefaultSize;
        this.gameObject.GetComponent<Image>().color = defaultColor;
        desiredColor = defaultColor;
    }
    // This is only to be used by rumor option 0;
    // this moves the option to the center of the rumor board and should be the only time this moves
    public void CenterOption0()
    {
        this.transform.position = position0.transform.position;
    }

    public void UseDefaultPosition()
    {
        this.transform.position = TransformOfDefaultPosition.transform.position;
    }
}
