using System.Threading;
using UnityEngine;

public class EnemyCatRotation : IEnemyCatRotation
{

    private Vector3 _posTarget;
    private IRotationControl controle;

    public Vector3 posTarget { get { return _posTarget; } set { _posTarget = value; }}
    public EnemyCatRotation(RotationConf rotationConf, Transform transform)
    {
        controle = new RotationControl(rotationConf, transform);
        posTarget = Vector3.zero;
    }
 
    public void ManualUpdate()
    {
        if (posTarget != Vector3.zero){  
            controle.UpdateRotateTo(posTarget);
        }
    }


}
