using System;

public class Life : ILife
{
    private int health;
    public event Action OnDie;
    
    public Life(LifeConf lifeConf)
    {
        health = lifeConf.health;
    }

    public void Attack(int power)
    {
        health -= power;
        if (health <= 0) OnDie.Invoke();
    }
}
