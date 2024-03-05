using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private float _timeDelete = 4f;

    private Queue<IEntity> _queue = new();
    private bool _isActive = false;

    public void AddQueue(IEntity entity)
    {
        _queue.Enqueue(entity);
        entity.ChangePosition(transform.position);

        if (_queue.Count > 4 && _isActive == false)
            StartCoroutine(DeleteEntity());
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
}
