using UnityEngine;

[CreateAssetMenu(fileName = "MoveConf", menuName = "Scriptable Objects/MoveConf")]
public class MoveConf: ScriptableObject
{
    public float speedUpMoveWalk = 5f;
    public float speedUpMoveRun = 5f;
    public float howManyTimesWalkIsRun = 1.4f;
    public float declarationMove = 8f;


    public float speedMoveNotGrounded = 0.5f;
    public float maxSpeedFall = 1f;

}
