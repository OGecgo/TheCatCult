using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour, IAudioController
{
    private struct SFXSound
    {
        public string name;
        public AudioClip clip;
    }

    private struct BackgroundSound
    {
        public string name;
        public AudioClip clip;
    }


    [SerializeField] private AudioSource sfxAudio;
    [SerializeField] private AudioSource backgroundAudio;

    [SerializeField] private SFXSound[] sfxSound;
    [SerializeField] private BackgroundSound[] backgroundSound;

    // using dictionary fast find sound O(1)
    private Dictionary<string, AudioClip> sfxSoundDictionary;
    private Dictionary<string, AudioClip> backgroundSoundDictionary;

    public void SetSFX(string name)
    {
        if (sfxSoundDictionary.TryGetValue(name, out AudioClip clip))
        {
            backgroundAudio.clip = clip;
        }
        else
        {
            Debug.LogWarning("AudioManager Error: Sound with name=" + name + " not exist");
        }
    }

    public void SetBackground(string name)
    {
        if (backgroundSoundDictionary.TryGetValue(name, out AudioClip clip))
        {
            backgroundAudio.clip = clip;
        }
        else
        {
            Debug.LogWarning("AudioManager Error: Sound with name=" + name + " not exist");
        }
    }


    public void PlaySFX()
    {
        sfxAudio.Play();
    }

    public void PlayBackground()
    {
        backgroundAudio.Play();
    }

    public void StopSFX()
    {
        sfxAudio.Stop();
    }

    public void StopBackground()
    {
        backgroundAudio.Stop();
    }

    private void Awake()
    {
        backgroundSoundDictionary = new Dictionary<string, AudioClip>();
        foreach (BackgroundSound c in backgroundSound)
        {
            backgroundSoundDictionary.Add(c.name, c.clip);
        }

        sfxSoundDictionary = new Dictionary<string, AudioClip>();
        foreach (SFXSound s in sfxSound)
        {
            sfxSoundDictionary.Add(s.name, s.clip);
        }
    } 
}
