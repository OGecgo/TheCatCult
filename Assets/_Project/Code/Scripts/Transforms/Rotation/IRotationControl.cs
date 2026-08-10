using UnityEngine;


public interface IRotationControl
{
    public void UpdateLocalRotation(Vector2 difference);
    public void UpdateRotateTo(Vector3 target);
}
