using UnityEngine;

public class MeleeWeapon: IMeleeWeapon
{
    private MeleeWeaponConf config;

    private float timerCount;

    public MeleeWeapon(MeleeWeaponConf meleeWeaponConf)
    {
        config = meleeWeaponConf;
        ResetTimer();
    }

    public void ResetTimer()
    {
        timerCount = 0f;
    }

    public bool TestCollider(Collider colliderTarget)
    {
        // convert number to bit mask
        int targetLayerBit = 1 << colliderTarget.gameObject.layer;
        return (config.targetMask.value & targetLayerBit) != 0; 
    }

    public void Attack(Collider colliderTarget)
    {
        if (timerCount <= 0f)
        {
            IDamageable d = colliderTarget.GetComponent<IDamageable>();
            d.Attack(config.damage);

            timerCount = config.timerCountAttack;
            return;       
        }
        timerCount -= Time.deltaTime;
    }
}
