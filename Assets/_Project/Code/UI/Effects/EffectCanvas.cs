using Unity.VisualScripting;
using UnityEngine;

public class EffectCanvas : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _canvasController;
    [SerializeField] private MonoBehaviour _effectsUI;

    private IEffectUI effectsUI;
    private ICanvasController canvasController;

    private void OnEnable()
    {
        effectsUI.OnEnableEffect += TurnOn;
        effectsUI.OnDisableEffect += TurnOff;
    }

    private void OnDisable()
    {
        effectsUI.OnEnableEffect -= TurnOn;
        effectsUI.OnDisableEffect -= TurnOff;
    }

    private void Awake()
    {
        effectsUI = _effectsUI.GetComponent<IEffectUI>();
        canvasController = _canvasController.GetComponent<ICanvasController>();
    }

    private void TurnOn()
    {
        canvasController.TurnOnCanvas("CanvasEffects");
    }

    private void TurnOff()
    {
        canvasController.TurnOffCanvas("CanvasEffects");
    }

}
