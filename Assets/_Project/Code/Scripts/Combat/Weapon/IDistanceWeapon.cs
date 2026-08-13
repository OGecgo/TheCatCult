using UnityEngine;

public interface IDistanceWeapon
{

    // after ReloadBullets 
    // for time isReloading == true 
    // needed do UpdateReloading untile isReloading == false
    public bool isRealoading {get;}
    public int currentBullets {get;}
    public void ResetTimer();
    public void StartReloading();
    public void UpdateIsReloading();
    public void AddBunchOfBullets();
    public void Attack(Vector3 position, Vector3 direction);
}
