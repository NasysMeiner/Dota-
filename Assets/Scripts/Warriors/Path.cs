using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Point> _path;

    public List<Point> StandartPath => _path;
}
