using UnityEngine;

public class EnemyCatCombat : MonoBehaviour, IAwakable, IDamageable
{
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
        if (life == null)
        {
            life = new Life(lifeConf);
        }

        life.OnDie -= EnemyDie;
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
