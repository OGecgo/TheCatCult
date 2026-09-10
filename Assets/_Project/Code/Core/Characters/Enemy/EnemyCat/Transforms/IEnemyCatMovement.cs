using System;

public interface IEnemyCatMovement
{
    public enum TypeMovement  { RUN, WALK, WHAIT }
    public TypeMovement typeMovement {set;}

    public event Action OnWalk;
    public event Action OnStopWalk;
    public event Action OnRun;
    public event Action OnStopRun;
    public event Action OnTouchGround;
}
