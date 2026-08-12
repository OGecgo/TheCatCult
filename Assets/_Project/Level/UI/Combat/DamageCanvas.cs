using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI")]
public class DamageCanvas : MonoBehaviour
{
    // TODO: for future animations
    [Header("General settins")]
    public float timerShowDamage = 1f;
    public float timerHideDamage = 1f;
    [Header("Links")]
    public MonoBehaviour attacked;

    private IAttackedDamageUI attackedUI;
    private RawImage rawImage;

    public void OnDestroy()
    {
        attackedUI.OnSetAttacked -= SetAttacked;
        attackedUI.OnUnsetAttacked -= UnsetAttacked;

    }

    public void Awake()
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
