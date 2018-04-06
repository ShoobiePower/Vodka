using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ABSTFadableObject : MonoBehaviour {

    private float amountToFadeBy;
    private Color fullHereColor;
    const byte emptyAlphaValue = 0;
    private SpriteRenderer artToFade;
    private bool isPaused;

    protected enum animationStates { ENTER, ENTEREND, EXIT, EXITEND, NONE } 
    protected animationStates currentAnimationState;

    public ABSTFadableObject()
    {
        isPaused = false;
    }


    protected void assignObjectToFade(SpriteRenderer SpriteToFade)
    {
        artToFade = SpriteToFade;
        setDefaultFadeState();
    }


	
	// Update is called once per frame
	 protected void Update () {
        if (!isPaused)
        {
            if (currentAnimationState == animationStates.ENTER)
            {
                runEnterAnimation();
            }

            if (currentAnimationState == animationStates.EXIT)
            {
                runExitAnimation();
            }
        }
    }

    protected void showFullAlphaValueArt()
    {
        artToFade.color = fullHereColor;
    }

    protected void setDefaultFadeState()
    {
        fullHereColor = artToFade.color; // used for our fade in and out; 
        artToFade.color= new Color(fullHereColor.r, fullHereColor.g, fullHereColor.b, emptyAlphaValue); 
        currentAnimationState = animationStates.NONE;
    }

    protected void setFadeTime(float fadeTime)
    {
        amountToFadeBy = fadeTime;
    }

    public void startEnterAnimation()
    {
        currentAnimationState = animationStates.ENTER;
    }

   protected void runEnterAnimation()
    {
        artToFade.color += new Color(0, 0, 0, fullHereColor.a / amountToFadeBy) * Time.deltaTime;
        if (artToFade.color.a >= fullHereColor.a)
        {
            currentAnimationState = animationStates.NONE;
        }
    }

    public void startExitAnimation()
    {
        currentAnimationState = animationStates.EXIT;
    }

    protected void runExitAnimation()
    {
        artToFade.color -= new Color(0, 0, 0, fullHereColor.a / amountToFadeBy) * Time.deltaTime;
        if (artToFade.color.a <= 0)
        {
            currentAnimationState = animationStates.EXITEND;
        }
    }

    public void pauseAnimation(bool yesNo)
    {
        isPaused = yesNo;
    }

    
}
