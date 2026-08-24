using System;
using System.Collections;
using UnityEngine;

public class EnemyCatLife : MonoBehaviour, IPauseFeatures, IIsAttacked, ILifeAction
{
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;

    private bool featureIsPaused;
    private ILife life;

    public event Action OnIsAttacked;
    public event Action<int> OnSetHealth;
    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }

    public void OnAttack(int power)
    {
        if (featureIsPaused) return;
        
        life.Attack(power);
        OnIsAttacked?.Invoke();
    }

    private void OnDestroy()
    {
        if (life != null)
        {
            life.OnDie -= EnemyDie;
        }
    }

   private void Awake()
    {
        featureIsPaused = false;
        life = new Life(lifeConf); 
        life.OnDie += EnemyDie;
    }



    private void EnemyDie() 
    {
        Destroy(this.gameObject);
    }
}
