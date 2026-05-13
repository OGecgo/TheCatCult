using UnityEngine;

public class RotationControll : IRotationControll
{
    private Transform _transform;
    private float _sensitivity;
    private Directions _directions;
    private bool _max_min_yaw_on;
    private Vector2 _max_min_yaw;
    private bool _max_min_pitch_on;
    private Vector2 _max_min_pitch;
    private bool _max_min_roll_on;
    private Vector2 _max_min_roll;
    [Range(0, 1)]
    private float _yawSpeed;
    [Range(0, 1)]
    private float _pitchSpeed;
    [Range(0, 1)]
    private float _rollSpeed;


    public float sensitivity { get { return _sensitivity; } set { _sensitivity = value; } }
    public Directions directions { get { return _directions; } set { _directions = value; } }
    public Transform transform { get { return _transform; } set { _transform = value; } }
    public bool max_min_yaw_on { get { return _max_min_yaw_on; } set { _max_min_yaw_on = value; } }
    public Vector2 max_min_yaw { get { return _max_min_yaw; } set { _max_min_yaw = value; } }
    public bool max_min_pitch_on { get { return _max_min_pitch_on; } set { _max_min_pitch_on = value; }}
    public Vector2 max_min_pitch { get { return _max_min_pitch; } set { _max_min_pitch = value; } }
    public bool max_min_roll_on { get {return _max_min_roll_on; }  set { _max_min_roll_on = value; } }    
    public Vector2 max_min_roll { get { return _max_min_roll; } set { _max_min_roll = value; } }
    public float yawSpeed { get { return _yawSpeed; } set { _yawSpeed = value; } }
    public float pitchSpeed { get { return _pitchSpeed; } set { _pitchSpeed = value; } }
    public float rollSpeed { get { return _rollSpeed; } set { _rollSpeed = value; } }
    


    private float pitch;
    private float yaw;
    private float roll;




    public void Initialize(float sensitivity, Directions direction, Vector3 speedDirections, Transform playerTransform)
    {
        this.max_min_yaw_on = false;
        this.max_min_pitch_on = false;
        this.max_min_roll_on = false;
        this.yawSpeed = speedDirections.x;
        this.pitchSpeed = speedDirections.y;
        this.rollSpeed = speedDirections.z;
        this.pitch = 0f;
        this.yaw = 0f;
        this.roll = 0f;


        this.sensitivity = sensitivity;
        this.directions = direction;
        this.transform = playerTransform;

    }
    public void RotateTo(Vector3 delta)// update That !!!!!!!!!!!!!!!!
    {
        switch (directions)
        {
            case Directions.Pitch:
                UpdatePitch(delta.y);
                yaw = 0;
                roll = 0;
                break;
            case Directions.Yaw:
                UpdateYaw(delta.x);
                pitch = 0;
                roll = 0;
                break;
            case Directions.Roll:
                UpdateRoll(delta.z);
                pitch = 0;
                yaw = 0;
                break;
            case Directions.Pitch_Yaw:
                UpdatePitch(delta.y);
                UpdateYaw(delta.x);
                roll = 0;
                break;
            case Directions.Pitch_Roll:
                UpdatePitch(delta.y);
                UpdateRoll(delta.z);
                yaw = 0;
                break;
            case Directions.Yaw_Roll:
                UpdateYaw(delta.x);
                UpdateRoll(delta.z);
                pitch = 0;
                break;
            case Directions.Pitch_Yaw_Roll:
                UpdatePitch(delta.y);
                UpdateYaw(delta.x);
                UpdateRoll(delta.z);
                break;
        }
    }
    public void Update()
    {
        transform.localEulerAngles = new Vector3(pitch, yaw, roll);
    }


    private void UpdatePitch(float value)
    {
        pitch -= value * pitchSpeed * sensitivity;
        if (!max_min_pitch_on) return;
        if (pitch > max_min_pitch.x) pitch = max_min_pitch.x;
        else if (pitch < max_min_pitch.y) pitch = max_min_pitch.y;
    }
    private void UpdateYaw(float value)
    {
        yaw += value * yawSpeed * sensitivity;
        if (!max_min_yaw_on) return;
        if (yaw > max_min_yaw.x) yaw = max_min_yaw.x;
        else if (yaw < max_min_yaw.y) yaw = max_min_yaw.y;
    }    
    private void UpdateRoll(float value)
    {
        roll += value * rollSpeed * sensitivity;
        if (!max_min_roll_on) return;
        if (roll > max_min_roll.x) roll = max_min_roll.x;
        else if (roll < max_min_roll.y) roll = max_min_roll.y;
    }
}