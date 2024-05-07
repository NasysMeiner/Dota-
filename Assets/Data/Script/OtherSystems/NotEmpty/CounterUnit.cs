using System.Collections.Generic;
using UnityEngine;

public class CounterUnit : MonoBehaviour
{
    [SerializeField] private List<LineArea> _lines;

    public int GetDangerLine(TypeGroup typeGroup = TypeGroup.AttackType)
    {
        if (typeGroup == TypeGroup.DefType)
            return 4;

        int maxWeight = 0;
        int idLine = 0;

        for(int i = 0; i < _lines.Count; i++)
        {
            if (_lines[i].TotalLineWeight < maxWeight)
            {
                maxWeight = _lines[i].TotalLineWeight;
                idLine = i;
            }
        }

        if(maxWeight == 0)
            idLine = Random.Range(0, _lines.Count);

        return idLine;
    }
}
