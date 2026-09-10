using System;
using System.Collections;
using UnityEngine;

public class PlayerLifeUI : MonoBehaviour, IDamageUI, IHealingPackUI, IHealthUI, ILowHealthUI
{
    [Header("Player added life settings")]
    [SerializeField] private LifeConf lifeConf;
    [SerializeField] [Range (0, 1)] private float precentShowLowHealth = 0.3f;
    [SerializeField] private float timerAttackShow = 1f;

    private Coroutine coroutine;
    private ILifeAction playerLife;
    private IHealingPackAction playerHealingPack;

    public event Action OnSetAttacked;
    public event Action OnUnsetAttacked;
    public event Action<int> OnSetLife;
    public event Action<int> OnSetHealingPacks;
    public event Action OnIsLowHealth;
    public event Action OnIsNotLowhealth;

    private void OnEnable()
    {
        playerLife.OnIsHit += UpdateUIAttacked;
        playerLife.OnSetHealth += UpdateUIHealth;
        playerHealingPack.OnUpdateHealibngPack += UpdateUIHelingPack;
    }
    private void OnDisable()
    {
        playerLife.OnIsHit -= UpdateUIAttacked;
        playerLife.OnSetHealth -= UpdateUIHealth;
        playerHealingPack.OnUpdateHealibngPack -= UpdateUIHelingPack;
    }

    private void Awake()
    {
        playerLife = this.GetComponent<ILifeAction>();
        playerHealingPack = this.GetComponent<IHealingPackAction>();
    }

    // ------- LIFE -------
    private void UpdateUIAttacked()
    {
        // attacked UI
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(ShowAttackedUI());
    }

    private void UpdateUIHealth(int health)
    {
        // update health UI
        if (OnSetLife != null) OnSetLife.Invoke(health);
        // update low health UI
        if (health/((float)lifeConf.health) <= precentShowLowHealth)
        {
            OnIsLowHealth?.Invoke();
        }
        else
        {
            OnIsNotLowhealth?.Invoke();
        }
    }



    private IEnumerator ShowAttackedUI()
    {
        // show attacked UI
        if (OnSetAttacked != null) OnSetAttacked.Invoke(); 
        yield return new WaitForSeconds(timerAttackShow);
        // unshow attacked UI
        if (OnUnsetAttacked != null) OnUnsetAttacked.Invoke();
    }

    // ------- HEALING PACK -------
    private void UpdateUIHelingPack(int healingPacks)
    {
        if (OnSetHealingPacks != null) OnSetHealingPacks.Invoke(healingPacks);
    }
}
