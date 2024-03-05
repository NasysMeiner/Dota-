using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarracksMenu : MonoBehaviour
{
    public TextMeshProUGUI[] barracksHPTexts;
    public Button repairButton;

    private Barrack[] barracks;

    private void Start()
    {
        barracks = FindObjectsOfType<Barrack>();
        UpdateBarracksHP();
    }

    private void UpdateRepairButton()
    {
        bool anyBarrackIsDead = false;
        foreach (var text in barracksHPTexts)
        {
            int hp = int.Parse(text.text.Split('/')[0]);
            if (hp == 0)
            {
                anyBarrackIsDead = true;
                break;
            }
        }

        if (anyBarrackIsDead)
        {
            TextMeshProUGUI buttonText = repairButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = "Восстановить";
            }
        }
        else
        {
            TextMeshProUGUI buttonText = repairButton.GetComponentInChildren<TextMeshProUGUI>(); 
            if (buttonText != null)
            {
                buttonText.text = "Ремонт";
            }
        }
    }

    public void UpdateBarracksHP()
    {
        for (int i = 0; i < barracksHPTexts.Length; i++)
        {
            if (i < barracks.Length)
            {
                barracksHPTexts[i].text = $"{barracks[i].HealPoint}/{barracks[i].MaxHealPoint}";
            }
        }

        UpdateRepairButton();
    }

    public void OnRepairButtonClick()
    {
        foreach (var barrack in barracks)
        {
            if (barrack.HealPoint == 0)
            {
                barrack.Restore();
            }
            else
            {
                barrack.Repair();
            }
        }

        UpdateBarracksHP();
    }
}
