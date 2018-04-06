using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TimeOfDayPanel : MonoBehaviour
{  

    [SerializeField]
    float speedOfFade;
    public float SpeedOfFade { get { return speedOfFade; } }

    private Color fullNightimeColor;
    public Color FullNightimeColor { get { return fullNightimeColor; } }

    private Color fullTextColor;
    public Color FullTextColor { get { return fullTextColor; } }

    private Color blankTextColor;
    public Color BlankTextColor { get { return blankTextColor; } }

    Text calendar;
    public Text Calendar { get { return calendar; } }


    private void Start()
    {
        this.gameObject.GetComponent<Image>().color = new Color(fullNightimeColor.r, fullNightimeColor.g, fullNightimeColor.b, 0);
        implementCalandarText();
    }

    private void implementCalandarText()
    {
        calendar = this.gameObject.GetComponentInChildren<Text>();
        fullTextColor = calendar.color;
        blankTextColor = new Color(FullTextColor.r, FullTextColor.g, FullTextColor.b, 0);
        calendar.color = BlankTextColor;
    }

    public void setDateText(string dateText)
    {
        if (calendar == null)
        {
            implementCalandarText();
        }

        calendar.text = dateText;
    }

 
}