using UnityEngine;

public class DistanceWeapon: IDistanceWeapon
{
    DistanceWeaponConf config;

    public void Attack(Vector3 position, Vector3 direction)
    {
        if (Physics.Raycast(position, direction, out RaycastHit hit, config.range, config.targetMask))
        {
            // do damage if is hitable mask
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Attack(config.damage);
            }
        }
    }
    public DistanceWeapon(DistanceWeaponConf distanceWeaponConf)
    {
        config = distanceWeaponConf;
    } 


}
