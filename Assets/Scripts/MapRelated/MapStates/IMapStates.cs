using UnityEngine;
using System.Collections;

public interface IMapStates {  

    void openMapProps(); // determines which props load in when map is Opened
    void closeMapProps();
    void regionClicked(Region region); // what happens when we click a region? 
    void ShowQuestInfoPanel();
    void HideQuestInfoPanel();
    void FinishTaskOnMap();
    void GetRefrenceOfMapManager(MapManager refrence); //  a constructor hop around :P love you unity!
}
