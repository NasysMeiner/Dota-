using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatView : MonoBehaviour
{
    [SerializeField] private TMP_Text _oldLevelText;
    [SerializeField] private TMP_Text _newLevelText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _button;

    private int _price;

    public void UpdateValues(StatContainer statContainer)
    {
        _oldLevelText.text = statContainer.CurrentLevel.ToString();
        _newLevelText.text = statContainer.NextLevel.ToString();
        _priceText.text = statContainer.Price.ToString();

        _price = statContainer.Price;
    }

    public void UpdateActiveMoney(int money)
    {
        if (money > _price)
            _button.interactable = true;
        else
            _button.interactable = false;
    }
}
