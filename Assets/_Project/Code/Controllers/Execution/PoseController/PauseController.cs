using UnityEngine;

public class PauseController : MonoBehaviour, IPauseController
{
    private IPauseUpdate[] pauseUpdate;
    private IPauseFeatures[] pauseFeatures;

    public bool isPaused {get; private set;} 
    public void PauseGame()
    {
        isPaused = true;

        // pose the world
        foreach(IPauseUpdate update in pauseUpdate)
        {
            if ((UnityEngine.Object)update != null) update.UpdateIsPaused(true);
        }

        foreach (IPauseFeatures feature in pauseFeatures)
        {
            if ((UnityEngine.Object)feature != null) feature.FeatureIsPaused(true);
        }

        // free mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void UnpauseGame()
    {
        isPaused = false;

        // run the game
        foreach(IPauseUpdate update in pauseUpdate)
        {
            if ((UnityEngine.Object)update != null) update.UpdateIsPaused(false);
        }

        foreach (IPauseFeatures feature in pauseFeatures)
        {
            if ((UnityEngine.Object)feature != null) feature.FeatureIsPaused(false);
        }

        // locked mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    private void Awake()
    {
        pauseUpdate = GetComponentsInChildren<IPauseUpdate>();
        pauseFeatures = GetComponentsInChildren<IPauseFeatures>();
    }
}
