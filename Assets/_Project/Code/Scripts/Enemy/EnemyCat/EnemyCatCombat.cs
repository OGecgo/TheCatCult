using UnityEngine;

public class EnemyCatCombat : MonoBehaviour, IAwakable, IDamageable
{
    [SerializeField] LifeConf lifeConf;

    private ILife life;

    public void Onestroy()
    {
        if (life != null) life.OnDie -= this.EnemyDie;        
    }

    public void ManualAwake()
    {
        life = new Life(lifeConf);
        life.OnDie += this.EnemyDie;
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
