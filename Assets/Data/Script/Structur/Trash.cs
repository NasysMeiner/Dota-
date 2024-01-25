using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private float _timeDelete = 2f;

    private Queue<IEntity> _queue = new Queue<IEntity>();
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
            //Debug.Log(_queue.Count);
            Destroy(entity.GameObject);

            if (_queue.Count <= 0)
                break;
        }

        _isActive = false;

        StopCoroutine(DeleteEntity());
    }
}
