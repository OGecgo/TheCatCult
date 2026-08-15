using TMPro;
using UnityEngine;

public class HealingPackCanvas: MonoBehaviour
{
    [SerializeField] private MonoBehaviour healed;
    [SerializeField] private TextMeshProUGUI textHealingPacks;

    private IHealingPackUI healingPackUI;

    private void OnEnable()
    {
        healingPackUI.OnSetHealingPacks += SetHealingPack;
    }

    private void OnDisable()
    {
        healingPackUI.OnSetHealingPacks -= SetHealingPack;      
    }

    private void Awake()
    {
        healingPackUI = healed.GetComponent<IHealingPackUI>();
    }


    private void SetHealingPack(int num)
    {
        textHealingPacks.text = "x" + num;
    }
}
