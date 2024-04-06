using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private float _timeDelete = 4f;
    [SerializeField] private float _timeWaitDeath;
    [SerializeField] private List<Castle> _castleList;

    private Queue<IEntity> _queue = new();
    private bool _isActive = false;

    public void AddQueue(IEntity entity)
    {
        _queue.Enqueue(entity);

        if (_queue.Count > 4 && _isActive == false)
            StartCoroutine(DeleteEntity());
    }

    public void WriteUnit(Unit unit)
    {
        unit.Died += OnDied;
    }

    private IEnumerator DeleteEntity()
    {
        _isActive = true;

        while (true)
        {
            yield return new WaitForSeconds(_timeDelete);

            IEntity entity = _queue.Dequeue();
            Destroy(entity.GameObject);

            if (_queue.Count <= 0)
                break;
        }

        _isActive = false;

        StopCoroutine(DeleteEntity());
    }

    private void OnDied(Unit unit)
    {
        unit.Died -= OnDied;

        if (unit.EnemyCounter == _castleList[0].EnemyCounter)
            _castleList[0].Counter.DeleteEntity(unit);
        else
            _castleList[1].Counter.DeleteEntity(unit);

        StartCoroutine(WaitTimeDeathEffect(unit));
    }

    private IEnumerator WaitTimeDeathEffect(Unit unit)
    {
        yield return new WaitForSeconds(_timeWaitDeath);

        AddQueue(unit);
        unit.ChangePosition(transform.position);
    }
}
