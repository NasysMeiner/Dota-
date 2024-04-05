using UnityEngine;

public interface ISkill
{
    public Sprite Icon { get; }
    public bool IsPurchased {  get; }
    public int PriceSkill { get; }
    public TypeSkill TypeSkill { get; }

    public void UnlockSkill();
    public void InitData();
}
