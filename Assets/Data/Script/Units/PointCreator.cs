using UnityEngine;

public class PointCreator : MonoBehaviour
{
    [SerializeField] private bool _drawRadius;
    [SerializeField] private float _radius = 3f;

    public Vector3 CreateRangePoint
    {
        get
        {
            float randomX;
            float randomY;
            float maxRangeY;

            randomX = Random.Range(-_radius, _radius);

            if (randomX != 0)
                maxRangeY = Mathf.Sqrt(_radius - Mathf.Abs(transform.position.x - randomX));
            else
                maxRangeY = _radius;

            randomY = Random.Range(-maxRangeY, maxRangeY);

            Vector3 resultVector = new Vector3(randomX, randomY, 0);

            return resultVector;
        }
    }

    private void OnDrawGizmos()
    {
        if (_drawRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
