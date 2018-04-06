using UnityEngine;
using System.Collections;
using System;

public interface IEndOfDayStates {
    void ShowPresetAssets();
    void HidePresetAssets();
    void ScrollUp();
    void ScrollDown();
    void ShowStatsOnPage(byte index);
    void passRefrenceToEndOfDayManager(EndOfDayManager endOfDayManager);

}


       


