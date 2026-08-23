using UnityEngine;

public class AudioForestCollider : MonoBehaviour, IPauseFeatures
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private float timeToPlay = 5f;
    [SerializeField] private float timeToPause = 3f;

    private IAudioController audioController;
    private bool featureIsPaused;

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }

    private void Awake()
    {
        audioController = _audioController.GetComponent<IAudioController>();
        featureIsPaused = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // only if player enter to collider
        if (LayerMask.NameToLayer("PlayerLayer") == other.gameObject.layer)
        {
            audioController.SetBackground("AudioMusicChannel", "Forest_Day");
            audioController.PlayBackground("AudioMusicChannel", timeToPlay);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // only if player stay in collider
        if (LayerMask.NameToLayer("PlayerLayer") == other.gameObject.layer)
        {
            if (!featureIsPaused)
            {
                audioController.SetBackground("AudioMusicChannel", "Forest_Day");
                audioController.PlayBackground("AudioMusicChannel", timeToPlay);
            }
            else
            {
                audioController.PauseBackground("AudioMusicChannel");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // only if player exit from collider
        if (LayerMask.NameToLayer("PlayerLayer") == other.gameObject.layer)
        {
            audioController.PauseBackground("AudioMusicChannel", timeToPause);
        }
    }
}
