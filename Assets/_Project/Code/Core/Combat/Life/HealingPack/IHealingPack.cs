public interface IHealingPack
{
    public int heal {get;}
    public int healingPacks {get;}
    public void AddHealingPack();
    public void Heal(ILife life);
}
