using UnityEngine;

public class Weapon: IWeapon
{
    WeaponConf config;
    int i = 0;

    public void Attack(Vector3 position, Vector3 direction)
    {
        if (Physics.Raycast(position, direction, out RaycastHit hit, config.range, config.targetMask))
        {
            // do damage if is hitable mask
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log(hit.collider.name + " " + i);
                i +=1;
                damageable.Attack(config.damage);
            }
        }
    }
    public Weapon(WeaponConf weaponConf)
    {
        config = weaponConf;
    } 


}
