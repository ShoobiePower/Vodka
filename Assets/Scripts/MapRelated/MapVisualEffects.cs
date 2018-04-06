using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MapVisualEffects : MonoBehaviour
{
    [SerializeField]
    RectTransform MapImage;
    [SerializeField]
    ScrollRect MapScrollRect;
    [SerializeField]
    float ZoomSpeed = 5f;
    [SerializeField]
    float ScrollToSpeed = 3.5f;
    [SerializeField]
    Vector2 zoomedInScale = new Vector2(2, 2);

    Vector2 normalScale = new Vector2(1, 1); //This should be set equal to the starting scale of the map, but... no constructor :(
                                             //I could make this visible in the inspector, or set up an "Init()" function that you could call
                                             //whenever you want to set up all the classes
    bool isZooming = false;


    public void ZoomMapInToRegion(Region desiredRegion)
    {
        //StartCoroutine(ZoomWhileScrolling(zoomedInScale, desiredLocation));
    }

    public void ZoomMapOutOfRegion(Region desiredRegion)
    {
        //StartCoroutine(ZoomWhileScrolling(normalScale, desiredLocation));
    }



    /*
    Possible problems:
        > The zoom and scroll only stops when the camera finishes zooming (it doesn't check for scrolling)
        > Not good separation of concern? (separate into a couple methods?)
        > There's jerking when the player tries to move the camera while the camera is scrolling (SOLVED by adding tolerance)
    */

    //I hate that these are in class scope...
    //If the coroutine can be called > once, severe lag.  Variables, however, need to be updated.
    Vector2 desiredScale;
    Region desiredRegion;

    //IEnumerator ScrollToLocation(Location _desiredLocation)
    //{
    //    desiredLocation = _desiredLocation;

    //    if(!isZooming)
    //    {
    //        isZooming = true;

    //        //Lock scrolling
    //        MapScrollRect.vertical = false;
    //        MapScrollRect.horizontal = false;
    //        Vector3 desiredPosition = _desiredLocation.transform.position;
    //        float tolerance = 0.001f;


    //        while (Mathf.Abs(MapImage.position.x - MapScrollRect.transform.InverseTransformPoint(desiredPosition).x) > 0 &&
    //                Mathf.Abs(MapImage.position.y - MapScrollRect.transform.InverseTransformPoint(desiredPosition).y) > 0)
    //        {
    //            desiredPosition = _desiredLocation.transform.position;

    //        }
    //    }
    //}

    //IEnumerator ZoomWhileScrolling(Vector2 _desiredScale, Location _desiredLocation)
    //{
    //    desiredScale = _desiredScale;
    //    desiredLocation = _desiredLocation;

    //    if (!isZooming) //prevent the coroutine being called multiple times at once
    //    {
    //        isZooming = true;

    //        //Lock scrolling
    //        MapScrollRect.vertical = false;
    //        MapScrollRect.horizontal = false;
    //        Vector2 desiredPosition = new Vector2();
    //        float tolerance = 0.001f;

    //        while ((Mathf.Abs(desiredScale.x - MapImage.localScale.x) > tolerance ||     //While the Scroll rect has not finised zooming /*OR scrolling*/...
    //               Mathf.Abs(desiredScale.y - MapImage.localScale.y) > tolerance) &&
    //               (Mathf.Abs(desiredPosition.x - MapImage.position.x) > tolerance ||
    //               Mathf.Abs(desiredPosition.y - MapImage.position.y) > tolerance))

    //        {
    //            //Zoom the map
    //            MapImage.localScale = Vector2.Lerp(MapImage.localScale, desiredScale, Time.fixedDeltaTime * ZoomSpeed);

    //            //Scroll the map
    //            desiredPosition =
    //                (Vector2)MapScrollRect.transform.InverseTransformPoint(MapImage.position)
    //                - (Vector2)MapScrollRect.transform.InverseTransformPoint(desiredLocation.transform.position);

    //            //This if-statement corrects the map's ciruclar scrolls, but causes jerking if you try to zoom into a different position
    //            //Added tolerance to fix this issue
    //            if (desiredScale != normalScale) //scroll nicely to the position if we're zooming in
    //            {
    //                MapImage.anchoredPosition = Vector2.Lerp(MapImage.anchoredPosition, desiredPosition, Time.deltaTime * ScrollToSpeed);
    //            }
    //            else //force stay at the position if we're moving out
    //            {
    //                MapImage.anchoredPosition = desiredPosition;
    //            }

    //            yield return new WaitForEndOfFrame();
    //        }

    //        MapImage.anchoredPosition = desiredPosition;
    //        MapImage.localScale = desiredScale;

    //        if (desiredScale == normalScale)
    //        {
    //            //Allow scrolling
    //            MapScrollRect.vertical = true;
    //            MapScrollRect.horizontal = true;
    //        }

    //        isZooming = false;
    //    }
    //}
}
