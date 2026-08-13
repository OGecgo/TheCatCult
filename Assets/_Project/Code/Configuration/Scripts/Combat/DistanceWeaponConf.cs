using UnityEngine;

[CreateAssetMenu(fileName = "DistanceWeaponConf", menuName = "Scriptable Objects/DistanceWeaponConf")]
public class DistanceWeaponConf : ScriptableObject
{
    public int damage = 10;
    public float range = 100f;
    public int bullets = 100;
    public int bunchOfBullets = 3;
    public float timerCountAttack = 0.2f;
    public float timerReload = 3f;

    public LayerMask targetMask;
}
