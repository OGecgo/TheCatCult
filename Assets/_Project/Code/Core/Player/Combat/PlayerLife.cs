using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLife : MonoBehaviour, 
    IUpdatable, IPauseFeatures, 
    IDamageable, 
    IInteractHealth,
    IDamageUI, IHealingPackUI, IHealthUI, ILowHealthUI

{   
    [SerializeField] private MonoBehaviour _gameController;
    [Header("Input")]
    [SerializeField] private InputActionReference healAction;
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;
    [SerializeField] private HealingPackConf healingPackConf;
    [Header("Player take damage settigs")]
    [SerializeField] private float pushUp = 0.5f;
    [SerializeField] private float pushBack = 1f;
    [SerializeField] private float timerInvincible = 1f;
    [Header("Player added life settings")]
    [SerializeField] [Range (0, 1)] private float precentShowLowHealth = 0.3f;


    private IGameController gameController;
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
    public event Action<int> OnSetHealingPacks;
    public event Action OnIsLowHealth;
    public event Action OnIsNotLowhealth;

    public void ManualUpdate()
    {
        GetAttack();
        Heal();
        LowHealth();
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

        characterGravity = this.GetComponent<ICharacterGravity>();
        gameController = _gameController.GetComponent<IGameController>();

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

    private void Die()
    {
        gameController.DeathMode();
    }

    // ---- function used on Update ----
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

    private void LowHealth()
    {
        if (life.healthPrecent <= precentShowLowHealth)
        {
            OnIsLowHealth.Invoke();
        }
        else
        {
            OnIsNotLowhealth.Invoke();
        }
    }
}
