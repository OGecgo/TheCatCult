using UnityEngine;

public class EnemyCatAttackCollider : MonoBehaviour, IPauseFeatures
{
    [Header("General settins")]
    [SerializeField] private MeleeWeaponConf meleeWeaponConf;

    private bool featureIsPaused;
    private IMeleeWeapon meleeWeapon;

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }
    
    private void Awake()
    {
        featureIsPaused = false;
        meleeWeapon = new MeleeWeapon(meleeWeaponConf);
    }

    private void OnTriggerStay(Collider other)
    {
        if (featureIsPaused) return;

        if (meleeWeapon.TestCollider(other)) meleeWeapon.Attack(other); 
    }

    // if collider go out and timer is not == 0
    private void OnTriggerExit(Collider other)
    {
        meleeWeapon.ResetTimer();
    }
}
