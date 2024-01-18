using System;
using UnityEngine;
using UnityEngine.Events;

public class Scout
{
    private Counter _counter;
    private float _visibilityRange;
    private Transform _transform;

    public IEntity Target { get; private set; }

    public event UnityAction<IEntity> ChangeTarget;

    public Scout(Counter counter, Transform transform, float visibilityRange)
    {
        _counter = counter;
        _transform = transform;
        _visibilityRange = visibilityRange;
        Target = null;
        _counter.Delete += OnDelete;
    }

    public void LateUpdate()
    {
        SearchEntity();
        CheckVisibleRange();
    }

    public void OnDisable()
    {
        _counter.Delete -= OnDelete;
    }

    private void SearchEntity()
    {
        if (_counter != null && Target == null)
        {
            float length;
            float minLenght = -1;

            foreach (var entity in _counter.Entity)
            {
                if (entity == null)
                    continue;

                length = Math.Abs((_transform.position - entity.Position).magnitude);

                if (length <= _visibilityRange && (length <= minLenght || minLenght == -1))
                {
                    minLenght = length;
                    Target = entity;
                    ChangeTarget?.Invoke(Target);
                }
            }
        }
    }

    private void CheckVisibleRange()
    {
        if (_counter != null && _transform != null)
        {
            if (Target != null)
            {
                float length = Math.Abs((_transform.position - Target.Position).magnitude);

                if (length > _visibilityRange)
                {
                    Target = null;
                    ChangeTarget?.Invoke(Target);
                }
            }
        }
    }

    private void OnDelete(IEntity entity)
    {
        if (Target != null && Target == entity)
        {
            Target = null;
            ChangeTarget?.Invoke(Target);
        }
    }
}
