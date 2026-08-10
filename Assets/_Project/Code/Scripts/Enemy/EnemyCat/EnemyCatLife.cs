using UnityEngine;

public class EnemyCatCombat : MonoBehaviour, IAwakable, IDamageable
{
    [Header("General settings")]
    [SerializeField] LifeConf lifeConf;

    private ILife life;

    public void OnDestroy()
    {
        if (life != null)
        {
            life.OnDie -= EnemyDie;
        }
    }

    public void ManualAwake()
    {
        life = new Life(lifeConf);
        life.OnDie += EnemyDie;
    }

    public void Attack(int power)
    {
        life.Attack(power);
    }

    private void EnemyDie()
    {
        Destroy(this.gameObject);
    }
}
