using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Road : MonoBehaviour
{

    public Color colorWhenLocked;
    public Color colorWhenUnlocked; 

    public void showRoad()
    {
        this.GetComponent<Image>().color = colorWhenUnlocked;
    }

    public void hideRoad()
    {
        this.GetComponent<Image>().color = colorWhenLocked;
    }
    //This class will only contain methods for being highlighted or not being highlighted.  I may not even need a class for it, but I am for now
}
