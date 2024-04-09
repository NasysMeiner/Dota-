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
    public Vector3 BiasShadow;
    public bool IsShadowBias;
    public Vector3 BiasHPBar;

    public float Scale;
    public float ScaleEffact;

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

    [Space]

    [Header("UI")]

    public Sprite Icon;

    private List<SkillCont> _skillConts = new();

    public List<SkillCont> SkillConts => _skillConts;

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
        BiasShadow = warriorData.BiasShadow;
        IsShadowBias = warriorData.IsShadowBias;
        BiasHPBar = warriorData.BiasHPBar;
        Scale = warriorData.Scale;
        ScaleEffact = warriorData.ScaleEffact;

        EffectDamage = warriorData.EffectDamage;
        EffectAttack = warriorData.EffectAttack;
        EffectDeath = warriorData.EffectDeath;
        ColorEffectDamage = warriorData.ColorEffectDamage;

        IsDoubleAttack = warriorData.IsDoubleAttack;

        Price = warriorData.Price;
        CurrentLevel = warriorData.CurrentLevel;

        Icon = warriorData.Icon;

        Stats.Clear();
        Prices.Clear();
        LevelUpList.Clear();
        SkillList.Clear();
        AllSkillList.Clear();
        SkillConts.Clear();

        foreach (Stat stat in warriorData.Stats)
            Stats.Add(stat);

        //foreach (PriceStat stat in warriorData.Prices)
        //    Prices.Add(stat);

        foreach (StatUp skill in warriorData.LevelUpList)
        {
            SkillCont newSkillCont = new();
            newSkillCont.LoadData(skill);
            SkillConts.Add(newSkillCont);

            AllSkillList.Add(skill);
            LevelUpList.Add(skill);

            Debug.Log(skill.IsPurchased + " " + name + " " + skill + "  _____--_____");
            if (skill.IsPurchased)
                skill.LevelUpStat(this);
        }

        foreach (int value in warriorData.Prices)
            Prices.Add(value);

        if (Prices.Count == 0)
            Prices.Add(100);

        //foreach (StatUp statUp in LevelUpList)
        //    if (statUp.IsPurchased)
        //        statUp.LevelUpStat(this);

        foreach (SkillData skill in warriorData.SkillList)
        {
            SkillCont newSkillCont = new();
            newSkillCont.LoadData(skill);
            SkillConts.Add(newSkillCont);

            AllSkillList.Add(skill);
            SkillList.Add(skill);
        }


        //Debug.Log(AllSkillList.Count + " " + name);
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

    public void UnlockSkill(int idSkill)
    {
        if (idSkill < SkillConts.Count)
            SkillConts[idSkill].UnlockSkill();

        if (SkillConts[idSkill].Skill.TypeSkill == TypeSkill.StatsUp)
        {
            StatUp statUp = SkillConts[idSkill].Skill as StatUp;
            statUp.LevelUpStat(this);
        }

    }

    public bool CheckLevelUp()
    {
        if (CurrentLevel + 1 <= MaxLevel)
            return true;

        return false;
    }

    public bool CheckUnlockSkill(int idSkill)
    {
        if (SkillConts[idSkill].IsUnlock == false)
            return true;

        return false;
    }

    public int GetPriceSkill(int idSkill)
    {
        if (idSkill < SkillConts.Count)
            return SkillConts[idSkill].Skill.PriceSkill;

        return 0;
    }

    public int GetPriceLevel()
    {
        if (CurrentLevel < Prices.Count)
            return Prices[CurrentLevel];
        else
            return Prices[Prices.Count - 1];
    }

    public int GetSkill(int id)
    {
        if (id < SkillConts.Count)
            if (SkillConts[id].IsUnlock)
                return 1;
            else
                return 0;

        return -1;
    }

    public void SkillUp()
    {
        float value = GetValueStat(TypeStat.HealthPoint, CurrentLevel);

        if (value != -1)
            HealPoint = value;

        value = GetValueStat(TypeStat.Damage, CurrentLevel);

        if (value != -1)
            AttackDamage = value;

        value = GetValueStat(TypeStat.AttackSpeed, CurrentLevel);

        if (value != -1)
            AttackSpeed = value;
    }

    public float GetValueStat(TypeStat typeStat, int level)
    {
        Stat currentStat = null;

        foreach (Stat stats in Stats)
            if (stats.Type == typeStat)
                currentStat = stats;

        if (currentStat == null)
            return -1;

        if (level <= currentStat.Levels.Count)
            return currentStat.Levels[level - 1];
        else
            return currentStat.Levels[currentStat.Levels.Count - 1];
    }
}

[System.Serializable]
public class Stat
{
    public TypeStat Type;
    public List<float> Levels = new();
}

[System.Serializable]
public class SkillCont
{
    public ISkill Skill;

    public bool IsUnlock;

    public void LoadData(ISkill skill)
    {
        Skill = skill;
        Debug.Log(skill + " " + skill.IsPurchased);
        IsUnlock = skill.IsPurchased;
    }

    internal void UnlockSkill()
    {
        IsUnlock = true;
    }
}