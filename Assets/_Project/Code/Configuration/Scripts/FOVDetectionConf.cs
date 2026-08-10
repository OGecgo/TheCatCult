using UnityEngine;

[CreateAssetMenu(fileName = "FOVDetectionConf", menuName = "Scriptable Objects/FOVDetectionConf")]
public class FOVDetectionConf : ScriptableObject
{
    public float radius = 10f;
    public float closeRadius = 3f;
    [Range(0, 360)]
    public float angle = 70f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
}
