using UnityEngine;

public interface IAudioController
{
    public void SetBackground(string nameSource, string nameClip);


    public void PlayOneShotSFX(string nameClip);

    public void PlayBackground(string nameSource, float timeToPlay = 0f);
    // if timeToStop - timeToPause is 0 => volum not reseted. they just stop sound 
    public void StopBackground(string nameSource, float timeToStop = 0f);

    public void PauseBackground(string nameSource, float timeToPause = 0f);
} 
