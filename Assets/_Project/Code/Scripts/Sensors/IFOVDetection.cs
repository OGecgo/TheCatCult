using UnityEngine;

public interface IFOVDetection
{
    void StartDetection();
    public bool isTarget { get; set; }
    public Vector3 posTarget { get; set; }
    // public void Initialize(LayerMask targetMask, LayerMask obstructionMask, Transform transform, float radius, float angle);
    public void FOVCheck(); // do the check. (dont use if run Start())
}
