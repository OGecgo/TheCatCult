using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private MonoBehaviour attacked;
    [Header("General settings")]
    [SerializeField] private LifeConf lifeConf;

    private IHealthUI attackedUI;
    private Slider sliderHelth;
    private int damageTaked;


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
        sliderHelth = GetComponent<Slider>();
        attackedUI = attacked.GetComponent<IHealthUI>();
        damageTaked = 0;
        sliderHelth.value = 1;
    }

    private void SetLife(int health)
    {
        sliderHelth.value = (float)health / lifeConf.health;
    }

}
