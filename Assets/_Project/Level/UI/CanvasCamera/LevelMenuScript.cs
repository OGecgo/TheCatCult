using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelMenuScript : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference pauseAction;
    [Header("Canvases")]
    [SerializeField] private Canvas pause;
    [SerializeField] private Canvas settings;
    [Header("Pause manager")]
    [SerializeField] private MonoBehaviour m_pauseControl;

    private IPauseControl pauseControl;
    private void OnEnable()
    {
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.Disable();
    }

    private void Start()
    {
        pauseControl = m_pauseControl.GetComponent<IPauseControl>();
        ControlClosePauseMenu();
    }

    private void Update()
    {
        if (pauseAction.action.triggered)
        {
            if (!pauseControl.isPaused) ControlPauseMenu();
            else ControlClosePauseMenu();
        }
    }

    public void ControlClosePauseMenu()
    {
        pauseControl.UnpauseGame();
        pause.enabled = false;
        settings.enabled = false;
    }

    public void ControlPauseMenu()
    {
        pauseControl.PauseGame();
        pause.enabled = true;
        settings.enabled = false;
    }

    public void ControlSettings()
    {
        pause.enabled = false;
        settings.enabled = true;
    }

    public void ControlMainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void ControlLevelRestart()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(buildIndex);
    }
}
