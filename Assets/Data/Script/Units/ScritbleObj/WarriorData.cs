using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/DataWarrior")]
public class WarriorData : ScriptableObject
{
    public TypeUnit Type;

    public string Name;

    [Header("Graphic arts")]

    public RuntimeAnimatorController Avatar;

    public Sprite Sprite;

    [Header("Skills")]
    public List<Skill> SkillList = new();

    [Header("LevelUp")]
    public List<StatUp> LevelUpList = new();

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

    [Range(0, 100f)]
    public float ProjectileBlockChance;

    public float TimeImmortaly = 0;

    [Header("Effect")]

    public Effect EffectDamage;

    public Effect EffectAttack;

    public Effect EffectDeath;

    public float TimeEffectDeath;

    [Header("Other")]

    public int Price;

    [Header("Stats")]
    public List<CurrentStat> CurrentStats = new();
    public List<Stat> Stats = new();
    public List<PriceStat> Prices = new();

    public virtual void LoadStat(WarriorData warriorData)
    {
        Type = warriorData.Type;
        Name = warriorData.Name;

        Avatar = warriorData.Avatar;
        Sprite = warriorData.Sprite;

        HealPoint = warriorData.HealPoint;
        AttackDamage = warriorData.AttackDamage;
        AttackRange = warriorData.AttackRange;
        VisibilityRange = warriorData.VisibilityRange;
        AttackSpeed = warriorData.AttackSpeed;
        Speed = warriorData.Speed;
        ApproximationFactor = warriorData.ApproximationFactor;
        ProjectileBlockChance = warriorData.ProjectileBlockChance;
        TimeImmortaly = warriorData.TimeImmortaly;

        EffectDamage = warriorData.EffectDamage;
        EffectAttack = warriorData.EffectAttack;
        EffectDeath = warriorData.EffectDeath;

        Price = warriorData.Price;

        CurrentStats.Clear();
        Stats.Clear();
        Prices.Clear();
        LevelUpList.Clear();
        SkillList.Clear();

        foreach (CurrentStat stat in warriorData.CurrentStats)
        {
            CurrentStat newStat = new();
            newStat.Type = stat.Type;
            newStat.CurrentLevel = stat.CurrentLevel;
            CurrentStats.Add(newStat);
        }

        foreach (Stat stat in warriorData.Stats)
            Stats.Add(stat);

        foreach (PriceStat stat in warriorData.Prices)
            Prices.Add(stat);

        foreach (StatUp stat in warriorData.LevelUpList)
            LevelUpList.Add(stat);

        if (Prices.Count < Stats.Count)
            throw new NotImplementedException("Not price stat");

        for (int i  = 0; i < Stats.Count; i++)
        {
            if(CurrentStats[i].CurrentLevel > Stats[i].Levels.Count)
                CurrentStats[i].CurrentLevel = Stats[i].Levels.Count;

            if (Prices[i].Price.Count == 0)
                Prices[i].Price.Add(100);
        }

        foreach(StatUp statUp in LevelUpList)
            if(statUp.IsPurchased)
                statUp.LevelUpStat(this);

        foreach (Skill skill in warriorData.SkillList)
            SkillList.Add(skill);
    }

    public void IncreaseLevel(ContainerPack containerPack)
    {
        HealPoint = containerPack.Stats[0].Levels[containerPack.CurrentStats[0].CurrentLevel - 1];
        AttackDamage = containerPack.Stats[1].Levels[containerPack.CurrentStats[1].CurrentLevel - 1];
        AttackSpeed = containerPack.Stats[2].Levels[containerPack.CurrentStats[2].CurrentLevel - 1];
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