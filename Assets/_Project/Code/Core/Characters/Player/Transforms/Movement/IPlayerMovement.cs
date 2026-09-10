using System;
using UnityEngine;

public interface IPlayerMovement
{
    public event Action OnWalk;
    public event Action OnStopWalk;
    public event Action OnRun;
    public event Action OnStopRun;

    public event Action OnJump;
    public event Action OnTouchGround;
}
