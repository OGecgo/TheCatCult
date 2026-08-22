using UnityEngine;

public class WorldDeathMenu: MonoBehaviour
{
    [SerializeField] private MonoBehaviour _CanvasController;
    [SerializeField] private MonoBehaviour _worldDeathMenuUI;

    private ICanvasController CanvasController;
    private IWorldDeathMenuUI worldDeathMenuUI;

    private void OnEnable()
    {
        worldDeathMenuUI.OnDeath += Death;
    }

    private void OnDisable()
    {
        worldDeathMenuUI.OnDeath += Death;
    }

    private void Awake()
    {
        CanvasController = _CanvasController.GetComponent<ICanvasController>();
        worldDeathMenuUI = _worldDeathMenuUI.GetComponent<IWorldDeathMenuUI>();
    }

    private void Death()
    {
        CanvasController.TurnOnCanvas("CanvasDeath");
    }



}
