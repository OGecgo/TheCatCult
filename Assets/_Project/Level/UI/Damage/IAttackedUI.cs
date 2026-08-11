using System;

public interface IAttackedUI
{
    public event Action OnSetAttacked;
    public event Action OnUnsetAttacked;
}
