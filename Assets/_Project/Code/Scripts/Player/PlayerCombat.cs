using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour, IDamageable
{
    [Header("Input")]
    public InputActionReference attackAction;
    [Header("General settigs")]
    public DistanceWeaponConf distanceWeaponConf;
    public LifeConf lifeConf;
    [Header("Player take damage settigs")]
    public float pushUp = 0.5f;
    public float pushBack = 1f;
    public float timerInvincible = 1f;


    private float timerCount;
    private bool isAttacked;
    private Camera playerCamera;
    private ICharacterGravity characterGravity;
    private IPlayerTransform playerTransform;
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
        characterGravity = GetComponent<ICharacterGravity>();
        playerTransform = GetComponent<IPlayerTransform>();
        distanceWeapon = new DistanceWeapon(distanceWeaponConf);

        timerCount = 0f;
        isAttacked = false;
    }

    void Update()
    {
        if (attackAction.action.triggered)
        {
            Shoot();
        }
        if (isAttacked)
        {
            if (timerCount <= 0)
            {
                isAttacked = false;
                UnsetInvincible();
            }
            if (characterGravity.IsGrounded)
            {
                playerTransform.stopTranforms = false;
            }
            timerCount -= Time.deltaTime;
        }     
    }

    public void Attack(int power)
    {
        SetInvincible();
        AttackPushPlayer();
        isAttacked = true;
        timerCount = timerInvincible;
        playerTransform.stopTranforms = true;
        life.Attack(power);
    }

    private void AttackPushPlayer()
    {

        characterGravity.PushUp(pushUp);
        characterGravity.MoveForward(new Vector3(0, 0, -pushBack));
    }

    private void SetInvincible()
    {
        // 0 is default
        this.gameObject.layer = 0;
    }
    private void UnsetInvincible()
    {
        this.gameObject.layer = LayerMask.NameToLayer("PlayerLayer");
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
