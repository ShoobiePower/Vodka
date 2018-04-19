using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeployState : MonoBehaviour, IMapStates
{

    enum MapState { INACTIVE, CONVERTING_RUMOR_TO_QUEST, SETTING_ADVENTURE_LOCATION, CHOOSING_QUEST }
    MapState currentMapState = MapState.INACTIVE;

    [SerializeField]
    Color colorWhenSelectedForAdventure;
    [SerializeField]
    Color colorWhenNotSelectedForAdventure;

    [SerializeField]
    PatronCheatSheet patronCheatSheet;

    [SerializeField]
    RectTransform DependentUI;  //Condensed all the buttons and stuff into one parent object

    [SerializeField]
    Image patronInfoFrame;

    [SerializeField]            
    Text AdventureCostDisplay;

    [SerializeField]
    Text AdventureCostDisplayShadow;

    public MapManager mapManager;
    Region selectedRegion = null;

    public DeployState(MapManager mapsly)
    {
        mapManager = mapsly;
    }

    public void closeMapProps()
    {
        patronCheatSheet.deactivatePatronCheatSheet();
        HideQuestInfoPanel();
        DependentUI.gameObject.SetActive(false);
        patronInfoFrame.gameObject.SetActive(false);
        //AdventureUnderConstruction.ClearAdventure();
        mapManager.MapVisualEffectScript.ZoomMapOutOfRegion(selectedRegion); // just incase, I encounted a strange zoom in bug.
        UpdateAdventureCostDisplayText();
    }

    public void regionClicked(Region clickedRegion)
    {
            selectedRegion = clickedRegion;
            mapManager.MapVisualEffectScript.ZoomMapInToRegion(selectedRegion); 
            currentMapState = MapState.CHOOSING_QUEST;
            ShowQuestInfoPanel();
            UpdateAdventureCostDisplayText();
    }

    public void openMapProps()
    {
        Debug.Log("Dependant ui should be open");
        patronCheatSheet.deactivatePatronCheatSheet();
        patronCheatSheet.displayStats(mapManager.PatronToGoOnAdventure);
        currentMapState = MapState.SETTING_ADVENTURE_LOCATION;
        DependentUI.gameObject.SetActive(true);
        patronInfoFrame.gameObject.SetActive(true);
        UpdateAdventureCostDisplayText();
    }

    public void ShowQuestInfoPanel()
    {
        mapManager.getQuestInfoPanel.gameObject.SetActive(true);
        mapManager.getQuestInfoPanel.InitializeQuestInfoPanel(selectedRegion, mapManager.PatronToGoOnAdventure);
    }

    public void HideQuestInfoPanel()
    {
        mapManager.getQuestInfoPanel.gameObject.SetActive(false);
    }

    public void FinishTaskOnMap() //Called when the player clicks the "Choose quest" add quest button;
    {
            mapManager.PatronToGoOnAdventure.QuestToCompleete = mapManager.getQuestInfoPanel.GetQuestFromLocation();
            mapManager.sendPatronOnQuest(mapManager.PatronToGoOnAdventure);
            mapManager.getQuestInfoPanel.GetQuestFromLocation().ChangeQuestStatusToTaken();
        mapManager.getQuestInfoPanel.GetQuestFromLocation().SetTokenNumberForQuest(mapManager.PatronToGoOnAdventure.ID);
    }

    public void GetRefrenceOfMapManager(MapManager constructMapManager)
    {
       // mapManager = constructMapManager;
    }

    void UpdateAdventureCostDisplayText()
    {

        // Print cost of quest based on designer's ideas


        //AdventureCostDisplay.text = "Cost: " + AdventureUnderConstruction.Cost;
        //AdventureCostDisplayShadow.text = "Cost: " + AdventureUnderConstruction.Cost;
    }

    public void togglePatronCheatSheet()
    {
        Debug.Log("Is hit");
        if (patronCheatSheet.isActiveAndEnabled)
        {
            patronCheatSheet.deactivatePatronCheatSheet();
        }
        else
            patronCheatSheet.activatePatronCheatSheet();
    }

}

