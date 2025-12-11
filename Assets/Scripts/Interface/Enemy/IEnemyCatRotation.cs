using UnityEngine;

public interface IEnemyCatRotation
{
    public bool rotationOn {get; set;}
    public void Initialize( float sensitivity, Transform transform, GameObject target);
    public void ChangeSensitivity(float newSensitivity);
    public void Update();

}
