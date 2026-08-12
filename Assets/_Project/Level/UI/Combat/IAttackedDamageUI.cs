using System;

public interface IAttackedDamageUI
{
    public event Action OnSetAttacked;
    public event Action OnUnsetAttacked;
}
