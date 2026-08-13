using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour, IUpdatable
{
    [Header("Input")]
    [SerializeField] private InputActionReference attackAction;
    [Header("General settigs")]
    [SerializeField] private DistanceWeaponConf distanceWeaponConf;

    private Camera playerCamera;
    private IDistanceWeapon distanceWeapon;

    public void ManualUpdate()
    {
        if (attackAction.action.triggered) Shoot();
    }

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        distanceWeapon = new DistanceWeapon(distanceWeaponConf);
    }

    private void Shoot()
    {
        distanceWeapon.Attack(playerCamera.transform.position, playerCamera.transform.forward);
    }

}
