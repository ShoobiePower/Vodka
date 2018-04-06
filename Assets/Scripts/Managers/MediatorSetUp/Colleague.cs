using UnityEngine;
using System.Collections;

public abstract class Colleague : MonoBehaviour
{
    private IDirector director;
    protected IDirector Director { get { return director; } }

    public Colleague ()
    {
     
    }

    public void SetDirector(IDirector newDirector)
    {
        director = newDirector;
    }

    public virtual void StartPhase()
    {

    }

    public virtual void EndPhase()
    {

    }



}
