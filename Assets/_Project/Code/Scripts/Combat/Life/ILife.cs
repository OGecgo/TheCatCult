using System;

public interface ILife
{
    public int health {get;}   
    public event Action OnDie;
    public void Attack(int power);
}
