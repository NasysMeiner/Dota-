using System.Collections.Generic;
using UnityEngine.Events;

public class Counter
{
    public List<IEntity> Entity = new List<IEntity>();

    public event UnityAction<IEntity> Delete;

    public void AddEntity(IEntity entity)
    {
        if (!Entity.Contains(entity))
            Entity.Add(entity);
    }

    public void DeleteEntity(IEntity entity)
    {
        if (Entity.Contains(entity))
        {
            Entity.Remove(entity);
            Delete?.Invoke(entity);
        }
    }
}
