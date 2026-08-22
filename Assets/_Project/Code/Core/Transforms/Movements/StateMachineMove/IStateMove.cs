using UnityEngine;

// the state move update set push on charactergravity (for )
public interface IStateMove: IUpdatable
{
    public Vector3 direction {set;}
}
