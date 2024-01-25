using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected readonly StateMachine _stateMachine;
    protected bool _isWork = false;
    

    public string StateName {  get; protected set; } //временно

    public bool IsWork => _isWork;

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }
}
