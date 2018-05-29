using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MapOpen : MonoBehaviour, IMapStates, IEndOfDayStates
{

    EndOfDayManager endOfDayManager;

    [SerializeField]
    Button ExitOutOfMapButton;

    [SerializeField]
    MapManager mapManager;

    private Region selectedRegion;

    public void passRefrenceToEndOfDayManager(EndOfDayManager endOfDayManager)
    {
        this.endOfDayManager = endOfDayManager;
    }


    public void HidePresetAssets()
    {
        mapManager.closeMapProps();
    }

    public void ScrollDown()
    {
        throw new NotImplementedException();
    }

    public void ScrollUp()
    {
        throw new NotImplementedException();
    }

    public void ShowPresetAssets()
    {
        mapManager.openFromEndOfDay();
    }

    public void ShowStatsOnPage(byte index)
    {
      
    }

    public void openMapProps()
    {
        ExitOutOfMapButton.gameObject.SetActive(true);
    }

    public void closeMapProps()
    {
        ExitOutOfMapButton.gameObject.SetActive(false);
    }

    public void regionClicked(Region region)
    {
        selectedRegion = region;
        ShowQuestInfoPanel();
        //Debug.Log("This is: " + region.name);
        //Debug.Log("Patrons at this location" + region.giveNamesOfPatronsAtThisLocation());
        //Debug.Log("Active quests at this location" + region.giveNamesOfQuestsAtThisLocation());
    }

    public void ShowQuestInfoPanel()
    {
        mapManager.getQuestInfoPanel.gameObject.SetActive(true);
        mapManager.getQuestInfoPanel.ShowQuestInfoForRegion(selectedRegion);
    }

    public void HideQuestInfoPanel()
    {
        throw new NotImplementedException();
    }

    public void FinishTaskOnMap()
    {
        throw new NotImplementedException();
    }

    public void GetRefrenceOfMapManager(MapManager refrence)
    {
        throw new NotImplementedException();
    }
}
