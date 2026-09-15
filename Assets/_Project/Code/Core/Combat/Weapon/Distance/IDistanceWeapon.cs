using UnityEngine;

public interface IDistanceWeapon
{

    // after ReloadBullets 
    // for time isReloading == true 
    // needed do UpdateReloading untile isReloading == false
    public bool isReloading {get;}
    public bool isAttacking {get;}
    public int currentBullets {get;}
    public int currentBunchOfBullets {get;}
    public void ResetTimer();
    public void StartReloading();
    public void UpdateIsReloading();
    public void AddBunchOfBullets();
    public void Attack(Vector3 position, Vector3 direction);
    public void UpdateIsAttacking();
}
