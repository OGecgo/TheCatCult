using UnityEngine;

public class MeleeWeapon: IMeleeWeapon
{
    private MeleeWeaponConf config;
    public MeleeWeapon(MeleeWeaponConf meleeWeaponConf)
    {
        config = meleeWeaponConf;
    }

    public bool TestCollider(Collider colliderTarget)
    {
        // convert number to bit mask
        int targetLayerBit = 1 << colliderTarget.gameObject.layer;
        return (config.targetMask.value & targetLayerBit) != 0;
    }

    public void Attack(Collider colliderTarget)
    {
        IDamageable d = colliderTarget.GetComponent<IDamageable>();
        d.Attack(config.damage);
    }
}
