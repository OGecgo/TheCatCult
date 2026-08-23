using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour, IAudioController
{
    [Serializable]
    private struct SourceData
    {
        public string name;
        public AudioSource source;
    }

    [Serializable]
    private struct ClipData
    {
        public string name;
        public AudioClip clip;
    }


    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private SourceData[] backgroundSource;

    [SerializeField] private ClipData[] sfxClip;
    [SerializeField] private ClipData[] backgroundClips;

    // class because i needed pointer to data
    private class SourceDataExtended
    {
        public AudioSource source;
        public Coroutine coroutine;
        public bool isRunning;
        public SourceDataExtended(AudioSource source)
        {
            this.source = source;
            this.coroutine = null;
            this.isRunning = false;
        }
    }


    // using dictionary fast find sound O(1)
    private Dictionary<string, SourceDataExtended> backgroundSourceDict;
    private Dictionary<string, AudioClip> sfxClipDict;
    private Dictionary<string, AudioClip> backgroundClipDict;


    public void SetBackground(string nameSource, string nameClip)
    {
        if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended source))
        {
            if (backgroundClipDict.TryGetValue(nameClip, out AudioClip clip))
            {
                source.source.clip = clip;
            }
        }
        else
        {
            Debug.LogWarning("AudioManager Error: Sound with name=" + name + " not exist");
        }
    }


    public void PlayOneShotSFX(string nameClip)
    {
        if (sfxClipDict.TryGetValue(nameClip, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);            
        }
    }

    public void PlayBackground(string nameSource, float timeToPlay = 0f)
    {
        if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended sourceExtended))
        {
            if (sourceExtended.coroutine == null || !sourceExtended.isRunning)
            {
                // if not running and coroutine is not null (stop or pause coroutine is running or nothing is running)
                if (sourceExtended.coroutine != null) StopCoroutine(sourceExtended.coroutine);

                sourceExtended.coroutine = StartCoroutine(CoroutinePlayMusic(sourceExtended, timeToPlay));
                sourceExtended.isRunning = true;
            }
        }
    }

    public void StopBackground(string nameSource, float timeToStop = 0f)
    {
        if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended sourceExtended))
        {
            if (sourceExtended.coroutine != null && sourceExtended.isRunning)
            {
                // that will execute only if is running. (play coroutine is running or nothing is runnning)
                StopCoroutine(sourceExtended.coroutine);
                sourceExtended.coroutine = StartCoroutine(CoroutineCloseMusic(sourceExtended, timeToStop, sourceExtended.source.Stop));
                sourceExtended.isRunning = false;  
            } 
        }
    }

    public void PauseBackground(string nameSource, float timeToPause = 0f)
    {
        if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended sourceExtended))
        {
            if (sourceExtended.coroutine != null && sourceExtended.isRunning)
            {
                // that will execute only if is running. (play coroutine is running or nothing is runnning)
                StopCoroutine(sourceExtended.coroutine);
                sourceExtended.coroutine = StartCoroutine(CoroutineCloseMusic(sourceExtended, timeToPause, sourceExtended.source.Pause));
                sourceExtended.isRunning = false;  
            } 
        }
    }

    private void Awake()
    {
        // clips
        backgroundClipDict = new Dictionary<string, AudioClip>();
        foreach (ClipData c in backgroundClips)
        {
            backgroundClipDict.Add(c.name, c.clip);
        }

        sfxClipDict = new Dictionary<string, AudioClip>();
        foreach (ClipData s in sfxClip)
        {
            sfxClipDict.Add(s.name, s.clip);
        }
        // sources
        backgroundSourceDict = new Dictionary<string, SourceDataExtended>();
        foreach (SourceData s in backgroundSource)
        {
            backgroundSourceDict.Add(s.name, new SourceDataExtended(s.source));
        // reset volume
            s.source.volume = 0f;
        }

        sfxSource.volume = 0f;
    } 

    private IEnumerator CoroutinePlayMusic(SourceDataExtended source, float timeToPlay)
    {
        source.source.Play();
        if (timeToPlay == 0f)
        {
            source.source.volume = 1f;
            yield break;
        }


        float speed = 1f / timeToPlay;
        float currentTime = source.source.volume * timeToPlay; // start from stoped volume

        while (currentTime < timeToPlay)
        {
            currentTime += Time.deltaTime;

            source.source.volume = currentTime * speed;

            yield return null;
        }
        source.source.volume = 1f;
        yield return null;
    }

    private IEnumerator CoroutineCloseMusic(SourceDataExtended source, float timeToPlay, Action sourceAction)
    {
        if (timeToPlay == 0f)
        {
            sourceAction.Invoke();
            yield break;
        }


        float speed = 1f / timeToPlay;
        float currentTime = source.source.volume * timeToPlay; // start from stoped volume

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            source.source.volume = currentTime * speed;

            yield return null;
        }
        source.source.volume = 0f;
        sourceAction.Invoke();
        yield return null;
    }
}
