using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private Canvas settings;

    private void Awake()
    {
        ControlMainMenu();
    }
    public void ControlStartGame()
    {
        SceneManager.LoadSceneAsync("World");
    }

    public void ControlMainMenu()
    {
        settings.enabled = false;
        mainMenu.enabled = true;
    }

    public void ControlSettings()
    {
        settings.enabled = true;
        mainMenu.enabled = false;
    }

    public void ControlQuit()
    {
        Application.Quit();
    }
}
