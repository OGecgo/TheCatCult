using UnityEngine;

// give gravity to character
// give controll movements
public interface ICharacterGravity: IUpdatable
{
    public float g {get;}
    public bool IsGrounded { get; }
    // push forward have no stop after n time 
    // after x push. should do -x push for stop
    
    // push forward automaticly make proper direction for power
    // e.g. Vector3.forward make character move forward
    public void PushForward(Vector3 power, float acceleration);
    // can used for jump
    public void PushUp(float power);
}


