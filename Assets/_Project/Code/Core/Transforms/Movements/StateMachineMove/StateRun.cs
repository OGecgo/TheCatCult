using UnityEngine;

public class StateRun: IStateMove
{

    private Vector3 _direction;
    private RunConf config;
    private ICharacterGravity characterGravity;
    
    public Vector3 direction { set{_direction = value;} }
  

    public StateRun(ICharacterGravity cg, RunConf runConf)
    {
        config = runConf;
        characterGravity = cg; 
        _direction = Vector3.zero;
    }

    // very bad movement. need be rewrited
    public void ManualUpdate()
    {
        // acceleration 
        float acceleration = config.runAcceleration;
        if (_direction == Vector3.zero)
            acceleration = config.stopAcceleration;

        // corecting speed of direction
        Vector3 newDirection = Vector3.Scale(_direction, new Vector3(config.speedDirection.x, 0, config.speedDirection.y)); 
        characterGravity.PushForwardGraundedDirection(newDirection * config.maxRunSpeed, acceleration);
    }
    
}
