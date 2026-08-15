using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour
{
    // TODO: for future animations
    // [Header("General settins")]
    // [SerializeField] private float timerShowDamage = 1f;
    // [SerializeField] private float timerHideDamage = 1f;
    [Header("Links")]
    [SerializeField] private MonoBehaviour attacked;

    private IDamageUI attackedUI;
    private RawImage rawImage;

    private void OnDisable()
    {
        attackedUI.OnSetAttacked -= SetAttacked;
        attackedUI.OnUnsetAttacked -= UnsetAttacked;

    }

    private void OnEnable()
    {
        attackedUI.OnSetAttacked += SetAttacked;
        attackedUI.OnUnsetAttacked += UnsetAttacked;
    }
    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
        UnsetAttacked();
        attackedUI = attacked.GetComponent<IDamageUI>();
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
