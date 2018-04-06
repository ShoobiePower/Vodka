using UnityEngine;
using System.Collections;

public class EndOfDayMap : MonoBehaviour {

    public void SetEndOfDayMapActive()
    {
        this.gameObject.SetActive(true);
    }

    public void SetEndOfDayMapInactive()
    {
        this.gameObject.SetActive(false);
    }
}
