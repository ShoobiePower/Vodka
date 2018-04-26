using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public interface ITimeOfDayStates  {

    void startAnimation(Image imageToAnimate);
    void endAnimation(Image imageToAnimate);
    
}

public class TransitionToNight : ITimeOfDayStates
{
    TimeOfDayManager timeOfDayManager;
    private const float maxAlphaValue = 1;


    public TransitionToNight(TimeOfDayManager timeOfDayManager)
    {
        this.timeOfDayManager = timeOfDayManager;

    }

    public void startAnimation(Image imageToAnimate)
    {
        imageToAnimate.color += new Color(0, 0, 0, maxAlphaValue / 3) * Time.deltaTime;
        if (imageToAnimate.color.a >= maxAlphaValue)
        {
            endAnimation(imageToAnimate);
        }
    }

    public void endAnimation(Image imageToAnimate)
    {
        timeOfDayManager.StopAnimations();
        timeOfDayManager.EndPhase();
    }

}

public class TransitionToDay : ITimeOfDayStates
{

    TimeOfDayManager timeOfDayManager;
    private const float maxAlphaValue = 1;

    public TransitionToDay(TimeOfDayManager timeOfDayManager)
    {
        this.timeOfDayManager = timeOfDayManager;
    }

    public void startAnimation(Image imageToAnimate)
    {

        imageToAnimate.color -= new Color(0, 0, 0, maxAlphaValue / 2.5f) * Time.deltaTime;
        timeOfDayManager.TimeOfDayPanel.Calendar.color -= new Color(0, 0, 0, maxAlphaValue / 2.5f) * Time.deltaTime;
        if (imageToAnimate.color.a <= 0)
        {
            endAnimation(imageToAnimate);
        }
    }

    public void endAnimation(Image imageToAnimate)
    {
        timeOfDayManager.StopAnimations();
        timeOfDayManager.DisablePanel();
    }

}

public class NoAnimation : ITimeOfDayStates
{
   
    public void endAnimation(Image imageToAnimate)
    {
        throw new NotImplementedException();
    }

    public void setDefaultColorValue(Color defaultValue)
    {
        throw new NotImplementedException();
    }

    public void startAnimation(Image imageToAnimate)
    {

    }
}

public class DisplayText : ITimeOfDayStates
{
    TimeOfDayManager timeOfDayManager;
    private const float maxAlphaValue = 1;


    public DisplayText(TimeOfDayManager timeOfDayManager)
    {
        this.timeOfDayManager = timeOfDayManager;

    }

     public void startAnimation(Image imageToAnimate)
    {

        timeOfDayManager.TimeOfDayPanel.Calendar.color += new Color(0, 0, 0, maxAlphaValue / 3) * Time.deltaTime;
        if (timeOfDayManager.TimeOfDayPanel.Calendar.color.a >= maxAlphaValue)
        {
            endAnimation(imageToAnimate);
        }
    }

    public void endAnimation(Image imageToAnimate)
    {
        timeOfDayManager.FadeToStartOfDay();
    }

}
