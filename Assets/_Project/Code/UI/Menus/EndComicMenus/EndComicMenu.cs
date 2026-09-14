using System.Collections;
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
        StartCoroutine(WaitCoroutine());
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync("Menu");
    }
}
