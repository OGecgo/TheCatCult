using System.Threading;
using UnityEngine;

public class EnemyCatRotation : IEnemyCatRotation
{

    private Vector3 _posTarget;
    private IRotationControl controle;

    public Vector3 posTarget { get { return _posTarget; } set { _posTarget = value; }}
    public EnemyCatRotation(float sensitivity, Transform transform)
    {
        controle = new RotationControl(sensitivity, new Vector3(1, 0, 1), transform);
        posTarget = Vector3.zero;
    }
    public void ChangeSensitivity(float newSensitivity)
    {
        controle.sensitivity = newSensitivity;
    }

    public void ManualUpdate()
    {
        if (posTarget != Vector3.zero){  
            controle.UpdateRotateTo(posTarget);
        }
    }


}
