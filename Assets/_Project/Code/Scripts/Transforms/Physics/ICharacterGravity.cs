using UnityEngine;

// give gravity to character
// give controll movements
public interface ICharacterGravity: IUpdatable
{
    public float g {get;}
    public bool IsGrounded { get; }
    // push forward have no stop after n time 

    // horizotan movement automaticly take proper rotation for player
    // e.g. Vector3.forward makes character move forward

    // smothly change from current spedd to desired speed from acceleration
    public void SetOfDesiredSpeedForward(Vector3 desiredSpeed, float acceleration);
    // simple changes velocity    
    public void PushForward(Vector3 power);
    public void PushUp(float power);
}


