using System;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerAttack : MonoBehaviour, IUpdatable, IInteractBullet, IBulletsAction
{
    [Header("Input")]
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference reloadAction;
    [Header("General settigs")]
    [SerializeField] private DistanceWeaponConf distanceWeaponConf;

    private Camera playerCamera;
    private IDistanceWeapon distanceWeapon;

    public event Action<int, int> OnSetBullets;
    public event Action<int> OnSetBunchOfBullets;
    // will be used for animation and sounds
    public event Action OnReloadBullets; 

    public void ManualUpdate()
    {
        // shooting
        if (attackAction.action.IsPressed())
        {
            // shoot
            distanceWeapon.Attack(playerCamera.transform.position, playerCamera.transform.forward);
            // show on screen bullets
            OnSetBullets?.Invoke(distanceWeapon.currentBullets, distanceWeaponConf.bullets);
        } 
        else
        { 
            distanceWeapon.ResetTimer();
        }

        // start reloading
        if (reloadAction.action.triggered)
        {
            distanceWeapon.StartReloading();
            distanceWeapon.ResetTimer();
            OnSetBunchOfBullets?.Invoke(distanceWeapon.currentBunchOfBullets);
            OnSetBullets?.Invoke(distanceWeapon.currentBullets, distanceWeaponConf.bullets);
            OnReloadBullets?.Invoke();
        }

        // update reloading
        if (distanceWeapon.isReloading)
        {
            distanceWeapon.UpdateIsReloading();
        }
    }

    public void GetBunchOfBullets()
    {
        distanceWeapon.AddBunchOfBullets();
        OnSetBunchOfBullets.Invoke(distanceWeapon.currentBunchOfBullets);
    }

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        distanceWeapon = new DistanceWeapon(distanceWeaponConf);
    }

    private void Start()
    {
        OnSetBullets?.Invoke(distanceWeapon.currentBullets, distanceWeaponConf.bullets);
        OnSetBunchOfBullets?.Invoke(distanceWeapon.currentBunchOfBullets);
    }

}
