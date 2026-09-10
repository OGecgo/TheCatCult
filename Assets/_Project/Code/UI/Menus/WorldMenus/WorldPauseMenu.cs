using UnityEngine;


public class WorldPauseMenu : MonoBehaviour
{

    [SerializeField] private MonoBehaviour _canvasController;
    [SerializeField] private MonoBehaviour _gameController;

    private ICanvasController canvasController;
    private IGameController gameController;


    private void OnEnable()
    {
        gameController.OnUnpause += ClosePause;
        gameController.OnPause += Pause;
    }
    
    private void OnDisable()
    {
        gameController.OnUnpause -= ClosePause;
        gameController.OnPause -= Pause;
    }
    
    private void Awake()
    {
        canvasController = _canvasController.GetComponent<ICanvasController>();
        gameController = _gameController.GetComponent<IGameController>();
        ClosePause();
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
