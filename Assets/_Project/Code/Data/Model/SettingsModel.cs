using System;


[Serializable]
public struct SettingsModel
{
    public float gameVolume;
    public float backgroundVolume;
    public float soundVolume;

    public SettingsModel(float gameVolume, float backgroundVolume, float soundVolume)
    {
        this.gameVolume = gameVolume;
        this.backgroundVolume = backgroundVolume;
        this.soundVolume = soundVolume;
    }
}
