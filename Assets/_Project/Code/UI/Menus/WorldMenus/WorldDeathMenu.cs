using UnityEngine;

public class WorldDeathMenu: MonoBehaviour
{
    [SerializeField] private MonoBehaviour _canvasController;
    [SerializeField] private MonoBehaviour _gameController;

    private ICanvasController canvasController;
    private IGameController gameController;

    private void OnEnable()
    {
        gameController.OnDeath += Death;
    }

    private void OnDisable()
    {
        gameController.OnDeath -= Death;
    }

    private void Awake()
    {
        canvasController = _canvasController.GetComponent<ICanvasController>();
        gameController = _gameController.GetComponent<IGameController>();
    }

    private void Death()
    {
        canvasController.TurnOnCanvas("CanvasDeath");
    }



}
