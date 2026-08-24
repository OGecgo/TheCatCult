using System;

public interface ILifeAction
{
    public event Action OnIsAttacked;
    public event Action<int> OnSetHealth;
}
