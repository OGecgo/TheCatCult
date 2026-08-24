using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerLife : MonoBehaviour, IUpdatable, IPauseFeatures, 
    IIsAttacked, ILifeAction, IHealingPackAction,
    IInteractHealingPack
{   
    [SerializeField] private MonoBehaviour _gameController;
    [Header("Input")]
    [SerializeField] private InputActionReference healAction;
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;
    [SerializeField] private HealingPackConf healingPackConf;



    private IGameController gameController;
    private ILife life;
    private IHealingPack healingPack;
    private bool featureIsPaused;

    public event Action OnIsAttacked;
    public event Action<int> OnSetHealth;
    public event Action<int> OnUpdateHealibngPack;

    public void ManualUpdate()
    {
        // use healing pack
        if (healAction.action.triggered)
        {
            healingPack.Heal(life);
            OnUpdateHealibngPack(healingPack.healingPacks);
            OnSetHealth(life.health);
            
        }
    }

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }

    public void OnAttack(int power)
    {
        if (featureIsPaused) return;
        
        life.Attack(power);
        OnIsAttacked();
        OnSetHealth(life.health);
    }

    public void GetHealingPack()
    {
        healingPack.AddHealingPack();
        OnUpdateHealibngPack(healingPack.healingPacks);
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
        gameController = _gameController.GetComponent<IGameController>();
        featureIsPaused = false;
    }


    private void Die()
    {
        gameController.DeathMode();
    }

}
