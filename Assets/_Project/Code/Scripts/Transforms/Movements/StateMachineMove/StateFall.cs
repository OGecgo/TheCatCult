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
        //  move when on air
        if (_direction != Vector3.zero)
        {
            // max speed * direction * direction speed
            // Vector3 tempMoveTo = new Vector3(moveTo, 0, moveTo.z);
            // Vector3 targetVelocity = Vector3.Scale(, new Vector3(config.speedDirection.x, 0, config.speedDirection.y));
            // // current speed
            // Vector3 currentVelocity = new Vector3(data.velocity_x, 0, data.velocity_z);

            // // stop
            // float speedChange = Time.deltaTime * config.airStop;
            // Vector3 newVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, speedChange);

            
            // if (_moveTo != Vector3.zero)
            // {
            //     // if exist try movement on fall state
            //     // speedChange = Time.deltaTime * config.moveSpeed;
            //     // // set new max speed * direction * direction speed
            //     // targetVelocity = Vector3.Scale(_moveTo, new Vector3(config.speedDirection.x, 0, config.speedDirection.y)) *  config.moveSpeed ;
            //     // newVelocity = Vector3.MoveTowards(newVelocity, targetVelocity, speedChange);
            // }


            // // change speed
            // data.velocity_x = newVelocity.x;
            // data.velocity_z = newVelocity.z;
                

            // // do move
            // characterGravity.PushX(data.velocity_x);
            // characterGravity.PushZ(data.velocity_z); 
        }

    }

}