using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUnitView : MonoBehaviour
{
    [SerializeField] private List<ButtonStatus> _buttons = new List<ButtonStatus>();

    private RadiusSpawner _radiusSpawner;
    private ButtonStatus _currentButton;
    private int _currentId = -1;

    public void Init(RadiusSpawner radiusSpawner)
    {
        _radiusSpawner = radiusSpawner;
    }

    public void ChangeUnit(ButtonStatus button)
    {
        if (_currentButton != null)
            _currentButton.InActiveButton();

        if (button == _currentButton)
        {
            _currentButton = null;

            return;
        }

        _currentButton = button;
        _currentButton.ActiveButton();

        _currentId = SearchIdButton();
        _radiusSpawner.ChangeActiveUnit(_currentId);
    }

    private int SearchIdButton()
    {
        return _buttons.IndexOf(_currentButton);
    }
}
