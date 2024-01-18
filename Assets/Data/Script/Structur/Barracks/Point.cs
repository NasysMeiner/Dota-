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

    public float MaxLeangth
    {
        get
        {
            float leangth = Mathf.Abs((_leftPoint.position - _rightPoint.position).magnitude);

            return leangth;
        }
    }
}
