using System.Threading;
using UnityEngine;

public class EnemyCatRotation : IEnemyCatRotation
{

    private Vector3 _posTarget;
    public Vector3 posTarget { get { return _posTarget; } set { _posTarget = value; }}
    
    private IRotationControll contrl;


    
    public void Initialize(float sensitivity, Transform transform)
    {
        contrl = new RotationControll();
        contrl.Initialize(sensitivity, new Vector3(1, 0, 1), transform);
        posTarget = Vector3.zero;
    }
    public void ChangeSensitivity(float newSensitivity)
    {
        contrl.sensitivity = newSensitivity;
    }

    public void Update()
    {
        if (posTarget != Vector3.zero){  
            contrl.UpdateRotateTo(posTarget);
        }
    }


}
