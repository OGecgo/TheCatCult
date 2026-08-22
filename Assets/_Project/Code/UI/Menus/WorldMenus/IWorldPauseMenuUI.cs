using System;
using UnityEngine;

public interface IWorldPauseMenuUI
{
    public event Action OnPause;
    public event Action OnUnpause;
}
