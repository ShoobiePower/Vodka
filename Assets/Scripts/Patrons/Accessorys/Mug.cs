using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mug : ABSTFadableObject
{
    // TODO, Cant quite do it, but someday refactor out into a decorator that can throw an I fadeable and an I slideable 
    private SpriteRenderer patronsMug;

    private Vector3 mugEndPosition;


    [SerializeField]
    Transform mugStartTransform;

    //[SerializeField]
    //Transform mugEndTransform;

    

    [SerializeField]
    float howLongForMugToReachDestination;
    //private Color fullMugValue;


    public void initMug()
    {
        patronsMug = GetComponent<SpriteRenderer>();
        mugEndPosition = GetComponent<Transform>().position;
        assignObjectToFade(patronsMug);
        hideMug();
    }

     void Update()
    {
       this.transform.position = Vector3.Lerp(this.transform.position, mugEndPosition, howLongForMugToReachDestination);
        base.Update(); /// This works but I want to know if there is a best practice, seems sloppy. 
    }

    public void showMug() // float drinksDesity
    {
        setMugForSlide();
        showFullAlphaValueArt();
        this.gameObject.SetActive(true);
       // fadeMug(drinksDesity);
    }

    private void setMugForSlide()
    {
        this.transform.position = mugStartTransform.position;
    }

    public void fadeMug(float timeToFade)
    {
        setFadeTime(timeToFade);
        startExitAnimation();
    }


    public void hideMug()
    {
        if (patronsMug != null)
        {
            patronsMug.gameObject.SetActive(false);
        }
    }

}
