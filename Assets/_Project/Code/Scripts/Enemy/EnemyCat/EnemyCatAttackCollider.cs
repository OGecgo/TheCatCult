using UnityEngine;

public class EnemyCatAttackCollider : MonoBehaviour, IAwakable
{
    [Header("General settins")]
    public MeleeWeaponConf meleeWeaponConf;

    private MeleeWeapon meleeWeapon;

    public void ManualAwake()
    {
        meleeWeapon = new MeleeWeapon(meleeWeaponConf);
    }


    private void OnTriggerEnter(Collider other)
    {
        meleeWeapon.Attack(other);     
    }
}
