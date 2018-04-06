using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Toast : MonoBehaviour { // something else I would like to be a fading object. 


    Text wordsOnToast;
    public float howLongIsToast;
    private float toastCountDown;
    public float howFastIsToast;
    public float howHighUpScreenDoesToastGo; // where 1 is all the way up. 
    private float toastHeight; // placeholder
    private bool isToastSent = false;
    private const float spaceBetweenToasts = .05f;



   public void sendToast(Vector3 location, string whatToToast)
    {
        if (wordsOnToast == null)
        {
            assignWordsOnToastObject();
        }
        wordsOnToast.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(location.x, location.y, location.z));
        wordsOnToast.text = whatToToast;
        isToastSent = true;
        toastCountDown = howLongIsToast;
        
    }

    public void AlterToastFlightPlan(int numberOfActiveToasts)
    {
        toastHeight = howHighUpScreenDoesToastGo;
        toastHeight -= (spaceBetweenToasts * numberOfActiveToasts);
    }
	

	void Update ()
    {
    
        if (isToastSent)
        {
            wordsOnToast.transform.position = Vector3.Lerp(wordsOnToast.transform.position, new Vector3(wordsOnToast.transform.position.x, (toastHeight * Screen.height)  , wordsOnToast.transform.position.z), howFastIsToast);
            checkIfToastIsOver();
        }
    } 

  private void checkIfToastIsOver()
    {
        toastCountDown -= Time.deltaTime;
        if (toastCountDown <= 0)
        {
            wordsOnToast.text = " ";
            isToastSent = false;
            this.enabled = false;
        }

    }

    private void assignWordsOnToastObject()
    {
        wordsOnToast = this.GetComponent<Text>();
    }
}
