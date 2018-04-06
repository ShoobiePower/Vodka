using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PatronCheatSheet : MonoBehaviour {

    [SerializeField]
    Text patronName;

    [SerializeField]
    Image patronPortrait; 

    [SerializeField]
    Text patronLevel;

    [SerializeField]
    Text skill0;

    [SerializeField]
    Text skill1;

    [SerializeField]
    Text skill2;

    [SerializeField]
    Text skill3;

    private Color[] colorsOfStat;
    private Text[] statsToList;

    public void activatePatronCheatSheet()
    {
        this.gameObject.SetActive(true);
    }

    public void deactivatePatronCheatSheet()
    {
        this.gameObject.SetActive(false);
    }

    public void displayStats(Patron patronToDisplay)
    {

        if (statsToList == null)
        {
            initStatsToList();
        }

        patronName.text = patronToDisplay.Name;
        patronLevel.text = "Bond Level: " + patronToDisplay.Level.ToString();
        patronPortrait.sprite = ApperanceManager.instance.ThisPatronsBarToken(patronToDisplay.ID);

        fillAllBlanksWithLockedStats();
        for (int i = 0; i < patronToDisplay.PatronSkills.Count; i++)
        {
            statsToList[(byte)i].text = patronToDisplay.PatronSkills[i].ToString();
        }
    }

    private void initStatsToList()
    {
        statsToList = new Text[4];
        statsToList[0] = skill0;
        statsToList[1] = skill1;
        statsToList[2] = skill2;
        statsToList[3] = skill3;
    }

    private void fillAllBlanksWithLockedStats()
    {
        for (int i = 0; i < statsToList.Length; i++)
        {
            statsToList[i].text = "???";
        }
    }
}
