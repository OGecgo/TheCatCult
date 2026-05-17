using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{

    [Header("InputActions")]
    public InputActionReference moveAction;
    public InputActionReference runAction;
    public InputActionReference jumpAction;
    public InputActionReference rotationAction;

    [Header("Player Movements")]
    public float speedMove = 10f;
    public float heightJump = 1.5f;
    [Header("Player View")]
    public float sensitivity = 2f;
    // value between 0 andd 1
    public Vector2 speedDirections = new Vector2(1f, 1f); 
    public float FOV;
    [Range (0, 1)]


    private IPlayerMovement playerMovement;
    private IPlayerRotation playerRotation;

    private IPlayerHead playerHead;

    public void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        rotationAction.action.Enable();

    } 
    public void OnDisable() 
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        rotationAction.action.Disable();
    }

    public void Awake()
    {
        playerMovement = new PlayerMovement(this.GetComponent<CharacterController>(), moveAction, runAction, jumpAction, heightJump, speedMove);
        playerRotation = new PlayerRotation(rotationAction, sensitivity * speedDirections.x, new Vector3(1, 0, 0), transform); 

        playerHead = GetComponentInChildren<PlayerHead>();
        playerHead.sensitivity = sensitivity * speedDirections.y; 
        playerHead.FOV = FOV;
        playerHead.lookAction = rotationAction;
        playerHead.ManualAwake();
    }
 
    void Start()
    {
        playerHead.ManualStart();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        playerMovement.Update();
        playerRotation.Update();
        playerHead.ManualUpdate();


        // // debuging
        // playerMovement.UpdateHeightJump(heightJump);
        // playerMovement.UpdateSpeedMove(speedMove);
        // playerRotation.ChangeSensitivity(sensitivity);
    }

}
