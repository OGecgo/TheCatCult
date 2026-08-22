using UnityEngine;

public class StateFall: IStateMove
{
    private FallConf config;   
    
    private Vector3 _direction;
    private ICharacterGravity characterGravity;

    public Vector3 direction { set{_direction = value;} } 

    public StateFall(ICharacterGravity cg, FallConf fallConf)
    {
        this.config = fallConf;
        this.characterGravity = cg;
        this._direction = Vector3.zero;
    }


    public void ManualUpdate()
    {
        if (_direction != Vector3.zero)
        {
            //  have permision for small moving on air
            characterGravity.PushForward(_direction * config.acceleration);   
        }
    }

}