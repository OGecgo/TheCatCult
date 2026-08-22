using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndComicMenu : MonoBehaviour
{
    [SerializeField] private Button mainMenu;

    private void Awake()
    {
        mainMenu.onClick.AddListener(MainMenu);
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
