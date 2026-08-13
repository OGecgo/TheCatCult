using System.Collections;
using UnityEngine;

public class DistanceWeapon: IDistanceWeapon
{
    [SerializeField] private DistanceWeaponConf config;

    private float timerCountAttack;
    private float timerCountRelaod;
    private int bulletsUsed;
    private int bunchOfBulletsUsed;

    public bool isRealoading {get{return timerCountRelaod > 0;}}
    public int currentBullets {get{return config.bullets - bulletsUsed;}}

    public DistanceWeapon(DistanceWeaponConf distanceWeaponConf)
    {
        config = distanceWeaponConf;
        timerCountAttack = 0f;
        timerCountRelaod = 0f;
        bulletsUsed = 0;
        bunchOfBulletsUsed = 0;
    } 

    public void ResetTimer()
    {
        timerCountAttack = 0f;
    }

    public void StartReloading()
    {
        if (isRealoading) return;

        if (bunchOfBulletsUsed < config.bunchOfBullets)
        {
            bunchOfBulletsUsed += 1;
            bulletsUsed = 0;
            timerCountRelaod = config.timerReload;
        }
    }

    public void UpdateIsReloading()
    {
        if (isRealoading)
        {
            timerCountRelaod -= Time.deltaTime;
        }
    }

    public void AddBunchOfBullets()
    {
        bunchOfBulletsUsed -= 1;
    }

    public void Attack(Vector3 position, Vector3 direction)
    {
        if (isRealoading) return;

        if (timerCountAttack <= 0f && bulletsUsed < config.bullets)
        {
            if (Physics.Raycast(position, direction, out RaycastHit hit, config.range, config.targetMask))
            {
                // do damage if is hitable mask
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Attack(config.damage);
                }
            }
            bulletsUsed += 1;
            timerCountAttack = config.timerCountAttack;
            return;
        }
        timerCountAttack -= Time.deltaTime;
    }
}
