using System.Data.Common;
using System.Runtime.InteropServices;
using UnityEngine;

public class CharacterGravity: ICharacterGravity
{
    private float _g = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;


    public bool IsGrounded { get { return controller.isGrounded; } }

    public float g { get { return _g; }}

    // character should be body. not camera
    public CharacterGravity(CharacterController cc)
    {
        this.controller = cc;
        velocity = Vector3.zero;
    }


    public void SetOfDesiredSpeedForward(Vector3 desiredSpeed, float acceleration)
    {
        // rotation * direction + (dont lost velocity.y)
        Vector3 direction = controller.transform.rotation * desiredSpeed;
        Vector3 newVelocity = Vector3.MoveTowards(velocity, direction, acceleration * Time.deltaTime); 
        velocity.x = newVelocity.x;
        velocity.z = newVelocity.z;
    }

    public void PushForward(Vector3 power)
    {
        Vector3 direction = controller.transform.rotation * power;
        velocity.x += direction.x;
        velocity.z += direction.z;
    }

    public void PushUp(float power)
    {
        velocity.y = power;
    }


    public void ManualUpdate()
    {
        velocity.y += g * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
    }
}
