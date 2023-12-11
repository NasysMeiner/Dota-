using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public Building[] playerBuildings;
    private int gold = 0;
    public Text goldText;

    void Start()
    {
        StartCoroutine(EarnGoldCoroutine());
    }
    private IEnumerator EarnGoldCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            int totalIncome = CalculateTotalIncome();
            gold += totalIncome;

            UpdateGoldText();
            Debug.Log("Total Income: " + totalIncome);
            Debug.Log("Total Gold: " + gold);
        }
    }
    private int CalculateTotalIncome()
    {
        int totalIncome = 0;
        foreach (Building building in playerBuildings)
        {
            totalIncome += building.income;
            Debug.Log("Building Income: " + building.income);

        }
        return totalIncome;
    }
    private void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = gold.ToString();
        }
    }

    [System.Serializable]
    public class Building
    {
        public int income;
    }
}
