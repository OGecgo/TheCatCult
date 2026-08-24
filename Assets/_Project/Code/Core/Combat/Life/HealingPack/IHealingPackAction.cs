using System;
using UnityEngine;

public interface IHealingPackAction
{
    public event Action<int> OnUpdateHealibngPack;
}
