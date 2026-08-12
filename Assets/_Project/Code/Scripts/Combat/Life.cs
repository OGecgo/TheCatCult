using System;

public class Life : ILife
{
    private int damageTaked;
    private LifeConf config;
    public event Action OnDie;
    
    public Life(LifeConf lifeConf)
    {
        damageTaked = 0;
        config = lifeConf;
    }

    public void Attack(int power)
    {
        damageTaked += power;
        if (config.health <= damageTaked) OnDie.Invoke();
    }
}
