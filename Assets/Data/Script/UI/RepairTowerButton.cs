using TMPro;
using UnityEngine;

public class RepairTowerButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Canvas _canvas;

    private RepairView _repairView;

    private string _name;
    private int _id;

    public bool IsActive { get; private set; }

    public void InitButton(RepairView repairView)
    {
        _repairView = repairView;
    }

    public void PlaceButton(string name, int id, int price, Vector3 position)
    {
        _name = name;
        _id = id;
        _priceText.text = price.ToString();
        transform.position = position;
        SetEnable();
    }

    public void Repair()
    {
        if(_repairView.TryRepairTower(_name, _id))
            SetDisable();
    }

    public void SetEnable()
    {
        _canvas.gameObject.SetActive(true);
        IsActive = true;
    }

    public void SetDisable()
    {
        _canvas.gameObject.SetActive(false);
        IsActive = false;
    }
}
