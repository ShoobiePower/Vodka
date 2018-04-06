using UnityEngine;
using System.Collections;


public class ToastPool : MonoBehaviour { 
    Toast[] toastPool = new Toast[7];
    
    [SerializeField]
    Toast ToastObjectToCreate;

    private int numberOfUsedToasts; // used to determine how low our next toast should go;


    void Start()
    {
        Init();
    }

    void Init()
    {
        for (int i = 0; i < toastPool.Length; i++)
        {
            toastPool[i] = Instantiate(ToastObjectToCreate);
            toastPool[i].enabled = false;
        }
    }

    public void SendToastFromLocation(Vector3 locationOfToast, string whatToToast)
    {
        numberOfUsedToasts = 0; // PlaceHolder
        Toast toastOut = findAvailableToastFromPool();
        toastOut.transform.SetParent(this.transform);
        toastOut.AlterToastFlightPlan(numberOfUsedToasts);
        toastOut.sendToast(locationOfToast, whatToToast);
    }

    Toast findAvailableToastFromPool()
    {
        Toast toastToReturn = findUnoccupiedToast();
        toastToReturn.enabled = true; 

        return toastToReturn;
    }

    Toast findUnoccupiedToast()
    {
        for (int i = 0; i < toastPool.Length; i++)
        {
            if (!toastPool[i].enabled)
            {
                return toastPool[i];
            }
            else numberOfUsedToasts++; // if there is a
        }

        
        return toastPool[0];
    }

}
