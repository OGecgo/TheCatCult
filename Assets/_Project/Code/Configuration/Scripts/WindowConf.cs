using UnityEngine;

[CreateAssetMenu(fileName = "WindowConf", menuName = "Scriptable Objects/WindowConf")]
public class WindowConf : ScriptableObject
{
    public Vector2 cameraDimensions = new Vector2(16f, 9f);
    [Range(50f, 70f)]
    public float FOV = 60f;

}
