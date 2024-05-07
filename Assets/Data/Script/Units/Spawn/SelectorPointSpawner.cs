using System.Collections.Generic;
using UnityEngine;

public class SelectorPointSpawner : MonoBehaviour
{
    [SerializeField] private List<Point> _points;
    [SerializeField] private Point _defPoint;

    public Vector3 GetPointSpawn(TypeUnit typeUnit, int idLane, TypeGroup typeGroup = TypeGroup.AttackType)
    {
        if (idLane == 4)
            return _defPoint.GetPointInBox(typeUnit);

        return _points[idLane].GetPointInBox(typeUnit);
    }
}
