using UnityEngine;

public class StateWalk : IStateMove
{
    private Vector3 _direction;

    private ICharacterGravity characterGravity;
    private WalkConf config;


    public Vector3 direction { set{_direction = value;} }

    // velocity is pointer to one obj. that needed for syncronize different state with movement
    public StateWalk(ICharacterGravity cg, WalkConf walkConf)
    {
        this.config = walkConf;
        this.characterGravity = cg;
        this._direction = Vector3.zero;
    }
    public void ManualUpdate()
    {
        // acceleration
        float acceleration = config.walkAcceleration;
        if (_direction == Vector3.zero)
            acceleration = config.stopAcceleration;            

        // corecting speed of direction
        Vector3 newDirection = Vector3.Scale(_direction, new Vector3(config.speedDirection.x, 0, config.speedDirection.y)); 
        characterGravity.PushForwardGraundedDirection(newDirection * config.maxWalkSpeed, acceleration);
    }

}