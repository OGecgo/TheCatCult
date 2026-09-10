using UnityEngine;

public interface IAudioController
{
    public void PlayOneShotSFX(string nameClip);

    public void SetBackground(string nameSource, string nameClip);
    // if clip play onto nameSource. Do noting (first stop clip and then play new clip)
    public void PlayBackground(string nameSource, float timeToPlay = 0f);
    public bool IsPlayedBackground(string nameSource);
    public string GetPlayedClipBackground(string nameSrource);
    // if timeToStop/timeToPause is 0. vVlum not reseted. they just stop sound 
    public void StopBackground(string nameSource, float timeToStop = 0f);
    public void PauseBackground(string nameSource, float timeToPause = 0f);

    public AudioClip GetClip(string nameClip);
} 
