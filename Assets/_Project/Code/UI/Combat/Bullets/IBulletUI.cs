using System;

public interface IBulletUI
{
    // currentbullets, bullets 
    public event Action<int, int> OnSetBullets;    
    // bullets
    public event Action<int> OnSetBunchOfBullets;
}
