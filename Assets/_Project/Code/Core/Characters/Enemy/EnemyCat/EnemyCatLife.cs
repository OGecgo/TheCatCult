using System;
using System.Collections;
using UnityEngine;

public class EnemyCatCombat : MonoBehaviour, IDamageable, IPauseFeatures, IIsAttaked
{
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;
    [Header("Enemy take damage")]
    [SerializeField] private float timeDamageWillShowed = 0.3f;
    [SerializeField] private Color colorWhenTakedDamage = Color.red;

    private bool featureIsPaused;
    private bool isAttacked;
    private ILife life;
    private MeshRenderer meshRenderer;

    public event Action OnIsAttaked;
    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }

    public void Attack(int power)
    {
        if (featureIsPaused) return;
        
        life.Attack(power);
        // avoid star >1 routines
        if (!isAttacked)
        {
            // show to player. cat is attacked
            StartCoroutine(ShowDamageTaked());
            if (OnIsAttaked != null) OnIsAttaked.Invoke();
        }
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
        isAttacked = false;
        meshRenderer = GetComponent<MeshRenderer>();
        life = new Life(lifeConf); 
        life.OnDie += EnemyDie;
    }

    private IEnumerator ShowDamageTaked()
    {
        isAttacked = true;
        Color tempColor = meshRenderer.material.color;
        meshRenderer.material.color = colorWhenTakedDamage;
        yield return new WaitForSeconds(timeDamageWillShowed);
        meshRenderer.material.color = tempColor;
        isAttacked = false;
    }

    private void EnemyDie()
    {
        Destroy(this.gameObject);
    }
}
