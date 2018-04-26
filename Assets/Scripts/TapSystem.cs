using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
///  Our drink tap, this controls what is in the mug and the animation that plays
///  TODO, factor out the animation part to a seperate class.  (I am noting this to try to learn what I did)
/// </summary>
public class TapSystem : MonoBehaviour {

    // The despeser buttons we have 
    [SerializeField]
    Dispencer[] dispencersWeHaveEquipped = new Dispencer[4];

    // This button tells the bar manager to serve whatever is in the mug and then clean it. 
    [SerializeField]
    Button ServeDrinkButton;

    // this clears all ingredents from the mug.
    [SerializeField]
    Button RecycleDrinkButton;

    // An animator state machine that holds all the animations and the links between them. 
    [SerializeField]
    Animator drinkPourAnimator;

    // An easy way for me to put in "TargetAnimation" over and over ( this is a varable that helps the animator switch states) 
    private const string animationIntTriggerName = "TargetAnimation";
    private const string animationEndBoolTriggerName = "IsFinishedPlaying";

    // this is used as a code gate so the animation can return to idle. 
    private bool isAnimationOver;


    public void Start()
    {
        lockTapSystem();
        isAnimationOver = true;
    }

    private void Update()
    {
        checkIfDrinkAnimationIsDone();
    }


    public void lockTapSystem()
    {
        lockTapPulls();
        ServeDrinkButton.interactable = false;
        RecycleDrinkButton.interactable = false;
    }

    public void lockTapPulls()
    {
        for (int i = 0; i < dispencersWeHaveEquipped.Length; i++)
        {
            dispencersWeHaveEquipped[i].swapToDisabled();
        }
    }

    public void unlockTapSystem()
    {
        for (int i = 0; i < dispencersWeHaveEquipped.Length; i++)
        {
            dispencersWeHaveEquipped[i].swapToEnabled();
        }
       
    }

    public void unlockServeAndRecycleButtons()
    {
        ServeDrinkButton.interactable = true;
        RecycleDrinkButton.interactable = true;
    }

    public Ingredient useIngredent(byte slotToUse)
    {
        isAnimationOver = false;
        drinkPourAnimator.SetInteger(animationIntTriggerName, (int)slotToUse);
        drinkPourAnimator.SetBool(animationEndBoolTriggerName, isAnimationOver);
        return new Ingredient(dispencersWeHaveEquipped[slotToUse].dispenceIngredent());
    }

    // OK this is checked on update
    private void checkIfDrinkAnimationIsDone()
    {
     // our code gate to ensure some animation plays
       if (!isAnimationOver)
        {
            // if you have played for at least one "Tiny second" you are next going to go to idle, unless of course I tell you to do something else
            if (drinkPourAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < .1f)
            {
                isAnimationOver = true;
                drinkPourAnimator.SetBool(animationEndBoolTriggerName, isAnimationOver);
                drinkPourAnimator.SetInteger(animationIntTriggerName, -1);
            }

        }
    }
}
