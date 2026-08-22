using UnityEngine;

public interface IAudioController
{
    public void SetSFX(string name);
    public void SetBackground(string name);

    public void PlaySFX();
    public void PlayBackground();

    public void StopSFX();
    public void StopBackground();
} 
