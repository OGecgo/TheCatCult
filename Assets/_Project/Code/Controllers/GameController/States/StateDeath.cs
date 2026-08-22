using System;
using UnityEngine;

public class StateDeath: IStateGame
{
    private event Action OnDeath;

    public StateDeath(Action OnDeath)
    {
        this.OnDeath = OnDeath;
    }

    public void Enter()
    {
        if (OnDeath != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            OnDeath.Invoke();
        } 
    }

    // future add on revive 
    public void Exit()
    {
    }

}
