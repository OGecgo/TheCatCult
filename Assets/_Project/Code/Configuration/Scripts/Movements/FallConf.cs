using UnityEngine;

[CreateAssetMenu(fileName = "FallConf", menuName = "Scriptable Objects/FallConf")]
public class FallConf : ScriptableObject
{
    public float moveSpeed = 0.5f;
    public Vector2 speedDirection = new Vector2(1f, 1f);
}
