public class HealingPack: IHealingPack
{
    private HealingPackConf config;

    public int heal {get {return config.heal;}}
    public int healingPacks {get; private set;}

    public HealingPack(HealingPackConf healingPackConf)
    {
        config = healingPackConf;
        healingPacks = config.healingPacks;
    }

    public void AddHealingPack()
    {
        healingPacks++;
    }

    public void Heal(ILife life)
    {
        if (healingPacks > 0)
        {
            life.Attack(-config.heal);
            healingPacks--;
        }
    }
}
