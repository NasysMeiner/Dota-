using System.Collections.Generic;
using UnityEngine;

public class ButtonUnitView : MonoBehaviour
{
    [SerializeField] private GameObject _spellSpwanZone;
    [SerializeField] private List<ButtonStatus> _buttons = new();

    private RadiusSpawner _radiusSpawner;
    private List<ViewSprite> _images;
    private ButtonStatus _currentButton;
    private int _currentId = -1;

    public void Init(RadiusSpawner radiusSpawner, List<ViewSprite> images)
    {
        _spellSpwanZone.SetActive(false);
        _radiusSpawner = radiusSpawner;
        _images = images;

        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].SetName(_radiusSpawner.GetNameUnit(i));
            _buttons[i].SetPrice(_radiusSpawner.GetPriceUnit(i));
        }
    }

    public void ChangeUnit(ButtonStatus button)
    {
        if (_currentButton != null)
            _currentButton.InActiveButton();

        if (button == _currentButton)
        {
            _currentButton = null;
            _currentId = -1;
            _radiusSpawner.ChangeActiveUnit(_currentId);
            DrawRadius();

            return;
        }

        _currentButton = button;
        _currentButton.ActiveButton();

        _currentId = SearchIdButton();
        DrawRadius();
        _radiusSpawner.ChangeActiveUnit(_currentId);
    }

    private int SearchIdButton()
    {
        return _buttons.IndexOf(_currentButton);
    }

    private void DrawRadius()
    {
        _spellSpwanZone.SetActive(false);

        if (_currentButton != null)
            foreach (ViewSprite image in _images)
                image.Active();

        if (_currentButton == null)
            foreach (ViewSprite image in _images)
                image.InActive();
    }

    private void HideRadius()
    {
        foreach (ViewSprite image in _images)
            image.InActive();
    }

    public void DrawSpellZone(ButtonStatus button)
    {
        _spellSpwanZone.SetActive(true);
        HideRadius();

        if (_currentButton != null)
            _currentButton.InActiveButton();

        if (button == _currentButton)
        {
            _currentButton = null;
            _currentId = -1;

            _spellSpwanZone.SetActive(false);

            return;
        }

        _currentButton = button;
        _currentButton.ActiveButton();
    }
}
