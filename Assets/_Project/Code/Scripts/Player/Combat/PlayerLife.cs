using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLife : MonoBehaviour, 
    IUpdatable, IPauseFeatures, 
    IDamageable, 
    IInteractHealth,
    IDamageUI, IHealingPackUI, IHealthUI, IDeathUI

{   
    [Header("Input")]
    [SerializeField] private InputActionReference healAction;
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;
    [SerializeField] private HealingPackConf healingPackConf;
    [Header("Player take damage settigs")]
    [SerializeField] private float pushUp = 0.5f;
    [SerializeField] private float pushBack = 1f;
    [SerializeField] private float timerInvincible = 1f;


    private bool isAttacked;
    private float timerCount;
    private ICharacterGravity characterGravity;
    private ILife life;
    private IHealingPack healingPack;
    private bool featureIsPaused;

    // used from UI
    public event Action OnSetAttacked;
    public event Action OnUnsetAttacked;
    public event Action<int> OnSetLife;
    public event Action OnDeath;
    public event Action<int> OnSetHealingPacks;

    public void ManualUpdate()
    {
        GetAttack();
        Heal();
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
        if (OnSetAttacked != null) OnSetAttacked.Invoke();
        life.Attack(power);
        if (OnSetLife != null) OnSetLife.Invoke(life.health);
    }

    public void GetHealingPack()
    {
        healingPack.AddHealingPack();
    if (OnSetHealingPacks != null) OnSetHealingPacks.Invoke(healingPack.healingPacks);
    }


    private void OnEnable()
    {
        life.OnDie += Die;
    }
    private void OnDisable()
    {
        life.OnDie -= Die;   
    }

    private void Awake()
    {
        life = new Life(lifeConf);
        healingPack = new HealingPack(healingPackConf);
        characterGravity = GetComponent<ICharacterGravity>();

        featureIsPaused = false;
        timerCount = 0f;
        isAttacked = false;
    }

    private void Die()
    {
        if (OnDeath != null) OnDeath.Invoke();
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

    // attack every timeCount sec
    private void GetAttack()
    {
        if (isAttacked)
        {
            if (timerCount <= 0)
            {
                isAttacked = false;
                UnsetInvincible();
                if (OnUnsetAttacked != null) OnUnsetAttacked.Invoke();
            }
            timerCount -= Time.deltaTime;
        }  
    }

    private void Heal()
    {
        if (healAction.action.triggered)
        {
            healingPack.Heal(life);
            if (OnSetHealingPacks != null) OnSetHealingPacks.Invoke(healingPack.healingPacks);
            if (OnSetLife != null) OnSetLife.Invoke(life.health);
        }
    }
}
