using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference attackAction;
    [Header("General settigs")]
    public DistanceWeaponConf distanceWeaponConf;

    private Camera playerCamera;
    private IDistanceWeapon distanceWeapon;

    public void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        distanceWeapon = new DistanceWeapon(distanceWeaponConf);
    }

    void Update()
    {
        if (attackAction.action.triggered)
        {
            Shoot();
        }         
    }

    private void Shoot()
    {
        
        distanceWeapon.Attack(playerCamera.transform.position, playerCamera.transform.forward);
    }
}
