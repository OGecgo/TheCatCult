using UnityEngine;

public class ObjDoorSound : MonoBehaviour, IPauseFeatures
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string doorSoundName;

    private IInteractableDoor objDoor;
    private IAudioController audioController;
    private AudioClip doorSound;

    public void FeatureIsPaused(bool value)
    {
        if (value)
        {
            audioSource.Stop();
        }
    }

    private void Awake()
    {
        objDoor = this.GetComponent<IInteractableDoor>();
        audioController = _audioController.GetComponent<IAudioController>();
    }

    private void Start()
    {
        doorSound = audioController.GetClip(doorSoundName);
    }

    private void OnEnable()
    {
        objDoor.OnInteracted += OnInteractedSound;
    }

    private void OnDisable()
    {
        objDoor.OnInteracted -= OnInteractedSound;
    }

    private void OnInteractedSound()
    {
        audioSource.PlayOneShot(doorSound);
    }
}
