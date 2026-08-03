using UnityEngine;

[CreateAssetMenu(fileName = "RunConf", menuName = "Scriptable Objects/RunConf")]
public class RunConf : ScriptableObject
{
    public float maxRunSpeed = 14f;
    public Vector2 speedDirection = new Vector2(1f, 1f);
    public float runAcceleration = 5f;
    public float stopAcceleration = 8f;
}
