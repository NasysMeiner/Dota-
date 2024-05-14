using UnityEngine;
using UnityEngine.UI;

public class HealPoint : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private IEntity _entity;

    private void OnDisable()
    {
        _entity.HealPointChange -= OnHealPointChange;
    }

    private void Start()
    {
        _entity.HealPointChange += OnHealPointChange;
    }

    private void OnHealPointChange(float healPoint)
    {
        _image.fillAmount = _entity.MaxHealPoint / healPoint;
    }
}
