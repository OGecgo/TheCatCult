using System;

public class Life : ILife
{
    private int damageTaked;
    private LifeConf config;
    public event Action OnDie;

    public float healthPrecent {get{return (config.health - damageTaked)/(float)config.health;}}
    public int health { get{return config.health - damageTaked;} }
    
    public Life(LifeConf lifeConf)
    {
        damageTaked = 0;
        config = lifeConf;
    }

    public void Attack(int power)
    {
        damageTaked += power;
        if (damageTaked < 0) damageTaked = 0;
        if (config.health <= damageTaked) OnDie.Invoke();
    }
}
