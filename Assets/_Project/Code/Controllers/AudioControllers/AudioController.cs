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

    [SerializeField] private ClipData[] clips;

    // class because i needed pointer to data
    private class SourceDataExtended
    {
        public AudioSource source;
        public Coroutine coroutine;
        public string currentClip;
        public SourceDataExtended(AudioSource source)
        {
            this.source = source;
            this.coroutine = null;
            this.currentClip = "";
        }
    }


    // using dictionary fast find sound O(1)
    private Dictionary<string, SourceDataExtended> backgroundSourceDict;
    private Dictionary<string, AudioClip> clipDict;


    public void SetBackground(string nameSource, string nameClip)
    {
        if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended source))
        {
            if (clipDict.TryGetValue(nameClip, out AudioClip clip))
            {
                source.source.clip = clip;
                source.currentClip = nameClip;
            }
            else
            {
                Debug.LogError("AudioController Error:: SetBackground:\n Clip with name=" + nameClip + " not exist");
            }
        }
        else
        {
            Debug.LogError("AudioController Error:: SetBackground:\n Source with name=" + nameSource + " not exist");
        }
    }


    public void PlayOneShotSFX(string nameClip)
    {
        if (clipDict.TryGetValue(nameClip, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);            
        }
        else
        {
            Debug.LogError("AudioController Error:: PlayOneShotSFX:\n Clip with name=" + nameClip + " not exist");
        }
    }

    public void PlayBackground(string nameSource, float timeToPlay = 0f)
    {
        if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended sourceExtended))
        {
            if (sourceExtended.coroutine == null || !sourceExtended.source.isPlaying)
            {
                // if not running and coroutine is not null (stop or pause coroutine is running or nothing is running)
                if (sourceExtended.coroutine != null) StopCoroutine(sourceExtended.coroutine);

                sourceExtended.coroutine = StartCoroutine(CoroutinePlayMusic(sourceExtended, timeToPlay));
            }
        }
        else
        {
            Debug.LogError("AudioController Error:: PlayBackground:\n Source with name=" + nameSource + " not exist");
        }
    }

    public bool IsPlayedBackground(string nameSource)
    {
       if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended sourceExtended))
        {
            return sourceExtended.source.isPlaying;
        }
        else
        {
            Debug.LogWarning("AudioController Warning:: IsPlayedBackground:\n Source with name=" + nameSource + " not exist");
            return false;
        }
    }

    public string GetPlayedClipBackground(string nameSource)
    {
       if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended sourceExtended))
        {
            return sourceExtended.currentClip;
        }
        else
        {
            Debug.LogError("AudioController Error:: GetPlayedClipBackground:\n Source with name=" + nameSource + " not exist");
            return "";
        }
    }

    public void StopBackground(string nameSource, float timeToStop = 0f)
    {
        if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended sourceExtended))
        {
            if (sourceExtended.coroutine != null && sourceExtended.source.isPlaying)
            {
                // that will execute only if is running. (play coroutine is running or nothing is runnning)
                StopCoroutine(sourceExtended.coroutine);
            } 
            sourceExtended.coroutine = StartCoroutine(CoroutineCloseMusic(sourceExtended, timeToStop, sourceExtended.source.Stop));
        }
        else
        {
            Debug.LogError("AudioController Error:: StopBackground:\n Source with name=" + nameSource + " not exist");
        }
    }

    public void PauseBackground(string nameSource, float timeToPause = 0f)
    {
        if (backgroundSourceDict.TryGetValue(nameSource, out SourceDataExtended sourceExtended))
        {
            if (sourceExtended.coroutine != null && sourceExtended.source.isPlaying)
            {
                // that will execute only if is running. (play coroutine is running or nothing is runnning)
                StopCoroutine(sourceExtended.coroutine);
            } 
            sourceExtended.coroutine = StartCoroutine(CoroutineCloseMusic(sourceExtended, timeToPause, sourceExtended.source.Pause));
        }
        else
        {
            Debug.LogError("AudioController Error:: PauseBackground:\n Source with name=" + nameSource + " not exist");
        }
    }

    public AudioClip GetClip(string nameClip)
    {
        if (clipDict.TryGetValue(nameClip, out AudioClip clip))
        {
            return clip;
        }
        else
        {
            Debug.LogWarning("AudioController Warning:: GetClip:\n Source with name=" + nameClip + " not exist");
            return null;
        }
    }


    private void Awake()
    {
        // clips save to dictionary
        clipDict = new Dictionary<string, AudioClip>();
        foreach (ClipData s in clips)
        {
            clipDict.Add(s.name, s.clip);
        }
        // sources
        backgroundSourceDict = new Dictionary<string, SourceDataExtended>();
        foreach (SourceData s in backgroundSource)
        {
            backgroundSourceDict.Add(s.name, new SourceDataExtended(s.source));
        // reset volume
            s.source.volume = 1f;
        }

        sfxSource.volume = 1f;
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
