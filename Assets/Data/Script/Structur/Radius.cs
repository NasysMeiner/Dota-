using UnityEngine;

public class Radius : MonoBehaviour
{
    private float _radius;

    public void InitRadius(float radius)
    {
        _radius = radius;
        transform.localScale = new Vector3(_radius * 2, _radius * 2, 1);
    }

    public bool CheckInRadius(Vector3 point)
    {
        point.z = 0;

        return Mathf.Abs((point - transform.position).magnitude) <= _radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
