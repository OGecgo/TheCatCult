using System.Collections;
using UnityEngine;

public class EnemyCatCombat : MonoBehaviour, IDamageable, IPauseFeatures
{
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;
    [Header("Enemy take damage")]
    [SerializeField] private float timeDamageWillShowed = 1f;
    [SerializeField] private Color colorWhenTakedDamage = Color.red;

    private bool featureIsPaused;
    private ILife life;
    private MeshRenderer meshRenderer;


    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }

    public void Attack(int power)
    {
        if (featureIsPaused) return;
        
        life.Attack(power);
        // show to player. cat is attacked
        StartCoroutine(ShowDamageTaked());
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
        meshRenderer = GetComponent<MeshRenderer>();
        life = new Life(lifeConf); 
        life.OnDie += EnemyDie;
    }

    private IEnumerator ShowDamageTaked()
    {
        Color tempColor = meshRenderer.material.color;
        meshRenderer.material.color = colorWhenTakedDamage;
        yield return new WaitForSeconds(timeDamageWillShowed);
        meshRenderer.material.color = tempColor;
    }

    private void EnemyDie()
    {
        Destroy(this.gameObject);
    }
}
