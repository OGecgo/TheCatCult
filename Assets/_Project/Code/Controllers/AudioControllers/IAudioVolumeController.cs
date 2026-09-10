public interface IAudioVolumeController
{
    public string exposedParameterAudioMixer {get; set;}
    public void SetVolume(float volumePrecent);
}
