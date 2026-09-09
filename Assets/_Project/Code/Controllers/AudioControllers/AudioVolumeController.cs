using System;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumController : MonoBehaviour, IAudioVolumeController
{
    [SerializeField] private AudioMixer audioMixer;
    [Tooltip("By default value")]
    [SerializeField] private string _exposedParameterAudioMixer = "MasterVolume";

    public string exposedParameterAudioMixer {get; set;}
    public void SetVolume(float volumePrecent)
    {
        float dB;
        // volumePrecent *= maxPrecentValue; 
        if (volumePrecent < 0.0001f)
        {
            dB = -80;
        }
        else
        {
            dB = 20f * (float)Math.Log10(volumePrecent);
        }

        if (!audioMixer.SetFloat(exposedParameterAudioMixer, dB)) Debug.LogWarning("AudioVolumeController:: Dont find parameter");        
    }

    private void Awake()
    {
        exposedParameterAudioMixer = _exposedParameterAudioMixer;
    }
}
