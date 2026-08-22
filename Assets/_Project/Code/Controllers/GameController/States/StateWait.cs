using System;
using UnityEngine;

public class StateWait: IStateGame
{
    private event Action OnEnableEffect;
    private event Action OnDisableEffect;
    public StateWait(Action OnEnableEffect, Action OnDisableEffect)
    {
            this.OnEnableEffect = OnEnableEffect;
            this.OnDisableEffect = OnDisableEffect;
    }
    public void Enter()
    {
        if (OnDisableEffect != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            OnDisableEffect.Invoke();
        }
    }
    
    public void Exit()
    {
        if (OnEnableEffect != null) OnEnableEffect.Invoke();
    }

 
}
