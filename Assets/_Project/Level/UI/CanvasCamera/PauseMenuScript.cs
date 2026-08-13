using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference pauseAction;
    [Header("Canvases")]
    [SerializeField] private Canvas pause;
    [SerializeField] private Canvas settings;
    [Header("Pause managers")]
    [SerializeField] private MonoBehaviour[] updateMangers;

    private bool isClosed;
    private IPauseUpdate[] m_updateMangers;

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
        m_updateMangers = new IPauseUpdate[updateMangers.Length];
        for (int i = 0; i < updateMangers.Length; i++)
        {
            m_updateMangers[i] = updateMangers[i].GetComponent<IPauseUpdate>();
        }

        ControlClosePause();
    }

    private void Update()
    {
        if (pauseAction.action.triggered)
        {
            if (isClosed) ControlPause();
            else ControlClosePause();
        }
    }

    public void ControlClosePause()
    {
        isClosed = true;

        // run the game
        foreach(IPauseUpdate m in m_updateMangers)
        {
            m.ObjIsPaused(false);
        }

        // locked mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // canvases
        pause.enabled = false;
        settings.enabled = false;
    }

    public void ControlSettings()
    {
        pause.enabled = false;
        settings.enabled = true;
    }

    public void ControlPause()
    {
        isClosed = false;

        // pose the world
        foreach(IPauseUpdate m in m_updateMangers)
        {
            m.ObjIsPaused(true);
        }

        // free mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // canvases
        pause.enabled = true;
        settings.enabled = false;
    }

    public void ControlMainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
