using UnityEngine;
using System.Collections;

public class PauseScrollDownComponent : AbstractDropDownObject {

    public bool IsMenuDown { get; private set; }
    
    private void Start()
    {
        initLocationOfDrop();
        IsMenuDown = false;
        offscreenStartPosition = this.transform.position;
    }

    public void MoveMenuDown()
    {
        changeAnimationState(animationStates.MOVEDOWN);
        IsMenuDown = true;
    }

    public void MoveMenuUp()
    {
        changeAnimationState(animationStates.MOVEUP);
        IsMenuDown = false;
    }
}
