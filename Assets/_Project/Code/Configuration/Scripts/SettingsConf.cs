using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsConf", menuName = "Scriptable Objects/SettingsConf")]
public class SettingsConf : ScriptableObject
{
    public float gameVolume = 0.5f;
    public float backgroundVolume = 0.5f;
    public float soundVolume = 0.5f;

    public string fileName = "settings";
}
