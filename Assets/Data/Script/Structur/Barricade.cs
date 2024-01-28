using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(MeshRenderer))]
public class Barricade : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _alphaColor;

    private Vector3 _mousePositionNearClipPlane;
    private float _screenCameraDistance;
    private NavMeshObstacle _obstacle;
    private MeshRenderer _renderer;
    private Color _standartColor;
    private Color _flyColor;

    public bool IsAccessible { get; private set; }

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _obstacle = GetComponent<NavMeshObstacle>();
        _standartColor = _renderer.material.color;
        _flyColor = _standartColor;
        _flyColor.a = _alphaColor;
        _screenCameraDistance = Camera.main.nearClipPlane + 9f;
    }

    private void OnMouseDown()
    {
        _obstacle.enabled = false;
        _renderer.material.color = _flyColor;
    }

    private void OnMouseDrag()
    {
        _mousePositionNearClipPlane = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenCameraDistance);
        transform.position = Camera.main.ScreenToWorldPoint(_mousePositionNearClipPlane);
    }

    private void OnMouseUp()
    {
        _obstacle.enabled = true;
        _renderer.material.color = _standartColor;
    }

    public void MakeAvailable()
    {
        IsAccessible = true;
    }

    public void MakeInaccessible()
    {
        IsAccessible = false;
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
    }
}
