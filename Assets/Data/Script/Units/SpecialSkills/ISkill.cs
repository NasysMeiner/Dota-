public interface ISkill
{
    public bool IsPurchased {  get; }
    public int PriceSkill { get; }

    public void UnlockSkill();
    public void InitData();
}
