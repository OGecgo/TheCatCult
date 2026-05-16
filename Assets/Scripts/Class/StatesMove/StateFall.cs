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
        // if (_moveTo != Vector3.zero)
        // {
        //     data.velocity  += Time.deltaTime * config.speedMoveNotGrounded;
        //     data.lastMoveTo = _moveTo; // update last move direction with new direction 
        
        //     Vector3 movement = Vector3.ClampMagnitude(data.lastMoveTo, 1f) * data.velocity;
        //     characterGravity.PushX(movement.x);
        //     characterGravity.PushZ(movement.z);
        // }
    }
    public void Exit()
    {

    }

}