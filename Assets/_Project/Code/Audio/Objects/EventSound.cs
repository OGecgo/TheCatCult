using System.Collections;
using UnityEngine;

public class EventSound : MonoBehaviour, IPauseFeatures
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private string audioClipName;
    [SerializeField] private AudioSource audioSource; 

    private IEventAction eventAction;
    private IAudioController audioController;
    private AudioClip clip;

    private Coroutine coroutine;
    private bool isRunCoroutine;
    private bool featureIsPaused;

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
        if (isRunCoroutine && featureIsPaused)
        {
            audioSource.Pause();
        }
        else if (isRunCoroutine && !featureIsPaused)
        {
            audioSource.Play();
        }
    }

    private void Awake()
    {
        audioController = _audioController.GetComponent<IAudioController>();
        eventAction = this.GetComponent<IEventAction>();
        featureIsPaused = false;
    }

    private void Start()
    {
        clip = audioController.GetClip(audioClipName);
    }

    private void OnEnable()
    {
        eventAction.OnEventTriggered += OnEventTriggeredSound;
    }

    private void OnDisable()
    {
        eventAction.OnEventTriggered -= OnEventTriggeredSound;
    }

    private void OnEventTriggeredSound(float time)
    {
        if (coroutine != null && isRunCoroutine)
        {
            StopCoroutine(coroutine);
            isRunCoroutine = false;
        }
        coroutine = StartCoroutine(PlaySoundCoroutine(time));
    }

    private IEnumerator PlaySoundCoroutine(float time)
    {
        float countTime = time;
        isRunCoroutine = true;
        audioSource.clip = clip;
        audioSource.Play();
        while(countTime > 0f)
        {
            // run only if not paused
            if (!featureIsPaused)
            {
                countTime -= Time.deltaTime;   
            }
            yield return null;
        }
        audioSource.Stop();
        isRunCoroutine = false;
    }
}
