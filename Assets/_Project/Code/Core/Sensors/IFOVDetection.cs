using UnityEngine;

public interface IFOVDetection
{
    void StartDetection();
    public bool isTarget { get; set; }
    public Vector3 posTarget { get; set; }
    public FOVDetectionConf fovConf {get;}
}
