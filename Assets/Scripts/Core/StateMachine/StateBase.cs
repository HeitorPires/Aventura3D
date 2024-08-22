using UnityEngine;

public class StateBase
{
    public virtual void OnStateEnter(object o = null)
    {
        Debug.Log("OnStateEnter");
    }

    public virtual void OnStateStay()
    {
        Debug.Log("OnStateStay");
    }

    public virtual void OnStateExit()
    {
        Debug.Log(nameof(OnStateExit));
    }
}

