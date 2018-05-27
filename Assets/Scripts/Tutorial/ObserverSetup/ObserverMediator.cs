using System.Collections.Generic;

/*THE PURPOSE OF THIS CLASS: To link up all Subjects and Observers to each other
 * The Subjects still know about the Observers, the Obsevers still do not know about the Subjects (just like observer)
 * This chunk of code replaces the "Outside" Code part of the Observer pattern
 * It assumes the Observer will deal with its own problems when it's notified
 * 
 * NOTE: I could allow the Mediator to notify all the observers. This prevents the observers from knowing about the
 *      subjects AND the subjects from knowing about the observers. There'd also be less code.
 *      However, since the method call to "NotifyAllObservers" would be public static, that could... ah... dangerous
 */

public static class Mediator
{
    public enum ActionIdentifiers { DRINK_SERVED, INGREDIENT_USED, CONVERSATION_ENDED, COUNTDOWN_ENDED, PATRON_LEFT, DAY_STARTED,
    MAP_OPEN, SEND_ON_QUEST_CLICKED, WOODLANDS_CLICKED, HOME_CLICKED, MOUNTAIN_CLICKED, LAKESIDE_CLICKED, END_DAY_FADE_OUT
    }

    //HashSets prevent duplicates (it's also faster than lists when over ~19 elements)
    static HashSet<ISubject> subjects = new HashSet<ISubject>();
    static HashSet<IObserver> observers = new HashSet<IObserver>();

    //Register the Observers with all the subjects. Add it to the list of Observers we're tracking
    public static void Register(IObserver observerToRegister)
    {
        foreach(ISubject s in subjects)
        {
            s.registerObserver(observerToRegister);
        }

        observers.Add(observerToRegister);
    }

    //Register all observers with the subject, then register the subject
    public static void Register(ISubject subjectToRegister)
    {
        foreach (IObserver o in observers)
        {
            subjectToRegister.registerObserver(o);
        }

        subjects.Add(subjectToRegister);
    }

    //Unregister the Observer with all the subjects. Remove it from the list of Observers we're tracking
    public static void Unregister(IObserver observerToUnregister)
    {
        foreach (ISubject s in subjects)
        {
            s.unregisterObserver(observerToUnregister);
        }

        observers.Remove(observerToUnregister);
    }

    //Unregister all observers with the subject, then unregister the subject
    public static void Unregister(ISubject subjectToUnregister)
    {
        foreach (IObserver o in observers)
        {
            subjectToUnregister.unregisterObserver(o);
        }
        subjects.Remove(subjectToUnregister);
    }

    public static void GetRidOfObservers()
    {
        subjects.Clear();
        observers.Clear();
    }
}


/*
 * Use this chunk of code for all subjects
 * I would like to parent things, but it doesn't always make sense for this to have a parent.
 * #region Observer Pattern

    List<IObserver> observers = new List<IObserver>();

    public void registerObserver(IObserver observerToAdd)
    {
        observers.Add(observerToAdd);
    }

    public void unregisterObserver(IObserver observerToRemove)
    {
        observers.Remove(observerToRemove);
    }

    public void notifyObserver()
    {
        for (int i = 0; i < observers.Count; i++)
        {
            observers[i].notifyObserver(this);
        }
    }

    public void registerSelfToMediator()
    {
        Mediator.RegisterSubject(this);
    }

    public void unregisterSelfToMediator()
    {
        Mediator.UnregisterSubject(this);
    }
 *  #endregion 
 */