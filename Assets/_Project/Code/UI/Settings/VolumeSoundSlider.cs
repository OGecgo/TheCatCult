using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSoundSlider : MonoBehaviour
{

    [SerializeField] private MonoBehaviour _audioVolumeController; 
    [SerializeField] private string exposedParameterAudioMixer = "MasterVolume";

    private IAudioVolumeController audioVolumeController;

    private Slider sliderVolume;

    private void OnEnable()
    {
        sliderVolume.onValueChanged.AddListener(SliderListener);
    }

    private void OnDisable()
    {
        sliderVolume.onValueChanged.RemoveAllListeners();      
    }

    private void Awake()
    {
        audioVolumeController = _audioVolumeController.GetComponent<IAudioVolumeController>();
        audioVolumeController.exposedParameterAudioMixer = exposedParameterAudioMixer;
        sliderVolume = this.GetComponent<Slider>();
        sliderVolume.value = 1f;
    }

    private void SliderListener(float value){
        audioVolumeController.exposedParameterAudioMixer = exposedParameterAudioMixer;  
        audioVolumeController.SetVolume(value);
    }
}
