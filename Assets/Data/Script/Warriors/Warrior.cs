using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Warrior : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private Transform _point;

    private NavMeshAgent _meshAgent;
    private StateMachine _stateMachine;

    private void Start()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        //StartWarrior();
        InitWarrior(_path);
    }

    private void FixedUpdate()
    {
        if( _stateMachine.CurrentState != null)
            _stateMachine.Update();
    }

    public void InitWarrior(Path path)
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _path = path;
        _stateMachine = new StateMachine();

        _stateMachine.AddState(new WalkWarriorState(_stateMachine, transform, _path, _meshAgent));

        _stateMachine.SetState<WalkWarriorState>();
    }

    //public void StartWarrior()
    //{
    //    _meshAgent.SetDestination(_point.position);
    //}
}
