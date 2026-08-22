using System;

public interface ILife
{
    public float healthPrecent {get;}
    public int health {get;}   
    public event Action OnDie;
    public void Attack(int power);
}
