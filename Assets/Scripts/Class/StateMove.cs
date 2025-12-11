using UnityEngine;
using UnityEngine.InputSystem;

public class StateMove : IStateMove
{
    private InputActionReference moveAction;
    private ICharacterGravity characterGravity;

    private float _speedMove;
    public float speedMove { get { return _speedMove; } set { _speedMove = value; } }
    private Vector3 _moveTo;
    public Vector3 moveTo { get { return _moveTo; } set { _moveTo = value; } }
    private Vector3 LastMoveTo;


    private SpeedUpMoveConf config;
    private float speedUp { get { return config.speedUp; } set { config.speedUp = value; } }
    private float deceleration { get { return config.deceleration; } set { config.deceleration = value; } }
    private float velocity;

    public void Initialize(ICharacterGravity cg, float speedMove)
    {
        this.characterGravity = cg;
        this.speedMove = speedMove; 
        moveTo = Vector3.zero;
        velocity = 0f;
        config = Resources.Load<SpeedUpMoveConf>("Configuration/SpeedUpMoveConf");
    }

    public void Enter()
    {
        
    }

    // very bad movement. need be rewrited
    public void Update()
    {
        // not move or stop move
        if (moveTo == Vector3.zero)
        {
            if (velocity > 0) velocity -= speedMove * Time.deltaTime * deceleration;
            else velocity = 0f;

        }
        // move
        else
        {
            if (velocity < speedMove) velocity += speedMove * Time.deltaTime * speedUp;
            else velocity = speedMove;
            LastMoveTo = moveTo; // update last move direction with new direction
        }

        Vector3 movement = Vector3.ClampMagnitude(LastMoveTo, 1f) * velocity;
        characterGravity.PushX(movement.x);
        characterGravity.PushZ(movement.z);
    }

    public void Exit()
    {
        
    }


}