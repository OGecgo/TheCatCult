using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndComicMenu : MonoBehaviour
{
    [SerializeField] private Button mainMenu;
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private string clipName;

    private IAudioController audioController;

    private void Awake()
    {
        audioController = _audioController.GetComponent<IAudioController>();
        mainMenu.onClick.AddListener(MainMenu);
    }

    private void Start()
    {
        audioController.PlayOneShotSFX(clipName);
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
