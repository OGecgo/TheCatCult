using UnityEngine;

public class PauseControl : MonoBehaviour, IPauseControl
{
    private bool _isPaused;
    private IPauseUpdate[] pauseUpdate;

    public bool isPaused {get{return _isPaused;}} 
    public void PauseGame()
    {
        _isPaused = true;

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
        _isPaused = false;

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
