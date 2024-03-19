using System;
using System.Collections.Generic;

public class StateMachine
{
    private readonly Unit _warrior;

    public StateMachine(Unit warrior)
    {
        _warrior = warrior;
    }

    public string CurrentTextState { get; private set; }//временно

    public State CurrentState { get; private set; }
    public Unit Warrior => _warrior;

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

        if (_states.TryGetValue(typeof(ArcherAttackState), out State state) && typeof(T) == typeof(AttackState))
            SetState<ArcherAttackState>();

        if (_states.TryGetValue(typeof(T), out State newState))
        {
            //Debug.Log("new: " + newState + " old: " + CurrentState + "   " + _warrior._healPoints);
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
            CurrentTextState = CurrentState.StateName;
        }
    }

    public void Update()
    {
        if (CurrentState.IsWork)
            CurrentState?.Update();
    }

    internal void Die()
    {
        SetState<DeathState>();
    }
}
