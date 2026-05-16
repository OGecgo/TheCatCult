using UnityEngine;

public class StateMoveSyncronizeData
{
    // used for smooth movement
    private Vector2 _velocity;
    // used for smoothStop obj from move
    private Vector3 _lastMoveTo;

    public float velocity_x { get{ return _velocity.x; } set{_velocity.x = value; } }
    public float velocity_z { get{ return _velocity.y; } set{_velocity.y = value; } }

    public Vector3 lastMoveTo { get{ return _lastMoveTo; } set{ _lastMoveTo = value; } }

    public StateMoveSyncronizeData()
    {
        _velocity = Vector2.zero;
        _lastMoveTo = Vector3.zero;
    }
}


