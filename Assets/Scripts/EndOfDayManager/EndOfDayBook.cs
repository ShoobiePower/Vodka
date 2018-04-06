using UnityEngine;
using System.Collections;

public class EndOfDayBook : MonoBehaviour {

    public void SetEndOfDayBookActive()
    {
        this.gameObject.SetActive(true);
    }

    public void SetEndOfBookInactive()
    {
        this.gameObject.SetActive(false);
    }
}
