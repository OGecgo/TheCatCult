using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    [Header("Links")]
    public MonoBehaviour attacked;
    [Header("General settings")]
    public LifeConf lifeConf;

    private IAttackedHelthUI attackedUI;
    private Slider sliderHelth;
    private int damageTaked;


    public void OnDestroy()
    {
        attackedUI.OnAttacked -= Attacked;

    }

    public void Awake()
    {
        sliderHelth = GetComponent<Slider>();
        attackedUI = attacked.GetComponent<IAttackedHelthUI>();
        if (attacked == null) Debug.Log("wtf");
        attackedUI.OnAttacked += Attacked;
        damageTaked = 0;
        sliderHelth.value = 1;
    }

    private void Attacked(int damage)
    {
        damageTaked += damage;
        sliderHelth.value = ((float)(lifeConf.health - damageTaked)) / lifeConf.health;
    }

}
