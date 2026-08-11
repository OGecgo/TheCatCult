using UnityEngine;

// give gravity to character
// give controll movements
public interface ICharacterGravity
{
    public float g {get;}
    public bool IsGrounded { get; }
    // push forward have no stop after n time 

    // horizotan movement automaticly take proper rotation for player
    // e.g. Vector3.forward makes character move forward

    // Linear Functions
    public void PushForwardToDesireSpeed(Vector3 desiredSpeed, float acceleration);
    public void PushForward(Vector3 power);
    // Constant Fucntion
    public void MoveForward(Vector3 constant);
    public void PushUp(float power);
}


