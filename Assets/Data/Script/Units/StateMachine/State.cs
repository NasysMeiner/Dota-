using UnityEngine.Events;

public abstract class State
{
    protected readonly StateMachine _stateMachine;
    protected bool _isWork = false;

    public event UnityAction StateActive;

    public string StateName { get; protected set; } //временно

    public bool IsWork => _isWork;

    public State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Update() { }

    protected void StartAnimation()
    {
        StateActive?.Invoke();
    }
}
