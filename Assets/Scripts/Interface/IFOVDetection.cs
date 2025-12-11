using UnityEngine;

public interface IFOVDetection
{
    void StartDetection();
    public float radius {get; set;}
    public bool isTarget {get; set;}
    public GameObject target {get; set;} // if detection true or not

    public void Initialize(GameObject target, LayerMask targetMask, LayerMask obstructionMask, Transform transform, float radius, float angle);
    public void FOVCheck(); // do the check. (dont use if run Start())
}
