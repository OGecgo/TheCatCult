using System;

public interface IAttackedHelthUI
{
    // damage 
    public event Action<int> OnAttacked;
}
