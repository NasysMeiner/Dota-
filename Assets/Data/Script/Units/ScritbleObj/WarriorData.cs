using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataWarrior")]
public class WarriorData : ScriptableObject
{
    public TypeUnit Type;

    [Header("Graphic arts")]

    public RuntimeAnimatorController Avatar;

    public Sprite Sprite;

    [Header("Stats")]

    [Range(1, 100000)]
    public float HealPoint;

    [Range(0.1f, 10000)]
    public float AttackDamage;

    [Range(0.1f, 100)]
    public float AttackRange;

    [Range(0.1f, 100)]
    public float VisibilityRange;

    [Range(0.1f, 100)]
    public float AttackSpeed;

    [Range(1, 100)]
    public float Speed;

    [Range(0.1f, 1)]
    public float ApproximationFactor;

    [Header("Effect")]

    public Effect EffectDamage;

    public Effect EffectAttack;

    [Header("Other")]

    public int Price;

    [Header("Stats")]
    public List<CurrentStat> CurrentStats = new();
    public List<Stat> Stats = new();
    public List<PriceStat> Prices = new();

    public virtual void LoadStat(WarriorData warriorData)
    {
        Type = warriorData.Type;

        Avatar = warriorData.Avatar;
        Sprite = warriorData.Sprite;

        HealPoint = warriorData.HealPoint;
        AttackDamage = warriorData.AttackDamage;
        AttackRange = warriorData.AttackRange;
        VisibilityRange = warriorData.VisibilityRange;
        AttackSpeed = warriorData.AttackSpeed;
        Speed = warriorData.Speed;
        ApproximationFactor = warriorData.ApproximationFactor;

        EffectDamage = warriorData.EffectDamage;
        EffectAttack = warriorData.EffectAttack;

        Price = warriorData.Price;

        CurrentStats = warriorData.CurrentStats;
        Stats = warriorData.Stats;
        Prices = warriorData.Prices;

        if (Prices.Count < Stats.Count)
            throw new NotImplementedException("Not price stat");

        for (int i  = 0; i < Stats.Count; i++)
        {
            if(CurrentStats[i].CurrentLevel > Stats[i].Levels.Count)
                CurrentStats[i].CurrentLevel = Stats[i].Levels.Count;

            if (Prices[i].Price.Count == 0)
                Prices[i].Price.Add(100);
        }
    }
}

[System.Serializable]
public class Stat
{
    public TypeStat Type;
    public List<int> Levels = new();
}

[System.Serializable]
public class CurrentStat
{
    public TypeStat Type;

    [Range(1, 1000)]
    public int CurrentLevel;
}

[System.Serializable]
public class PriceStat
{
    public TypeStat Type;
    public List<int> Price;
}