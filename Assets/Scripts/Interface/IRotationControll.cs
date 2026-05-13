using UnityEngine;


// for using IRotationControll you need first every frame do RotateTo and then call Update
// else rotation will not be applied


public interface IRotationControll
{
    public float sensitivity { get; set; }
    public Directions directions { get; set; }
    public Transform transform { get; set; }
    // all values by defaul 0f and false
    // first max second min max_min
    public bool max_min_yaw_on { get; set; }
    public Vector2 max_min_yaw { get; set; }
    public bool max_min_pitch_on { get; set; }
    public Vector2 max_min_pitch { get; set; }
    public bool max_min_roll_on { get; set; }
    public Vector2 max_min_roll { get; set; }
    // give values between 0 and 1
    public float yawSpeed { get; set; }
    public float pitchSpeed { get; set; }
    public float rollSpeed { get; set; }
    public void Initialize(float sensitivity, Directions direction, Vector3 speedDirections, Transform playerTransform);
    public void RotateTo(Vector3 delta);
    public void Update();
}
