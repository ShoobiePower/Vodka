using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TimeOfDayManager : Colleague, ISubject
{

    private int dayNumber;

    private float positionForSliderToMoveTo;

    [SerializeField]
    float howLongIsLerp;


    [SerializeField] TimeOfDayPanel timeOfDayPanel;
    public TimeOfDayPanel TimeOfDayPanel { get { return timeOfDayPanel; } }


    #region AnimationStates
    ITimeOfDayStates changeToNight;
    ITimeOfDayStates changeToDay;
    ITimeOfDayStates pullUpText;
    ITimeOfDayStates stopAnimations;

    ITimeOfDayStates currentTimeOfDay;
    #endregion


    private void Start()
    {
        changeToDay = new TransitionToDay(this);
        changeToNight = new TransitionToNight(this);
        pullUpText = new DisplayText(this);
        stopAnimations = new NoAnimation();
        currentTimeOfDay = stopAnimations;

        registerSelfToMediator();

        dayNumber = 1; // HACK
        incrementDayCount(); 

    }

    private void Update()
    {
        currentTimeOfDay.startAnimation(timeOfDayPanel.gameObject.GetComponent<Image>());
    }

    #region animationFunctions
    public void FadeToEndDay()
    {
        EnablePanel();
        currentTimeOfDay = changeToNight;      
    }

    public void FadeToStartOfDay()
    {
        currentTimeOfDay = changeToDay;
    }

    public void fadeInText()
    {
        currentTimeOfDay = pullUpText;
    }

    public void StopAnimations()
    {
        currentTimeOfDay = stopAnimations;
    }

    public void EnablePanel()
    {
        timeOfDayPanel.gameObject.SetActive(true);
    }

    public void DisablePanel()
    {
        timeOfDayPanel.gameObject.SetActive(false);
    }
    #endregion


    public void incrementDayCount()
    {
        dayNumber++;
        timeOfDayPanel.setDateText("Day " + dayNumber);
        notifyObserver(Mediator.ActionIdentifiers.DAY_STARTED);
        
    }

    public void progressSundownMeter()
    {
        positionForSliderToMoveTo++;
    }

    public override void EndPhase() 
    {
        notifyObserver(Mediator.ActionIdentifiers.END_DAY_FADE_OUT);
        Director.EndPhase(this);
    }

    public void SignalEndOfFadeInAnimation()
    {
        Director.ActivateBODProps();
    }

    #region Observer Pattern

    List<IObserver> observers = new List<IObserver>();

    public void registerObserver(IObserver observerToAdd)
    {
        observers.Add(observerToAdd);
    }

    public void unregisterObserver(IObserver observerToRemove)
    {
        observers.Remove(observerToRemove);
    }

    public void notifyObserver(Mediator.ActionIdentifiers ActionIdentifier)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].notifyObserver(ActionIdentifier);
        }
    }

    public void registerSelfToMediator()
    {
        Mediator.Register(this);
    }

    public void unregisterSelfToMediator()
    {
        Mediator.Unregister(this);
    }
    #endregion

}
