using UnityEngine;

public class PauseController : MonoBehaviour, IPauseController
{
    private IPauseUpdate[] pauseUpdate;

    public bool isPaused {get; private set;} 
    public void PauseGame()
    {
        isPaused = true;

        // pose the world
        foreach(IPauseUpdate m in pauseUpdate)
        {
            m.ObjIsPaused(true);
        }

        // free mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void UnpauseGame()
    {
        isPaused = false;

        // run the game
        foreach(IPauseUpdate m in pauseUpdate)
        {
            m.ObjIsPaused(false);
        }

        // locked mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    private void Awake()
    {
        pauseUpdate = GetComponentsInChildren<IPauseUpdate>();
    }
}
