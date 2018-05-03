using UnityEngine;
using System.Collections;

public class Shutter : AbstractDropDownObject
{

    private void Start()
    {
        invertLocationToDropTo();
        //offscreenStartPosition = this.transform.position;
        //this.transform.position = 
    }

    public void MoveShutterUp()
    {
        changeAnimationState(animationStates.MOVEUP);
    }

    public void MoveShutterDown()
    {
        changeAnimationState(animationStates.MOVEDOWN);
    }

}
