using System;

public interface ILife
{   
    public event Action OnDie;
    public void Attack(int power);
}
