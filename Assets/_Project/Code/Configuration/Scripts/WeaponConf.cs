using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConf", menuName = "Scriptable Objects/WeaponConf")]
public class WeaponConf : ScriptableObject
{
    public int damage = 10;
    public float range = 100f;

    public LayerMask targetMask;
}
