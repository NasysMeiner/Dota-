using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PathBuilder : MonoBehaviour
{
    [SerializeField] private float _timeWait = 1f;
    [SerializeField] private Transform _target;

    private NavMeshAgent _agent;

    private bool _isWork = false;
    private NavMeshPath _path;
    private float _time = 0;

    public bool PathStatus { get; private set; }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_isWork && _time >= _timeWait)
        {
            _time = 0;
            CalculeitPath();
        }
    }

    public void InitPathBuilder()
    {
        _agent = GetComponent<NavMeshAgent>();
        PathStatus = false;
        _path = new NavMeshPath();
    }

    public void Enable()
    {
        _isWork = true;
    }

    public void Disable()
    {
        _isWork = false;
    }

    public void Check()
    {
        Debug.Log(_path.status);
    }

    private void CalculeitPath()
    {
        _agent.CalculatePath(_target.position, _path);
        PathStatus = _path.status == NavMeshPathStatus.PathComplete;
    }
}
