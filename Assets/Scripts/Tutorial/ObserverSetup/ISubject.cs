/*
 * The Subject keeps track of all the Observers it's going to notify
 *      Also, it notifies the Observer
 * Example of Subject: Drink Making
 * 
	The subject
		There is one subject (usually. In our case, there'll be many subjects)
		Exist to notify observers
		register and unregister observers to themselves
 */

public interface ISubject
{
    void registerSelfToMediator();
    void unregisterSelfToMediator();

    void registerObserver(IObserver o);

    void unregisterObserver(IObserver o);

    void notifyObserver(Mediator.ActionIdentifiers CallingMethodName);
}

/*
 * Use this chunk of code for all subjects
 * I would like to have this be parented (if we're copying and pasting code), but that could get dicey too
 * Be sure to register self to mediator

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
 *
 */
