using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHead : MonoBehaviour
{
    [Header("InputActiuons")]
    public InputActionReference lookAction;

    [Header("Player Rotation")]
    public float sensitivityLook;

    private IPlayerRotation playerRotation;


    public void OnEnable()
    {
        lookAction.action.Enable();
    }
    public void OnDisable()
    {
        lookAction.action.Disable();
    }
    public void Awake()
    {
        playerRotation = new PlayerRotation();
        playerRotation.Initialize(lookAction, sensitivityLook, Directions.Pitch, this.transform);
    }
    void Update()
    {
        playerRotation.Update();
        // only for debuging Debuging
        playerRotation.ChangeSensitivity(sensitivityLook);
    }
}
