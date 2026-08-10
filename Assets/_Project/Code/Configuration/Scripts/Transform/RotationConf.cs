using UnityEngine;

[CreateAssetMenu(fileName = "RotationConf", menuName = "Scriptable Objects/RotationConf")]
public class RotationConf : ScriptableObject
{
    public float sensitivity;
    [Header("X directoin conf")]
    [Range(0, 1)] public float xDirection;
    public bool xDomain;
    public float xMin;
    public float xMax;
    [Header("Y direction conf")]
    [Range(0, 1)] public float yDirection;
    public bool yDomain;
    public float yMin;
    public float yMax;
    [Header("Z direction conf")]
    [Range(0, 1)] public float zDirection;
    public bool zDomain;
    public float zMin;
    public float zMax;
}
