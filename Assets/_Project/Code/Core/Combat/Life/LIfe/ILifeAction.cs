using System;

public interface ILifeAction
{
    public event Action OnIsHit;
    public event Action<int> OnSetHealth;
}
