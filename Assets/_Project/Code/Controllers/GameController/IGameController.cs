using System;
using UnityEngine;

public interface IGameController
{
    public void DeathMode();
    public void PlayMode();
    public void PauseMode();
    public void WaitMode();

    public event Action OnDeath;
    public event Action OnPause;
    public event Action OnUnpause;
}
