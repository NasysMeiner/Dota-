using UnityEngine;

public class AllocableObject : MonoBehaviour
{
    [SerializeField] private Barrack _barrack;
    [Space]
    [SerializeField] private Selection _selection;
    [SerializeField] private TypeBlockView _blockViewType;
    [Space]
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _pressedColor;

    private bool _isPressed = false;
    private bool _isActive = false;
    private bool _isWorkSelection = true;

    public int GetUnitId => _barrack.StartIdUnit;
    public TypeBlockView TypeBlockView => _blockViewType;

    private void OnMouseOver()
    {
        if(_isPressed == false && _isActive == false)
        {
            _selection.ChangeColor(_normalColor);
            _selection.gameObject.SetActive(true);
        }  
    }

    private void OnMouseExit()
    {
        if(_isPressed == false && _isActive == false)
        {
            _selection.ChangeColor(_normalColor);
            _selection.gameObject.SetActive(false);  
        }   
    }

    private void OnMouseDrag()
    {
        if(_isPressed == false)
        {
            _isPressed = true;
            _selection.ChangeColor(_pressedColor);
            _selection.gameObject.SetActive(true);
        }
    }
    private void OnMouseUp()
    {
        if (_isPressed)
        {
            _selection.ChangeColor(_normalColor);
            _isPressed = false;

            if (_isActive == false)
                _selection.gameObject.SetActive(false);
        }
    }

    public void SetOffSelection()
    {
        _normalColor.a = 0;
        _pressedColor.a = 0;
    }

    public void SetOnSelection()
    {
        _normalColor.a = 255;
        _pressedColor.a = 255;
    }

    public void SetEnable()
    {
        _isActive = true;
        _selection.gameObject.SetActive(true); 
        _selection.ChangeColor(_normalColor);
    }

    public void SetDisable()
    {
        _isActive = false;
        _selection.gameObject.SetActive(false);
    }
}
