using UnityEngine;

[CreateAssetMenu(fileName = "WalkConf", menuName = "Scriptable Objects/WalkConf")]
public class WalkConf : ScriptableObject
{
    public float maxWalkSpeed = 10f;
    public Vector2 speedDirection = new Vector2(1f, 1f);
    public float walkAcceleration = 5f;
    public float stopAcceleration = 5f; 
}
