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

    private void Start()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        StartWarrior();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Yes");
    }

    public void InitWarrior(Path path)
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _path = path;
    }

    public void StartWarrior()
    {
        _meshAgent.SetDestination(_point.position);
    }
}
