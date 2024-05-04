using System.Collections.Generic;
using UnityEngine;

public class SelectorPointSpawner : MonoBehaviour
{
    [SerializeField] private List<Point> _points;

    public Vector3 GetPointSpawn(TypeUnit typeUnit, int idLane)
    {
        return _points[idLane].GetPointInBox(typeUnit);
    }
}
