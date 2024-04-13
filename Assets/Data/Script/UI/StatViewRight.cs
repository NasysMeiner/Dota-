using TMPro;
using UnityEngine;

public class StatViewRight : StatView
{
    [SerializeField] private TMP_Text _clueText;

    public void ViewText(string text)
    {
        _clueText.text = text;
    }
}
