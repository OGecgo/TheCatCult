using UnityEngine;


public class WorldPauseMenu : MonoBehaviour
{

    [SerializeField] private MonoBehaviour _canvasController;
    [SerializeField] private MonoBehaviour _worldPauseMenuUI;

    private ICanvasController canvasController;
    private IWorldPauseMenuUI worldPauseMenuUI;


    private void OnEnable()
    {
        worldPauseMenuUI.OnUnpause += ClosePause;
        worldPauseMenuUI.OnPause += Pause;
    }
    
    private void OnDisable()
    {
        worldPauseMenuUI.OnUnpause -= ClosePause;
        worldPauseMenuUI.OnPause -= Pause;
    }
    
    private void Awake()
    {
        canvasController = _canvasController.GetComponent<ICanvasController>();
        worldPauseMenuUI = _worldPauseMenuUI.GetComponent<IWorldPauseMenuUI>();
    }

    private void ClosePause()
    {
        canvasController.TurnOffCanvas("CanvasPause");
        canvasController.TurnOffCanvas("CanvasSettings");
    }

    private void Pause()
    {
        canvasController.TurnOnCanvas("CanvasPause");
        canvasController.TurnOffCanvas("CanvasSettings");
    }
}
