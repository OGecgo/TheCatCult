using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponConf", menuName = "Scriptable Objects/MeleeWeaponConf")]
public class MeleeWeaponConf : ScriptableObject
{
    public int damage = 10;
    public LayerMask targetMask;
}
