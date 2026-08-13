using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IUpdatable, IPauseFeatures, IDamageable, IAttackedDamageUI, IAttackedHelthUI
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
    private bool featureIsPaused;


    public event Action OnSetAttacked;
    public event Action OnUnsetAttacked;
    public event Action<int> OnAttacked;

    public void ManualUpdate()
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

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }

    public void Attack(int power)
    {
        if (featureIsPaused) return;

        SetInvincible();
        AttackPushPlayer();
        isAttacked = true;
        timerCount = timerInvincible;
        OnSetAttacked.Invoke();
        life.Attack(power);
        OnAttacked.Invoke(power);
    }

    private void OnEnable()
    {
        life.OnDie += PlayerDeath;
    }
    private void OnDisable()
    {
        life.OnDie -= PlayerDeath;   
    }

    private void Awake()
    {
        life = new Life(lifeConf);
        characterGravity = GetComponent<ICharacterGravity>();

        featureIsPaused = false;
        timerCount = 0f;
        isAttacked = false;
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
