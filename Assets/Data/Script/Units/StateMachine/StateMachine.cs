using System;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class StateMachine
{
    private Transform _transform;

    public StateMachine(Transform transform)
    {
        _transform = transform;
    }

    public State CurrentState { get; private set; }
    public IEntity Target {  get; private set; }
    public Transform Transform => _transform;

    private Dictionary<Type, State> _states = new Dictionary<Type, State>();

    public void AddState(State state)
    {
        if (_states.TryGetValue(state.GetType(), out State s))
            throw new NotImplementedException();

        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : State
    {
        if (CurrentState != null && CurrentState.GetType() == typeof(T))
            return;

        if(_states.TryGetValue(typeof(T), out State newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }

    public void ChangeTarget(IEntity newTarget)
    {
        Target = newTarget;
    }

    public void Update()
    {
        if(CurrentState.IsWork)
            CurrentState?.Update();
    }
}
