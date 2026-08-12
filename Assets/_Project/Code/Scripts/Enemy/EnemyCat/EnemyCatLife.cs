using System.Collections;
using UnityEngine;

public class EnemyCatCombat : MonoBehaviour, IAwakable, IDamageable
{
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;
    [Header("Enemy take damage")]
    [SerializeField] private float timeDamageWillShowed = 1f;
    [SerializeField] private Color colorWhenTakedDamage = Color.red;

    private ILife life;
    private MeshRenderer meshRenderer;

    public void OnDestroy()
    {
        if (life != null)
        {
            life.OnDie -= EnemyDie;
        }
    }

    public void ManualAwake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        life = new Life(lifeConf); 
        life.OnDie += EnemyDie;
    }

    public void Attack(int power)
    {
        life.Attack(power);
        // show to player. cat is attacked
        StartCoroutine(ShowDamageTaked());
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
