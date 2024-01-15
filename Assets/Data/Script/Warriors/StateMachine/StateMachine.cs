using System;
using System.Collections.Generic;

public class StateMachine
{
    public State CurrentState { get; private set; }

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

    public void Update()
    {
        CurrentState?.Update();
    }
}
