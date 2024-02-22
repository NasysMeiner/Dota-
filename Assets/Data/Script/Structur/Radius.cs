using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radius : MonoBehaviour
{
    private float _radius;

    private void Awake()
    {
        _radius = 4f;
    }

    public bool CheckInRadius(Vector3 point)
    {
        point.z = 0;

        return Mathf.Abs((point - transform.position).magnitude) <= _radius;
    }
}
