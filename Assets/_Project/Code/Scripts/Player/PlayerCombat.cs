using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour, IDamageable
{
    [Header("Input")]
    public InputActionReference attackAction;
    [Header("General settigs")]
    public DistanceWeaponConf distanceWeaponConf;
    public LifeConf lifeConf;


    private Camera playerCamera;
    private IDistanceWeapon distanceWeapon;
    private ILife life;

    public void OnEnable()
    {
        life.OnDie += PlayerDeath;
    }
    public void OnDisable()
    {
        life.OnDie -= PlayerDeath;   
    }

    public void Awake()
    {
        life = new Life(lifeConf);
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

    public void Attack(int power)
    {
        life.Attack(power);
    }


    // for now nothing (connect with UI)
    private void PlayerDeath()
    {
        Debug.Log("Player not dead");
    }

    private void Shoot()
    {
        distanceWeapon.Attack(playerCamera.transform.position, playerCamera.transform.forward);
    }


}
