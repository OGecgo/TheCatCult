using UnityEngine;

public class StateMoveSyncronizeData
{
    // used for smooth movement
    private float[] _velocity;
    // used for smoothStop obj from move
    private Vector3 _lastMoveTo;

    public float velocity { get{ return _velocity[0]; } set{_velocity[0] = value; } }
    public Vector3 lastMoveTo { get{ return _lastMoveTo; } set{ _lastMoveTo = value; } }

    public StateMoveSyncronizeData()
    {
        _velocity = new float[1];
        _lastMoveTo = Vector3.zero;
    }
}


