using System;
using UnityEngine;

public class StatePause: IStateGame
{
    private event Action OnPause;
    private event Action OnUnpause;
    public StatePause(Action OnPause, Action OnUnpause)
    {
        this.OnPause = OnPause;
        this.OnUnpause = OnUnpause;
    }

    public void Enter()
    {
        if (OnPause != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            OnPause.Invoke();            
        }

    }

    public void Exit()
    {
        if (OnUnpause != null) OnUnpause.Invoke();   
    }


}
