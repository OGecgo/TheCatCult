using UnityEngine;

public interface IEnemyCatRotation: IUpdatable
{
    // for properly work need to update posTarget
    public Vector3 posTarget { get; set; }

    public void ChangeSensitivity(float newSensitivity);

}
