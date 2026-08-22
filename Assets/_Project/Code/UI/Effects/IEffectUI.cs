using System;
using UnityEngine;

public interface IEffectUI
{
    public event Action OnEnableEffect;
    public event Action OnDisableEffect;
}
