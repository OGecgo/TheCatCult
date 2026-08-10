using UnityEngine;

public class MeleeWeapon: IMeleeWeapon
{
    private MeleeWeaponConf config;
    public MeleeWeapon(MeleeWeaponConf meleeWeaponConf)
    {
        config = meleeWeaponConf;
    }

    public void Attack(Collider colliderTarget)
    {
        // convert number to bit mask
        int targetLayerBit = 1 << colliderTarget.gameObject.layer;
        if  ((config.targetMask.value & targetLayerBit) != 0) 
        {
            IDamageable d = colliderTarget.GetComponent<IDamageable>();
            d.Attack(config.damage);
        }
    }
}
