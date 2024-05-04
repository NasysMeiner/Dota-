using System.Collections.Generic;
using UnityEngine;

public class UnitStatsBlockRightView : UnitStatsBlock
{
    [SerializeField] private List<GameObject> _points;

    public override void UpdateView(int id)
    {
        base.UpdateView(id);

        if (id + _bias < _points.Count)
            id += _bias;
        else
            id = 1;

        gameObject.transform.position = _points[id].transform.position;
    }
}
