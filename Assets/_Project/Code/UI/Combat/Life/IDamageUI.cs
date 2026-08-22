using System;

public interface IDamageUI
{
    public event Action OnSetAttacked;
    public event Action OnUnsetAttacked;
}
