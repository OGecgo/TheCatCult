using UnityEngine;

public class audioCollider : MonoBehaviour, IPauseFeatures
{
    [SerializeField] private string soundName;
    [SerializeField] private string channelName;
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private float timeToPlay = 5f;
    [SerializeField] private float timeToPause = 3f;

    private IAudioController audioController;
    private bool featureIsPaused;
    private LayerMask playerLayers;

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }

    private void Awake()
    {
        audioController = _audioController.GetComponent<IAudioController>();
        featureIsPaused = false;
        playerLayers = LayerMask.GetMask("PlayerLayer", "PlayerInvisibleLayer");
    }
    private void OnTriggerEnter(Collider other)
    {
        // only if player enter to collider
        if (((1 << other.gameObject.layer) & playerLayers) != 0)
        {
            audioController.SetBackground("AudioMusicChannel", soundName);
            audioController.PlayBackground("AudioMusicChannel", timeToPlay);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // only if player stay in collider
        if (((1 << other.gameObject.layer) & playerLayers) != 0)
        {
            if (!featureIsPaused)
            {
                audioController.SetBackground("AudioMusicChannel", soundName);
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
        if (((1 << other.gameObject.layer) & playerLayers) != 0)
        {
            audioController.PauseBackground("AudioMusicChannel", timeToPause);
        }
    }
}
