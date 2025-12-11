using UnityEngine;

public interface IEnemyCatRotation
{
    // for properly work need to update posTarget
    public Vector3 posTarget { get; set; }

    public void Initialize( float sensitivity, Transform transforms);
    public void ChangeSensitivity(float newSensitivity);
    public void Update();

}
