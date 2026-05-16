using UnityEngine;

public class StateFall: IStateMove
{
    private Vector3 _moveTo;
    public Vector3 moveTo { set {_moveTo = value;} }

    private ICharacterGravity characterGravity;
    private MoveConf config;   
    private StateMoveSyncronizeData data;



    public StateFall(ICharacterGravity cg, StateMoveSyncronizeData data)
    {
        this.characterGravity = cg;
        this.config = Resources.Load<MoveConf>("Configuration/MoveConf");
        this.data = data;
    }

    public void Enter()
    {

    }
    public void Update()
    {
        // // move when on air
        if (_moveTo != Vector3.zero)
        {
            // move to
            float targetVelocityX = _moveTo.x * config.speedMoveNotGrounded;
            float targetVelocityZ = _moveTo.z * config.speedMoveNotGrounded;

            // smooth change
            float speedChange = Time.deltaTime * config.speedMoveNotGrounded;
            data.velocity_x = Mathf.MoveTowards(data.velocity_x, targetVelocityX, speedChange);
            data.velocity_z = Mathf.MoveTowards(data.velocity_z, targetVelocityZ, speedChange);

            // save last movement
            if (_moveTo != Vector3.zero)
                data.lastMoveTo = _moveTo;

            // do move
            characterGravity.PushX(data.velocity_x);
            characterGravity.PushZ(data.velocity_z);
        }
    }
    public void Exit()
    {

    }

}