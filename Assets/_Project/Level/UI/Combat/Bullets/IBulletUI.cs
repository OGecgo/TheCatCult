using System;

public interface IBulletUI
{
    public event Action<int, int> OnSetBullets;    
}
