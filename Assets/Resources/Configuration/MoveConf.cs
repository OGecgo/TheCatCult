using UnityEngine;

[CreateAssetMenu(fileName = "MoveConf", menuName = "Scriptable Objects/MoveConf")]
public class MoveConf: ScriptableObject
{
    public float walkAcceleration = 5f;
    public float runAcceleration = 5f;
    public float runSpeedMultiplied = 1.4f;
    public float stopAcceleration = 8f;

    public float airMoveSpeed = 0.5f; 

}
