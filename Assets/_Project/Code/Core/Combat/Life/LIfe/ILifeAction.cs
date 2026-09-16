using System;

public interface ILifeAction
{
    public event Action OnIsHit;
    public event Action OnIsHeal;
    public event Action<int> OnSetHealth;
}
