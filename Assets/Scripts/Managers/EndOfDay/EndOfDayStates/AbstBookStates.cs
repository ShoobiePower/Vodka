using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class AbstBookStates : MonoBehaviour, IEndOfDayStates
{
    protected EndOfDayManager endOfDayManager;

    protected Button[] menuButtons;
    protected Image[] menuTokens;

     private int numberOfActiveButtons;
     protected int NumberOfActiveButtons { get { return numberOfActiveButtons; } }


    // It works but I want to finda better way, static anything makes me weary.
    static private byte currentSelection = 0;
    protected byte CurrentSelection { get { return currentSelection; } set { currentSelection = value; } }

    static private byte currentTopOfPage = 0;
    protected byte CurrentTopOfPage { get { return currentTopOfPage; } set { currentTopOfPage = value; } }

    static private byte currentBottomOfPage = 0;
    protected byte CurrentBottomOfPage { get { return currentBottomOfPage; } set { currentBottomOfPage = value; } }


    public virtual void ShowPresetAssets(GameObject propsToActivate)
    {
        propsToActivate.SetActive(true);
        formatButtonsForThisPage();
        currentBottomOfPage = (byte)(currentTopOfPage + menuButtons.Length);
        ShowStatsOnPage(currentSelection);
    }

    public virtual void ShowPresetAssets()
    {
        Debug.Log("Error, fall through"); 
    }

    public void HidePresetAssets(GameObject propsToDeactivate)
    {
        propsToDeactivate.SetActive(false);
    }

    public void decideOnHowManyButtonsToShow(int sizeOfList)
    {
        setAllButtonsInactive();
        numberOfActiveButtons = (byte)menuButtons.Length;
        if (sizeOfList < menuButtons.Length)
        {
            numberOfActiveButtons = (byte)sizeOfList;
        }

        for (int i = 0; i < numberOfActiveButtons; i++)
        {
            menuButtons[i].gameObject.SetActive(true);
        }
    }

    public virtual void formatButtonsForThisPage()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].GetComponentInChildren<Text>().text = endOfDayManager.AllPatronsTheBartenderKnows[i + currentTopOfPage].Name;
            menuTokens[i].GetComponent<Image>().sprite = ApperanceManager.instance.ThisPatronsToken(endOfDayManager.AllPatronsTheBartenderKnows[i + currentTopOfPage].ID);
        }
    }

    public virtual void passRefrenceToEndOfDayManager(EndOfDayManager endOfDayManager)
    {
       
        this.endOfDayManager = endOfDayManager;
    }

    // could pass in a number, rather than having this be a virtual overriden by just one class.
    public virtual void ScrollDown()
    {


        if (currentBottomOfPage < endOfDayManager.AllPatronsTheBartenderKnows.Count)
        {
            currentTopOfPage++;
            currentBottomOfPage++;
            formatButtonsForThisPage();
        }
    }

    public virtual void ScrollUp()
    {

        if (currentTopOfPage > 0)
        {
            currentTopOfPage--;
            currentBottomOfPage--;
            formatButtonsForThisPage();
        }
    }

    public virtual void ShowStatsOnPage(byte index)
    {
        Debug.Log("Error, fall through on Show Stats page");
    }

    public virtual void HidePresetAssets()
    {
        Debug.Log("Error, fall through on Hide presetAssests");
    }

    public void setButtonArray(Button[] buttons)
    {
        menuButtons = buttons;
    }

    public void setTokenArray(Image[] buttons)
    {
        menuTokens = buttons;
    }

    private void setAllButtonsInactive()
    {
        for(int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].gameObject.SetActive(false);
        }
    }
}
