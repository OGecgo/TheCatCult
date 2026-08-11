using UnityEngine;

public class EnemyCatAttackCollider : MonoBehaviour, IAwakable
{
    [Header("General settins")]
    public MeleeWeaponConf meleeWeaponConf;
    public float timerAttack = 1f;

    private float timerCount;
    private IMeleeWeapon meleeWeapon;

    public void ManualAwake()
    {
        meleeWeapon = new MeleeWeapon(meleeWeaponConf);
        timerCount = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (meleeWeapon.TestCollider(other))
        {
            if (timerCount <= 0f) 
            {
                meleeWeapon.Attack(other); 
                timerCount = timerAttack;
            }
            else timerCount -= Time.deltaTime;
        }
    }

    // if collider go out and timer is not == 0
    private void OnTriggerExit(Collider other)
    {
        timerCount = 0f;
    }
}
