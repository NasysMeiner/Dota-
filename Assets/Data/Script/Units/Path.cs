using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Point> _path = new List<Point>();

    public List<Point> StandartPath => _path;

    public void SearchPoint()
    {
        for (int i = 0; i < transform.childCount; i++)
            _path.Add(transform.GetChild(i).GetComponent<Point>());
    }
}
