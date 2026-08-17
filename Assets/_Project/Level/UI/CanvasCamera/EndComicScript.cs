using UnityEngine;
using UnityEngine.SceneManagement;

public class EndComicScript : MonoBehaviour
{
    public void ControlMainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
}
