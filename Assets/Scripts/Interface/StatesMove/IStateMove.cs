using UnityEngine;

public interface IStateMove
{
    public Vector3 moveTo { set; }

    public void Enter();
    public void Update();
    public void Exit(); 
}
