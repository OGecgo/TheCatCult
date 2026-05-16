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
        // here add very slow movement
    }
    public void Exit()
    {

    }

}
