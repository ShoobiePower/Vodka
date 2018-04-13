using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class FadingText : MonoBehaviour
{ // I want this to be a fadable object too, I jsut an not sure how to get an image to act like a sprite renderer or have fadable object call one or the other. 

    public Text dioToFade;
    public Text nameToFade;
    public float howLongIsThisFade;
    protected float fadeCountdown;
    private Color FullHereColorForBox;
    private Color FullHereColorForText;

    [SerializeField]
    float howLongBeforeNextCharacter;

    [SerializeField]
    Image conversationMarker;

    private bool isShouldFade;
    private bool isTextAnimating;
    private float speedOfTextCountdown;
    private string dioToSpellOut;
    private int currentCharacterIndex;

    private void Start()
    {
        //textToFade = this.gameObject.GetComponentInChildren<Text>();
        FullHereColorForText = dioToFade.color;
    }


    // Update is called once per frame
    void Update()
    {
        if (isShouldFade) { runFadeAnimation(); }
        if (isTextAnimating) { spellOutText(); }
    }

    public virtual void sendWhatToSay(string patronsName, string dioOut)
    {
        dioToFade.color = FullHereColorForText;
        nameToFade.color = FullHereColorForText;
        this.gameObject.SetActive(true);
        isTextAnimating = true;
        dioToSpellOut = dioOut;
        currentCharacterIndex = 0;
        dioToFade.text = string.Empty;
        //dioToFade.text = dioOut;  // Format text
        nameToFade.text = patronsName;
        fadeCountdown = howLongIsThisFade;
        isShouldFade = false;
    }

    public void cutOff(float speedOfSeatFade)
    {
        isShouldFade = true;
        fadeCountdown = speedOfSeatFade;
    }


    private void runFadeAnimation()
    {
        dioToFade.color -= new Color(0, 0, 0, FullHereColorForText.a / fadeCountdown) * Time.deltaTime;
        nameToFade.color -= new Color(0, 0, 0, FullHereColorForText.a / fadeCountdown) * Time.deltaTime;

    }

    // Make conversation marker appear
    public void activateConversationMarker()
    {
        conversationMarker.gameObject.SetActive(true);
        showArrow();
    }

    public void showArrow()
    {

    }

    // Show dot when conversation is at it's end
    public void showDot()
    {

    }

    public void deactivateConversationMarker()
    {
        conversationMarker.gameObject.SetActive(false);
    }

    private void reloadTimer()
    {
        speedOfTextCountdown = howLongBeforeNextCharacter;
    }

    private void spellOutText()
    {
        speedOfTextCountdown -= Time.deltaTime;
        if (speedOfTextCountdown <= 0)
        {
            addNextCharacter();
            reloadTimer();
        }
        
    }


    string hypertextClose = "";

    private void addNextCharacter()
    {
        if (dioToSpellOut[currentCharacterIndex] == '<')
        {
            if (hypertextClose != "") //if we're currently typing color
            {
                //We want to stop typing color
                currentCharacterIndex = GetNextClosingAngleBracketIndex(currentCharacterIndex);
                hypertextClose = "";
            }
            else //we're not typing out colored text and we now want to
            {
                currentCharacterIndex = GetNextClosingAngleBracketIndex(currentCharacterIndex);
                int hypertextCloseStartIndex = GetNextOpeningAngleBracketIndex(currentCharacterIndex);
                int hypertextCloseLength = GetNextClosingAngleBracketIndex(hypertextCloseStartIndex) - hypertextCloseStartIndex + 1;
                hypertextClose = dioToSpellOut.Substring(hypertextCloseStartIndex, hypertextCloseLength);
            }
        }

        currentCharacterIndex++;

        dioToFade.text = dioToSpellOut.Substring(0, currentCharacterIndex) + hypertextClose;

        if (currentCharacterIndex == dioToSpellOut.Length)
        {
            isTextAnimating = false;
        }
    }

    private int GetNextClosingAngleBracketIndex(int currentIndex)
    {
        return dioToSpellOut.IndexOf('>', currentIndex);
    }

    private int GetNextOpeningAngleBracketIndex(int currentIndex)
    {
        return dioToSpellOut.IndexOf('<', currentIndex);
    }

    //private void addNextCharacter()
    //{
    //    dioToFade.text += dioToSpellOut[currentCharacterIndex];
    //    currentCharacterIndex++;
    //    if (currentCharacterIndex == dioToSpellOut.Length)
    //    {
    //        isTextAnimating = false;
    //    }
    //}



    //private string formatText(string textToFormat)  // TODO give public accessTo designer to change how many characters before cut off. // make this a util tool too!
    //{
    //    StringBuilder strBuilder = new StringBuilder(textToFormat);
    //    int i = 0;
    //    for (int j = 0; j < textToFormat.Length; j++)
    //    {
    //        if (i >= charactersBeforeBreak && strBuilder[j] == ' ')
    //        {
    //            strBuilder[j] = '\n';
    //            i = 0;
    //        }
    //        i++;
    //    }
    //    textToFormat = strBuilder.ToString();
    //    return textToFormat;
    //}
}
