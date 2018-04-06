
/*
 * The Observer watches for an action to take place and notified ISubject
 * Example of Observer: Achievement Manager
 * 
	The Observer
		There are many observers (usually. In our cases there'll only be a couple)
		These have functionality behind them
		These wait and listen to the subject to notify them
		When they're notified, they do... something given the parameters you hand them
 * 
 * When an Observer is implemented, make sure to "register" it to the Subject in the constructor / start

    class Achievements : public Observer
    {
    public:
      virtual void onNotify(const Entity& entity, Event event)
      {
        switch (event)
        {
        case EVENT_ENTITY_FELL:
          if (entity.isHero() && heroIsOnBridge_)
          {
            unlock(ACHIEVEMENT_FELL_OFF_BRIDGE);
          }
          break;

          // Handle other events, and update heroIsOnBridge_...
        }
      }

    private:
      void unlock(Achievement achievement)
      {
        // Unlock if not already unlocked...
      }

      bool heroIsOnBridge_;
    };

 *
 */

public interface IObserver
{
    void notifyObserver(Mediator.ActionIdentifiers actionIdentifier);
    void regiesterSelfToMediator();
}