using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeployState : MonoBehaviour, IMapStates
{

    [SerializeField]
    PatronCheatSheet patronCheatSheet;

    [SerializeField]
    Button ExitMapButton;

    [SerializeField]
    Image patronInfoFrame;

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
        ExitMapButton.gameObject.SetActive(false);
        patronInfoFrame.gameObject.SetActive(false);
        mapManager.MapVisualEffectScript.ZoomMapOutOfRegion(selectedRegion); // just incase, I encounted a strange zoom in bug.
    }

    public void regionClicked(Region clickedRegion)
    {
            selectedRegion = clickedRegion;
            mapManager.MapVisualEffectScript.ZoomMapInToRegion(selectedRegion); 
            ShowQuestInfoPanel();
    }

    public void openMapProps()
    {
        Debug.Log("Dependant ui should be open");
        patronCheatSheet.deactivatePatronCheatSheet();
        patronCheatSheet.displayStats(mapManager.PatronToGoOnAdventure);
        patronInfoFrame.gameObject.SetActive(true);
        CheckIfCancleButtonShouldAppear();
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


    public void togglePatronCheatSheet()
    {
        if (patronCheatSheet.isActiveAndEnabled)
        {
            patronCheatSheet.deactivatePatronCheatSheet();
        }
        else
            patronCheatSheet.activatePatronCheatSheet();
    }

    private void CheckIfCancleButtonShouldAppear()
    {
        ExitMapButton.gameObject.SetActive( mapManager.NumberOfPatronsInTheBar > mapManager.CountHowManyQuestsAreAvailable());
    }

}

