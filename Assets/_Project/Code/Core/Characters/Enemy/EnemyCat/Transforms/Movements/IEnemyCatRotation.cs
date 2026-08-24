using UnityEngine;

public interface IEnemyCatRotation: IUpdatable
{
    public Vector3 posTarget {get; set;}
    public float angle {get; set;}
}
