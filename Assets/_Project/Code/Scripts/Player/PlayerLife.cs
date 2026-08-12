using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IDamageable, IAttackedDamageUI, IAttackedHelthUI
{
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;
    [Header("Player take damage settigs")]
    [SerializeField] private float pushUp = 0.5f;
    [SerializeField] private float pushBack = 1f;
    [SerializeField] private float timerInvincible = 1f;


    private bool isAttacked;
    private float timerCount;
    private ICharacterGravity characterGravity;
    private ILife life;
    public event Action OnSetAttacked;
    public event Action OnUnsetAttacked;
    public event Action<int> OnAttacked;

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
        characterGravity = GetComponent<ICharacterGravity>();

        timerCount = 0f;
        isAttacked = false;
    }

    // do not use Update. find something better that this
    void Update()
    {
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
        OnAttacked.Invoke(power);
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
}
