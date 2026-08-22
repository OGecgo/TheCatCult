using NUnit.Framework;
using UnityEngine;


// charachter gravity work only if someone calls the update
// (that need for ease controll update)
public class CharacterGravity: MonoBehaviour, ICharacterGravity, IUpdatable
{
    private float _g = -9.81f;

    private CharacterController controller;
    private Vector3 _velocity;
    private Vector3 normalGroundedObj;

    public bool isGrounded { get { return controller.isGrounded; } }
    public Vector3 groundNormal { get{ return normalGroundedObj; }}
    public Vector3 velocity { get{ return _velocity; }}
    public float g { get { return _g; }}
    public void PushForwardGraundedDirection(Vector3 desiredSpeed, float acceleration)
    {
        // rotation * direction + (dont lost _velocity.y)
        Vector3 direction = controller.transform.rotation * desiredSpeed;
        
        if (controller.isGrounded)
        {
            // for acess diagonal movemnet
            direction = Vector3.ProjectOnPlane(direction, normalGroundedObj);
            _velocity = Vector3.MoveTowards(_velocity, direction, acceleration * Time.deltaTime);
        }
        else
        {
            // Air Do not touch _velocity.y
            Vector3 targetAirVelocity = new Vector3(direction.x, _velocity.y, direction.z);
            _velocity = Vector3.MoveTowards(_velocity, targetAirVelocity, acceleration * Time.deltaTime);
        }
    }

    public void PushForward(Vector3 desiredSpeed, float acceleration)
    {
        // rotation * direction + (dont lost _velocity.y)
        Vector3 direction = controller.transform.rotation * desiredSpeed;

        // Air Do not touch _velocity.y
        Vector3 targetAirVelocity = new Vector3(direction.x, _velocity.y, direction.z);
        _velocity = Vector3.MoveTowards(_velocity, targetAirVelocity, acceleration * Time.deltaTime);
    }

    public void PushForward(Vector3 power)
    {
        Vector3 direction = controller.transform.rotation * power;

        // Air Do not touch _velocity.y
        _velocity.x += direction.x * Time.deltaTime;
        _velocity.z += direction.z * Time.deltaTime;
    } 

    public void MoveForward(Vector3 constant)
    {
        Vector3 direction = controller.transform.rotation * constant;
        _velocity.x = direction.x;
        _velocity.z = direction.z;
    }

    public void PushUp(float power)
    {
        _velocity.y = power;
    }

    public void ManualUpdate()
    {
        // reset
        if (!controller.isGrounded)
        {
            normalGroundedObj = Vector3.up;
        }

        // gravity
        _velocity.y += g * Time.deltaTime;
        
        controller.Move(_velocity * Time.deltaTime);

        if (controller.isGrounded && _velocity.y < 0)
        {
            // make sure is grounded when on object (for onHitCollider)
            _velocity.y = -2f;
        }

    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        _velocity = Vector3.zero;
        normalGroundedObj = Vector3.up;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (controller.isGrounded && hit.normal.y > 0.1f)
        {
            normalGroundedObj = hit.normal;
        }
    }
}
