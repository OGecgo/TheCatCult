using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class LowLifeSound : MonoBehaviour, IPauseFeatures
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private string BackgroundSourcename;
    [SerializeField] private string ClipName;

    private ILowHealthUI lowHealthUI;
    private IAudioController audioController;

    public void FeatureIsPaused(bool value)
    {
        // only if played
        if (value && audioController.GetPlayedClipBackground(BackgroundSourcename) == ClipName && audioController.IsPlayedBackground(BackgroundSourcename))
        { 
            audioController.StopBackground(BackgroundSourcename);
        }
    }

    private void Awake()
    {
        lowHealthUI = this.GetComponent<ILowHealthUI>();
        audioController = _audioController.GetComponent<IAudioController>();

    }

    private void OnEnable()
    {
        lowHealthUI.OnIsLowHealth += OnIsLowHealthSound;
        lowHealthUI.OnIsNotLowhealth += OnIsNotLowHealthSound;
    }

    private void OnDisable()
    {
        lowHealthUI.OnIsLowHealth -= OnIsLowHealthSound;
        lowHealthUI.OnIsNotLowhealth -= OnIsNotLowHealthSound;
    }

    private void OnIsLowHealthSound()
    {
        if (audioController.GetPlayedClipBackground(BackgroundSourcename) == ClipName && audioController.IsPlayedBackground(BackgroundSourcename))
        {
            return;
        }
        audioController.SetBackground(BackgroundSourcename, ClipName);
        audioController.PlayBackground(BackgroundSourcename);
    }

    private void OnIsNotLowHealthSound()
    { 
        // only if played
        if (audioController.GetPlayedClipBackground(BackgroundSourcename) == ClipName && audioController.IsPlayedBackground(BackgroundSourcename))
        { 
            audioController.StopBackground(BackgroundSourcename);
        }
    }
}
