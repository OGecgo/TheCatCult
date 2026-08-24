using System;
using UnityEngine;

public interface IBulletsAction
{
    public event Action<int, int> OnSetBullets;
    public event Action<int> OnSetBunchOfBullets;
    public event Action OnReloadBullets;
}
