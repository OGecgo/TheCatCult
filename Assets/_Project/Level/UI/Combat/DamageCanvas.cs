using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI")]
public class DamageCanvas : MonoBehaviour
{
    // TODO: for future animations
    // [Header("General settins")]
    // [SerializeField] private float timerShowDamage = 1f;
    // [SerializeField] private float timerHideDamage = 1f;
    [Header("Links")]
    [SerializeField] private MonoBehaviour attacked;

    private IAttackedDamageUI attackedUI;
    private RawImage rawImage;

    private void OnDestroy()
    {
        attackedUI.OnSetAttacked -= SetAttacked;
        attackedUI.OnUnsetAttacked -= UnsetAttacked;

    }

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
        UnsetAttacked();
        attackedUI = attacked.GetComponent<IAttackedDamageUI>();
        attackedUI.OnSetAttacked += SetAttacked;
        attackedUI.OnUnsetAttacked += UnsetAttacked;
    }

    private void SetAttacked()
    {
        rawImage.enabled = true;
    }

    private void UnsetAttacked()
    {
        rawImage.enabled = false;    
    } 
}
