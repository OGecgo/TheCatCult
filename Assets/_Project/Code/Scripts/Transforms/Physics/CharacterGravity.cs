using UnityEngine;

public class CharacterGravity: MonoBehaviour, ICharacterGravity
{
    private float _g = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    public bool IsGrounded { get { return controller.isGrounded; } }
    public float g { get { return _g; }}


    public void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null) Debug.Log("controler is null");
        velocity = Vector3.zero;
    }

    public void PushForwardToDesireSpeed(Vector3 desiredSpeed, float acceleration)
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
        velocity.x += direction.x * Time.deltaTime;
        velocity.z += direction.z * Time.deltaTime;
    } 

    public void MoveForward(Vector3 constant)
    {
        Vector3 direction = controller.transform.rotation * constant;
        velocity.x = direction.x;
        velocity.z = direction.z;
    }

    public void PushUp(float power)
    {
        velocity.y = power;
    }

    public void Update()
    {
        // gravity
        velocity.y += g * Time.deltaTime;
        // moves
        controller.Move(velocity * Time.deltaTime);
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
    }
}
