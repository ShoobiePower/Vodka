using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Region : MonoBehaviour
{
    [SerializeField]
    List<LocationAndRoad> adjacentRegions;
    [SerializeField]
    List<Image> emptySpotsAtThisRegion;

     List<Quest> questsAtLocation = new List<Quest>();
     public List<Quest> QuestsAtLocation { get { return questsAtLocation; } }

    List<Patron> patronsAtThisLocation = new List<Patron>();
    

    public Image flag;
    
    public enum LocationIdentifier
    {
        KASTONIA, WOODLAND, MOUNTAINS, LAKESIDE
        //HOME, GRAND_MARKET, BOROUGH, LOWTOWN, SCHOLARS_PLAZA,
        //CALLOUSED_MOOR, WOODGLADE, GRASSY_PLAINS, KELLS_PLATEAU,
        //HOOTIES_HARBOR, WIPLASH_RAPIDS, THISTLEWOOD, SKYPEAK, FARLANDS
    }
    [SerializeField]
    LocationIdentifier NameOfLocation;

    Sprite thisLocationsDefautlMapImage;

    public int QuestCountAtLocation { get { return questsAtLocation.Count; } }  //NOTE: Slot 0 should always be reserved for "no quest."  There should be one more slot than there are quests at this location
    public string Name { get { return NameOfLocation.ToString(); } }

    //List<Patron> patronsAtLocation = new List<Patron>();

    [System.Serializable]
    struct LocationAndRoad //workaround: Unity cannot display dictionaries to the editor
    {
        public Region region;   //should probably be called "ConnectedLocations", but I'm afraid of having to drag everything back in again.
        public Road roadConnection;
    }


    public bool IsAdjacentToLocation(Region desiredRegion)
    {
        for (int i = 0; i < adjacentRegions.Count; i++)
        {
            if (adjacentRegions[i].region == desiredRegion)
                return true;
        }

        return false;
    }

    public void unlockLocation()
    {
        thisLocationsDefautlMapImage = this.gameObject.GetComponent<Button>().image.sprite;
        this.gameObject.GetComponent<Button>().enabled = true;
        this.GetComponent<Image>().color = this.gameObject.GetComponent<Button>().colors.normalColor;

       for (int i = 0; i < adjacentRegions.Count; i++)
        {
            if (IsAdjacentToLocation(adjacentRegions[i].region) && adjacentRegions[i].region.gameObject.GetComponent<Button>().enabled == true)
            {
                adjacentRegions[i].roadConnection.showRoad();
            }
        }
    }

    public void lockLocation()
    {
        this.gameObject.GetComponent<Button>().enabled = false;
        this.GetComponent<Image>().color = this.gameObject.GetComponent<Button>().colors.disabledColor;
    }

    public void removePotentialQuestFlag()
    {
        removeFlagIfLastQuest();
        flag.sprite = ApperanceManager.instance.GetQuestAlertToken();
    }

    public void AddNewQuestToThisLocation(Quest questToAdd)
    {
        flag.sprite = ApperanceManager.instance.GetQuestAlertToken();
        flag.gameObject.SetActive(true);
        questsAtLocation.Add(questToAdd);
    }


    public void removeQuestFromLocation(Quest questToRemove)
    {
        questsAtLocation.Remove(questToRemove);
        removeFlagIfLastQuest();
    }

    public Quest findQuestAtThisLocationByIndex(int desiredQuestIndex)
    {
        try
        {
            return questsAtLocation[desiredQuestIndex];
        }
        catch
        {
            Debug.Log("The Quest at index " + desiredQuestIndex + " could not be found at location " + this + " (Index out of range)");
            return null;
        }
    }


    public void clearPatronsAtLocation()
    {
        Debug.Log("Clearing patrons at this location" + this.Name);
        patronsAtThisLocation.Clear();
    }

    public void addPatronsToLocation(Patron patronToAdd)
    {
        patronsAtThisLocation.Add(patronToAdd);
        Debug.Log("Patron" + patronToAdd.Name + "Added to location");
        placeArtAtEmptySpot(patronToAdd.ID);
    }

    public void removePatronFromLocation(Patron patronToRemove)
    {
        patronsAtThisLocation.Remove(patronToRemove);
        Debug.Log("Patron" + patronToRemove.Name + "Removed from location");
        shuffleArtAtThisLocation();
    }

    private void placeArtAtEmptySpot(byte idNumberOfArtToFetch)
    {
        for (int i = 0; i < emptySpotsAtThisRegion.Count; i++)
        {
            
            if (emptySpotsAtThisRegion[i].sprite == null)
            {
                emptySpotsAtThisRegion[i].sprite = ApperanceManager.instance.ThisPatronsToken(idNumberOfArtToFetch);
                break;
            }
        }
    }

    private void shuffleArtAtThisLocation() //after leaving, a parton's art needs to dissapear and any extra patron's art needs to show up. 
    {
       for (int i = 0; i < emptySpotsAtThisRegion.Count; i++)
        {
            if (i >= patronsAtThisLocation.Count)
            {
                emptySpotsAtThisRegion[i].sprite = null;
            }
            else
            {
                emptySpotsAtThisRegion[i].sprite = ApperanceManager.instance.ThisPatronsToken(patronsAtThisLocation[i].ID);
            }
        }
    }

    public string giveNamesOfPatronsAtThisLocation()
    {
        string namesOfPatrons = string.Empty;
       for ( int i = 0; i < patronsAtThisLocation.Count; i++)
        {
            namesOfPatrons += patronsAtThisLocation[i].Name + "\n";
        }
        return namesOfPatrons;
    }

    public string giveNamesOfQuestsAtThisLocation()
    {
        string namesOfQuests = string.Empty;
        for (int i = 0; i < questsAtLocation.Count; i++)
        {
            namesOfQuests += questsAtLocation[i].QuestName + "\n";
        }
        return namesOfQuests;
    }


    private void removeFlagIfLastQuest()
    {
        if (questsAtLocation.Count == 0)
        {
            flag.gameObject.SetActive(false);
        }
    }
}

