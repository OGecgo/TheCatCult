using UnityEngine;

[CreateAssetMenu(fileName = "DistanceWeaponConf", menuName = "Scriptable Objects/DistanceWeaponConf")]
public class DistanceWeaponConf : ScriptableObject
{
    public int damage = 10;
    public float range = 100f;

    public LayerMask targetMask;
}
