using UnityEngine;

public class AudioPaused : MonoBehaviour, IPauseFeatures
{
    [SerializeField] private AudioSource[] audioSources;
    
    public void FeatureIsPaused(bool value)
    {
        if (value)
        {
            foreach(AudioSource audioSource in audioSources)
            {
                audioSource.Pause();
            }
        }
        else
        {
            foreach(AudioSource audioSource in audioSources)
            {
                audioSource.Play(); 
            }
        }
    }


}
