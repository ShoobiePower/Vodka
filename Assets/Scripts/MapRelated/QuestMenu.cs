using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestMenu : MonoBehaviour{  // monobehaviour

    [SerializeField]
    Button[] menuChoices;

    [SerializeField]
    Button quitOutButton;

    private int numberOfActiveButtons;

    private byte topOfTheList;

    private byte bottomOfList;
	// Activate quest menu on area click
    public void activateQuestMenu()
    {
        quitOutButton.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
        topOfTheList = 0;
    }

    // Deactivate quest menu on close button click
    public void deactivateQuestMenu()
    {
        quitOutButton.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }


    #region thingsToRefactor

     public byte getTopOfList(byte index)
    {
        return (byte)(index + topOfTheList);
    }

    public void formatButtonsForThisPage(List<Quest> questsAtThisLocation)
    {
        decideOnHowManyButtonsToShow(questsAtThisLocation.Count);

        int i = 0;
        foreach (Button menuButton in menuChoices)
        {
            if (menuButton.isActiveAndEnabled)
            {
                menuChoices[i].GetComponentInChildren<Text>().text = questsAtThisLocation[i].QuestName;
               // menuTokens[i].GetComponentInChildren<Image>().sprite = null;
                i++;
            }
        }
    }

    private void decideOnHowManyButtonsToShow(int sizeOfList)
    {
        setAllButtonsInactive();
        numberOfActiveButtons = (byte)menuChoices.Length;
        if (sizeOfList < menuChoices.Length)
        {
            numberOfActiveButtons = (byte)sizeOfList;
        }

        for (int i = 0; i < numberOfActiveButtons; i++)
        {
            menuChoices[i].gameObject.SetActive(true);
        }
    }

    private void setAllButtonsInactive()
    {
        for (int i = 0; i < menuChoices.Length; i++)
        {
            menuChoices[i].gameObject.SetActive(false);
        }
    }

    #endregion
}
