using UnityEngine;

public class EnemyCatAttackCollider : MonoBehaviour, IPauseFeatures
{
    [Header("General settins")]
    [SerializeField] private MeleeWeaponConf meleeWeaponConf;
    [SerializeField] private float timerAttack = 1f;

    private bool featureIsPaused;
    private float timerCount;
    private IMeleeWeapon meleeWeapon;

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }
    
    private void Awake()
    {
        featureIsPaused = false;
        meleeWeapon = new MeleeWeapon(meleeWeaponConf);
        timerCount = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (featureIsPaused) return;

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
