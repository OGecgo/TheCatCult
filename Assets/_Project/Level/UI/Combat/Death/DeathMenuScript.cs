using UnityEngine;

public class DeathMenuScript : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas gameCanvas;
    [SerializeField] private Canvas deathCanvas;

    [Header("Links")]
    [SerializeField] private MonoBehaviour death;
    [SerializeField] private MonoBehaviour pauseControl;

    private IDeathUI deathUI;
    private IPauseControl _pauseControl;

    private void OnDisable()
    {
        deathUI.OnDeath += Death;
    }

    private void OnEnable()
    {
        deathUI.OnDeath += Death;
    }

    private void Awake()
    {
        _pauseControl = pauseControl.GetComponent<IPauseControl>();
        deathUI = death.GetComponent<IDeathUI>();
        deathCanvas.enabled = false;
        gameCanvas.enabled = true;
    }

    private void Death()
    {
        _pauseControl.PauseGame();
        deathCanvas.enabled = true;
        gameCanvas.enabled = false;
    }
}
