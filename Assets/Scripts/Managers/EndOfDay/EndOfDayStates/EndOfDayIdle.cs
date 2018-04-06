using UnityEngine;
using System.Collections;
using System;

public class EndOfDayIdle : MonoBehaviour, IEndOfDayStates
{
    [SerializeField]
    GameObject endOfDayManagementPanel;


    public void ShowPresetAssets()
    {
        endOfDayManagementPanel.SetActive(false);
    }

    public void decideOnHowManyButtonsToShow()
    {
      
    }

    public void formatButtonsForThisPage()
    {
    
    }

    public void HidePresetAssets()
    {
        
    }

    public void passRefrenceToEndOfDayManager(EndOfDayManager endOfDayManager)
    {
       
    }

    public void ScrollDown()
    {
       
    }

    public void ScrollUp()
    {
        
    }

    

    public void ShowStatsOnPage(byte index)
    {
     
    }
}
