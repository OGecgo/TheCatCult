using UnityEngine;

public class EnemyCatRotation: IEnemyCatRotation
{
    private IRotationControll contrl;
    private GameObject target;
    private bool _rotationOn;
    public bool rotationOn {get {return _rotationOn;} set {_rotationOn = value;}}
    public void Initialize( float sensitivity, Transform transform, GameObject target)
    {
        contrl = new RotationControll();
        contrl.Initialize(sensitivity, Directions.Yaw, transform);
        contrl.pitchSpeed = 1f;// that be not harded writed
    }
    public void ChangeSensitivity(float newSensitivity)
    {
        contrl.sensitivity = newSensitivity;
    }
    public void Update()
    {
        if (rotationOn)
        {
            contrl.RotateTo(Vector3.left);
            contrl.Update();
        }
    }


}
