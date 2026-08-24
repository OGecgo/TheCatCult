using UnityEngine;

public class EnemyCatRotation : IEnemyCatRotation
{
    private enum UpdateState {STATE_POSTARGET, STATE_ANGLE}

    private Vector3 _posTarget;
    private float _angle;
    private UpdateState state;

    private IRotationControl controle;
    public Vector3 posTarget {get{return _posTarget;} set
        {
            _posTarget = value;
            state = UpdateState.STATE_POSTARGET;
        }
    }
    public float angle {get{return _angle;} set
        {
            _angle = value;
            state = UpdateState.STATE_ANGLE;
        }
    }


    public EnemyCatRotation(RotationConf rotationConf, Transform transform)
    {
        controle = new RotationControl(rotationConf, transform);
        angle = 0f;
        posTarget = Vector3.zero;        
    }
 

    public void ManualUpdate()
    {
        switch (state)
        {
            case UpdateState.STATE_ANGLE:
                controle.UpdateRotation(angle, Vector3.up);
                break;
            case UpdateState.STATE_POSTARGET:
                controle.UpdateRotateTo(posTarget);
                break;
        }
    }
}
