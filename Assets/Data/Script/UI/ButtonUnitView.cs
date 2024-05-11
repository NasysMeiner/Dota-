using System.Collections.Generic;
using UnityEngine;

public class ButtonUnitView : PanelStat
{
    [SerializeField] private List<ButtonStatus> _buttons = new();

    private RadiusSpawner _radiusSpawner;
    private List<ViewSprite> _images;
    private ButtonStatus _currentButton;
    private int _currentId = -1;

    private void OnDisable()
    {
        if (_currentButton != null)
            _currentButton.InActiveButton();

        _currentButton = null;
        _currentId = -1;
        _radiusSpawner.ChangeActiveUnit(_currentId);
        DrawRadius();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (_buttons.Count > 0)
                ChangeUnit(_buttons[0]);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            if (_buttons.Count > 1)
                ChangeUnit(_buttons[1]);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            if (_buttons.Count > 2)
                ChangeUnit(_buttons[2]);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            if (_buttons.Count > 3)
                ChangeUnit(_buttons[3]);
        }
    }

    public void Init(RadiusSpawner radiusSpawner, List<ViewSprite> images)
    {
        _radiusSpawner = radiusSpawner;
        _images = images;

        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].SetName(_radiusSpawner.GetNameUnit(i));
            _buttons[i].SetPrice(_radiusSpawner.GetPriceUnit(i));
        }
    }

    public void StartGame()
    {
        foreach (var button in _buttons)
            button.OnButton();
    }

    public void ChangeUnit(ButtonStatus button)
    {
        if (button.Interectable == false)
            return;

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
        if (_currentButton != null)
            foreach (ViewSprite image in _images)
                image.Active();

        if (_currentButton == null)
            foreach (ViewSprite image in _images)
                image.InActive();
    }
}
