using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private MonoBehaviour attacked;
    [SerializeField] private Image imageHealth;
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;

    private IHealthUI attackedUI;


    private void OnDisable()
    {
        attackedUI.OnSetLife -= SetLife;

    }

    private void OnEnable()
    {
        attackedUI.OnSetLife += SetLife;   
    }
    private void Awake()
    {
        attackedUI = attacked.GetComponent<IHealthUI>();
        imageHealth.fillAmount = 1;
    }

    private void SetLife(int health)
    {
        imageHealth.fillAmount = (float)health / lifeConf.health;
    }

}
