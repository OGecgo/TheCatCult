using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour, IDamageable, IAttackedUI
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


    private bool isAttacked;
    private float timerCount;
    // Monobehaviour
    private Camera playerCamera;
    private ICharacterGravity characterGravity;
    // classes
    private IDistanceWeapon distanceWeapon;
    private ILife life;

    public event Action OnSetAttacked;
    public event Action OnUnsetAttacked;

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
                OnUnsetAttacked.Invoke();
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
        OnSetAttacked.Invoke();
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
