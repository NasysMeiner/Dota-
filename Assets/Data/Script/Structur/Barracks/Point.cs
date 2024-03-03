using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;
    [SerializeField] private float _faultMin = 0.15f;
    [SerializeField] private float _faultMax = 1.15f;

    public float _leanght;
    public Vector2 _direction;

    private void Awake()
    {
        _direction = new Vector2(_leftPoint.position.x - _rightPoint.position.x, _leftPoint.position.y - _rightPoint.position.y);
    }

    public Vector3 RandomPoint
    {
        get
        {
            if(_direction.x == 0)
                return new Vector3(_leftPoint.position.x, Random.Range(_rightPoint.position.y, _leftPoint.position.y), 0);
            else if (_direction.y == 0)
                return new Vector3(Random.Range(_rightPoint.position.x, _leftPoint.position.x), _leftPoint.position.y, 0);

            if(Mathf.Abs(_leftPoint.position.x - _rightPoint.position.x) > Mathf.Abs(_leftPoint.position.y - _rightPoint.position.y))
            {
                float randomX = Random.Range(_rightPoint.position.x, _leftPoint.position.x);
                float Y = (randomX - _rightPoint.position.x) * (_leftPoint.position.y - _rightPoint.position.y) / (_leftPoint.position.x - _rightPoint.position.x) + _rightPoint.position.y;

                return new Vector3(randomX, Y, 0);
            }
            else
            {
                float randomY = Random.Range(_rightPoint.position.y, _leftPoint.position.y);
                float X = (randomY - _rightPoint.position.y) * (_leftPoint.position.x - _rightPoint.position.x) / (_leftPoint.position.y - _rightPoint.position.y) + _rightPoint.position.x;

                return new Vector3(X, randomY, 0);
            }
        }
    }

    public float MaxLeangth
    {
        get
        {
            float leangth = Mathf.Abs((_leftPoint.position - _rightPoint.position).magnitude);
            _leanght = leangth;
            return leangth;
        }
    }

    public bool CheckPointInLine(Vector3 position)
    {
        if (_direction.x == 0)
        {
            if (position.x >= _rightPoint.position.x - _faultMin && position.x <= _rightPoint.position.x + _faultMin)
            {
                float t = (position.y - _rightPoint.position.y) / _direction.y;

                if (t >= -_faultMin && t <= _faultMax)
                    return true;
            }

            return false;
        }
        else if (_direction.y == 0)
        {
            if (position.y >= _rightPoint.position.y - _faultMin && position.y <= _rightPoint.position.y + _faultMin)
            {
                float t = (position.x - _rightPoint.position.x) / _direction.x;

                if (t >= -_faultMin && t <= _faultMax)
                    return true;
            }

            return false;
        }

        float xValue = (position.x - _rightPoint.position.x) / _direction.x;
        float yValue = (position.y - _rightPoint.position.y) / _direction.y;

        if (xValue >= yValue - _faultMin && xValue <= yValue + _faultMin)
            if(xValue >= -_faultMin && xValue <= _faultMax)
                return true;

        return false;
    }
}
