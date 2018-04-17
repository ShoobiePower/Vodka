using UnityEngine;
using System.Collections;

public abstract class AbstractDropDownObject : MonoBehaviour
{
    protected enum animationStates { STOP, MOVEDOWN, COOLDOWN, MOVEUP };  // code smell, but I really need to get this done.
    protected animationStates currentAnimationState;

    [SerializeField]
    protected float howLongIsTransition;

    [SerializeField]
     protected Transform transformToMoveTo;

    private Vector3 positionToMoveTo;

    protected Vector3 offscreenStartPosition;

    protected Vector3 velocity;

    protected const float offset = 0.1f;

    protected void initLocationOfDrop()
    {
        positionToMoveTo = transformToMoveTo.position;
    }
    

    // Update is called once per frame
    protected void Update()
    {
        switch (currentAnimationState)
        {
            case animationStates.MOVEDOWN:
                {
                    repositionDropDown(positionToMoveTo);
                    checkIfWeReachedBottomPosition(positionToMoveTo);
                    break;
                }

                 case animationStates.MOVEUP:
                {
                    repositionDropDown(offscreenStartPosition);
                    checkIfWeReachedStartPosition(offscreenStartPosition);
                    break;
                }
        }
    }

    protected virtual void checkIfWeReachedBottomPosition(Vector3 desiredLocation)
    {
        if (this.transform.position.y <= (desiredLocation.y + offset))
        {
            changeAnimationState(animationStates.STOP);
        }
    }

     private void checkIfWeReachedStartPosition(Vector3 desiredLocation)
    {
        if (this.transform.position.y >= (desiredLocation.y - offset))
        {
            changeAnimationState(animationStates.STOP);
        }
    }

    private void repositionDropDown(Vector3 desiredLocation)
    {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, desiredLocation, ref velocity, howLongIsTransition);
    }

    protected void changeAnimationState(animationStates newAnimationState)
    {
        currentAnimationState = newAnimationState;
    }

}
