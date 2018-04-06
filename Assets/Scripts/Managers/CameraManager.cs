using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour{

    public Camera mainCamera;
    public Transform cameraStart;
    public Transform cameraDestination;
    public float howLongIsPan;
    private Vector3 defaultCameraLocation;
    private Vector3 desiredLocation;
    private Vector3 velocity;
    private bool isCameraShaking;
    private float shakeAmount = 1f;

    void Start()
    {
        defaultCameraLocation = this.transform.position;
        desiredLocation = defaultCameraLocation;
    }
    private void Update()
    {
        mainCamera.transform.position = Vector3.SmoothDamp(this.transform.position, desiredLocation, ref velocity , howLongIsPan);
        
        if (isCameraShaking)
        {
            moveTheCameraAround();
        } 
    }

    public void PanToLocation(Vector3 targetLocation)
    { 
        desiredLocation = new Vector3(targetLocation.x,defaultCameraLocation.y,defaultCameraLocation.z);
    }

    public void shakeTheCamera()
    {
        isCameraShaking = true;
    }

    public void stopShakingCamera()
    {
        isCameraShaking = false;
        panToDefaultLocation();
    }

    private void moveTheCameraAround()
    {
        Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;

        desiredLocation = new Vector3(mainCamera.transform.position.x + ShakePos.x, mainCamera.transform.position.y + ShakePos.y, mainCamera.transform.position.z);
    }

    private void panToDefaultLocation()
    {
        PanToLocation(defaultCameraLocation);
    }

    #region ThingsWeMayNeed
    //public void panDown() // We may need this at some point
    //{
    //    if(checkCanPan(desiredLocation.y + speedToGoDown))
    //    desiredLocation = new Vector3(this.transform.position.x , this.transform.position.y + speedToGoDown, this.transform.position.z);
    //}


    //private bool checkCanPan(float yValueToCheck) // we may also need this
    //{
    //    if (yValueToCheck <= bottomParimiter) //|| yValueToCheck >= rightMostBound)
    //        return false;

    //    else
    //        return true;
    //}

    //public void ZoomOut()
    //{
    //    // mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, howFarToZoomOut, ref speedOfZoom, zoomTime);
    //    desiredLocation = this.transform.position;
    //    desiredZoom = howFarToZoomOut;
    //}

    //public void ZoomIn(Vector3 targetPatronLocation)
    //{

    //    //mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, howFarToZoomIn, ref speedOfZoom, zoomTime);
    //    desiredLocation = new Vector3(targetPatronLocation.x, targetPatronLocation.y, this.transform.position.z);
    //    desiredZoom = howFarToZoomIn;

    //    //Debug.Log(mainCamera.orthographicSize);
    //}

    //public void goBackToDefaultView()
    //{
    //    desiredLocation = defaultCameraLocation;
    //}
    #endregion
}
