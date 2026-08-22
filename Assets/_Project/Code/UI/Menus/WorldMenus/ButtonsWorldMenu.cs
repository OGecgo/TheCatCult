using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsWorldMenu : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _gameController;
    [SerializeField] private MonoBehaviour _canvasController;
    [Header("Pause Menu")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [Header("Setting Menu")]
    [SerializeField] private Button pauseMenuButton;
    [Header("Death Menu")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button goMainMenuButton;


    private IGameController gameController;
    private ICanvasController canvasController;

    private void Awake()
    {
        canvasController = _canvasController.GetComponent<ICanvasController>();
        gameController = _gameController.GetComponent<IGameController>();

        resumeButton.onClick.AddListener(ClosePause);
        settingsButton.onClick.AddListener(Settings);
        quitButton.onClick.AddListener(MainMenu);

        pauseMenuButton.onClick.AddListener(Pause);

        restartButton.onClick.AddListener(LevelRestart);
        goMainMenuButton.onClick.AddListener(MainMenu);
    }



    private void ClosePause()
    {
        gameController.PlayMode();
    }

    private void Pause()
    {
        canvasController.TurnOnCanvas("CanvasPause");
        canvasController.TurnOffCanvas("CanvasSettings");
    }

    private void Settings()
    {
        canvasController.TurnOffCanvas("CanvasPause");
        canvasController.TurnOnCanvas("CanvasSettings");

    }

    private void MainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    private void LevelRestart()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(buildIndex);
    }
}
