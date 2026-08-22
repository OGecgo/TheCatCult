using System;
using UnityEngine;

public class StatePlay: IStateGame
{
    private event Action OnEnableEffect;
    private event Action OnDisableEffect;

    public StatePlay(Action OnEnableEffect, Action OnDisableEffect)
    {
        this.OnEnableEffect = OnEnableEffect;
        this.OnDisableEffect = OnDisableEffect;
    }

    public void Enter()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnEnableEffect.Invoke();
    }

    public void Exit()
    {
        OnDisableEffect.Invoke();
    }
}
