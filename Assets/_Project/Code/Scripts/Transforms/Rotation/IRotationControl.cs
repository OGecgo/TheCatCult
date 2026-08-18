using UnityEngine;


public interface IRotationControl
{
    public void UpdateRotation(float angle, Vector3 direction);
    public void UpdateLocalRotation(Vector2 difference);
    public void UpdateRotateTo(Vector3 target);
}
