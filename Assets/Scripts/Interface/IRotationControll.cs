using UnityEngine;


public interface IRotationControll
{
    public float sensitivity { get; set; }
    public Transform transform { get; set; }
    // all values by defaul 0f and false
    public bool[] onMinMaxValues { get; set; }
    public Vector3 maxValues { get; set; }
    public Vector3 minValues { get; set; }
    // values between 0 and 1
    public Vector3 onDirections { get; set; } 

    public void UpdateLocalRotation(Vector2 difference);
    public void UpdateRotateTo(Vector3 target);
}
