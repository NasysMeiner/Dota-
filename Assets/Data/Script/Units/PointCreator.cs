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

            randomX = Random.Range(transform.position.x - _radius, transform.position.x + _radius);

            if (randomX != 0)
                maxRangeY = Mathf.Sqrt(Mathf.Abs(_radius - Mathf.Abs(transform.position.x - randomX)));
            else
                maxRangeY = _radius;

            randomY = Random.Range(transform.position.y - maxRangeY, transform.position.y + maxRangeY);

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
