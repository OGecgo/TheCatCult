using UnityEngine;

public class ObjInteractSound : MonoBehaviour, IPauseFeatures
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string clipName;

    private IInteracted interact;
    private IAudioController audioController;
    private AudioClip clip;

    public void FeatureIsPaused(bool value)
    {
        if (value)
        {
            audioSource.Stop();
        }
    }

    private void Awake()
    {
        interact = this.GetComponent<IInteracted>();
        audioController = _audioController.GetComponent<IAudioController>();
    }

    private void Start()
    {
        clip = audioController.GetClip(clipName);
    }

    private void OnEnable()
    {
        interact.OnInteracted += OnInteractedSound;
    }

    private void OnDisable()
    {
        interact.OnInteracted -= OnInteractedSound;
    }

    private void OnInteractedSound()
    {
        audioSource.PlayOneShot(clip);
    }
}
