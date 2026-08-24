using UnityEngine;

public class DistanceWeapon: IDistanceWeapon
{
    [SerializeField] private DistanceWeaponConf config;

    private float timerCountAttack;
    private float timerCountRelaod;
    private int bulletsUsed;
    private int bunchOfBullets;

    public bool isReloading {get{return timerCountRelaod > 0;}}
    public int currentBullets {get{return config.bullets - bulletsUsed;}}
    public int currentBunchOfBullets {get{return bunchOfBullets;}}

    public DistanceWeapon(DistanceWeaponConf distanceWeaponConf)
    {
        config = distanceWeaponConf;
        timerCountAttack = 0f;
        timerCountRelaod = 0f;
        bulletsUsed = 0;
        bunchOfBullets = config.bunchOfBullets;;
    } 

    public void ResetTimer()
    {
        timerCountAttack = 0f;
    }

    public void StartReloading()
    {
        if (isReloading) return;

        if (bunchOfBullets > 0)
        {
            bunchOfBullets -= 1;
            bulletsUsed = 0;
            timerCountRelaod = config.timerReload;
        }        
    }

    public void UpdateIsReloading()
    {
        timerCountRelaod -= Time.deltaTime;
    }

    public void AddBunchOfBullets()
    {
        bunchOfBullets += 1;
    }

    public void Attack(Vector3 position, Vector3 direction)
    {
        if (isReloading) return;

        if (timerCountAttack <= 0f && bulletsUsed < config.bullets)
        {
            if (Physics.Raycast(position, direction, out RaycastHit hit, config.range, config.targetMask))
            {
                // do damage if is hitable mask
                if (hit.collider.TryGetComponent(out IIsAttacked damageable))
                {
                    damageable.OnAttack(config.damage);
                }
            }
            bulletsUsed += 1;
            timerCountAttack = config.timerCountAttack;
            return;
        }
        timerCountAttack -= Time.deltaTime;
    }
}
