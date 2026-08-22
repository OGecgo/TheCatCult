using System;

public interface IHealthUI
{
    // damage 
    public event Action<int> OnSetLife;
}
