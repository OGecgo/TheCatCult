using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [Header("Settings")]
    [SerializeField] private Canvas settings;
    [SerializeField] private Button mainMenuButton;



    private void Awake()
    {
        mainMenu.enabled = true;
        settings.enabled = false;
        startGameButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(Settings);
        exitButton.onClick.AddListener(Quit);
        mainMenuButton.onClick.AddListener(MainMenu);
    }
    private void StartGame()
    {
        StartCoroutine(WaitCoroutine());
    }

    private void Settings()
    {
        settings.enabled = true;
        mainMenu.enabled = false;
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void MainMenu()
    {
        settings.enabled = false;
        mainMenu.enabled = true;
    }

    // wait before change scene
    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync("World");
    }

}
