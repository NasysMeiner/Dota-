using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;

    public Vector3 RandomPoint
    {
        get
        {
            Vector3 random = new Vector3(Random.Range(_leftPoint.position.x, _rightPoint.position.x), Random.Range(_leftPoint.position.y, _rightPoint.position.y), 0);

            return random;
        }
    }
}
