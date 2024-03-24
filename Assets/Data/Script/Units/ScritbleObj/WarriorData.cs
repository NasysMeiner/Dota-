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

    public Vector3 Bias;

    public float Scale;

    public Sprite Sprite;

    [Header("Skills")]
    public List<SkillData> SkillList = new();

    [Header("LevelUp")]
    public List<StatUp> LevelUpList = new();
    public Color ColorEffectDamage = Color.red;

    public List<ISkill> AllSkillList = new();

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

    public bool IsDoubleAttack = false;

    [Header("Effect")]

    public Effect EffectDamage;

    public Effect EffectAttack;

    public Effect EffectDeath;

    public float TimeEffectDeath;

    [Header("Other")]

    public int Price;

    [Header("Stats")]
    public int CurrentLevel = 1;
    public int MaxLevel = 3;
    public List<Stat> Stats = new();
    public List<int> Prices = new();

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
        Bias = warriorData.Bias;
        Scale = warriorData.Scale;

        EffectDamage = warriorData.EffectDamage;
        EffectAttack = warriorData.EffectAttack;
        EffectDeath = warriorData.EffectDeath;
        ColorEffectDamage = warriorData.ColorEffectDamage;

        Price = warriorData.Price;
        CurrentLevel = warriorData.CurrentLevel;

        Stats.Clear();
        Prices.Clear();
        LevelUpList.Clear();
        SkillList.Clear();
        AllSkillList.Clear();

        foreach (Stat stat in warriorData.Stats)
            Stats.Add(stat);

        //foreach (PriceStat stat in warriorData.Prices)
        //    Prices.Add(stat);

        foreach (StatUp stat in warriorData.LevelUpList)
        {
            AllSkillList.Add(stat);
            LevelUpList.Add(stat);
        }

        foreach (int value in warriorData.Prices)
            Prices.Add(value);

        if (Prices.Count == 0)
            Prices.Add(100);

        foreach (StatUp statUp in LevelUpList)
            if (statUp.IsPurchased)
                statUp.LevelUpStat(this);

        foreach (SkillData skill in warriorData.SkillList)
        {
            AllSkillList.Add(skill);
            SkillList.Add(skill);
        }

        

        SkillUp();
    }

    public bool LevelUp()
    {
        if (CurrentLevel + 1 <= MaxLevel)
        {
            CurrentLevel++;
            SkillUp();

            return true;
        }

        return false;
    }

    public void SkillUp()
    {
        int value = GetValueStat(TypeStat.HealthPoint, CurrentLevel);

        if (value != -1)
            HealPoint = value;

        value = GetValueStat(TypeStat.Damage, CurrentLevel);

        if (value != -1)
            AttackDamage = value;

        value = GetValueStat(TypeStat.AttackSpeed, CurrentLevel);

        if (value != -1)
            AttackSpeed = value;
    }

    public int GetValueStat(TypeStat typeStat, int level)
    {
        Stat currentStat = null;

        foreach (Stat stats in Stats)
            if (stats.Type == typeStat)
                currentStat = stats;

        if (currentStat == null)
            return -1;

        if (currentStat.Levels.Count <= level)
            return currentStat.Levels[level - 1];
        else
            return currentStat.Levels[currentStat.Levels.Count - 1];
    }

    //public void IncreaseLevel(ContainerPack containerPack)
    //{
    //    HealPoint = containerPack.Stats[0].Levels[containerPack.Currentevel[0].CurrentLevel - 1];
    //    AttackDamage = containerPack.Stats[1].Levels[containerPack.Currentevel[1].CurrentLevel - 1];
    //    AttackSpeed = containerPack.Stats[2].Levels[containerPack.Currentevel[2].CurrentLevel - 1];
    //}
}

[System.Serializable]
public class Stat
{
    public TypeStat Type;
    public List<int> Levels = new();
    public List<int> Price = new();
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