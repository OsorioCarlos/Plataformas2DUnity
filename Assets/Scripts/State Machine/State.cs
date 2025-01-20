using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> : MonoBehaviour
{
    protected T controller;

    public virtual void OnEnterState(T controller)
    {
        if (this.controller == null)
        {
            this.controller = controller;
        }
    }

    public abstract void OnUpdateState();

    public abstract void OnFixedUpdateState();

    public abstract void OnExitState();
}
