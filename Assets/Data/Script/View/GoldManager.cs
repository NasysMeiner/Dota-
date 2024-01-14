using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
  //  public Building[] playerBuildings;
    public List<Barracks> barracksList;
    public Bank playerBank;
    public Text goldText;

    void Start()
    {
        StartCoroutine(EarnGoldCoroutine());
    }

    private IEnumerator EarnGoldCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            int totalIncome = CalculateTotalIncome();
            playerBank.AddGold(totalIncome);

            UpdateGoldText();
            Debug.Log("Total Income: " + totalIncome);
            Debug.Log("Total Gold: " + playerBank.GetGold());
        }
    }

    private int CalculateTotalIncome()
    {
        int totalIncome = 0;
        foreach (Barracks barracks in barracksList)
        {
            totalIncome += barracks.GetIncome();
            Debug.Log("Barracks Income: " + barracks.GetIncome());
        }
        return totalIncome;
    }

    private void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = playerBank.GetGold().ToString();
        }
    }

    [System.Serializable]
    public class Building
    {
        public int income;
    }

    [System.Serializable]
    public class Bank
    {
        private int gold = 0;

        public void AddGold(int amount)
        {
            gold += amount;
        }

        public bool SpendGold(int amount)
        {
            if (gold >= amount)
            {
                gold -= amount;
                return true; // potratil
            }
            return false; //ne dostatochno
        }

        public int GetGold()
        {
            return gold;
        }
    }
}
