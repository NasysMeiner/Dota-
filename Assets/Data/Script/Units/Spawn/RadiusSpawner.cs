using System.Collections.Generic;
using UnityEngine;

public class RadiusSpawner : MonoBehaviour
{
    //[SerializeField] private List<Unit> _units = new List<Unit>();
    [SerializeField] private Unit _prefabUnit;
    [SerializeField] private WarriorData _stats;
    //[SerializeField] private List<>

    [SerializeField] private float _cooldownSpawn = 2f;
    [SerializeField] private float _radius = 15.3f;

    private int _currentUnitId = -1;

    private bool _isReady = true;
    private float _time = 0;

    private void Update()
    {
        if (_isReady == false && _time >= _cooldownSpawn)
            _isReady = true;

        _time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnClick();
    }

    private void OnClick()
    {
        if (_isReady)
        {
            _isReady = false;

            RaycastHit hit;
            Ray myRay;

            myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(myRay.origin, myRay.direction * 10, Color.yellow);
            if (Physics.Raycast(myRay, out hit, 100))
            {
                MeshFilter filter = hit.collider.GetComponent(typeof(MeshFilter)) as MeshFilter;
                if (hit.collider.TryGetComponent(out Ground ground))
                {
                    Debug.Log(filter.gameObject.name + " Spawn");
                }
                else
                {
                    Debug.Log("Nononono");
                }
            }

            _time = 0;
        }
    }

    public void ChangeActiveUnit(int id)
    {
        _currentUnitId = id;
    }
}