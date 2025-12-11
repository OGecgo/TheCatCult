using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{

    [Header("InputActions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference rotationAction;

    [Header("Player Movements")]
    public float speedMove;
    public float heightJump;
    public float sensitivityLook;



    private IPlayerMovement playerMovement;
    private IPlayerRotation playerRotation;


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
        playerMovement = new PlayerMovement();
        playerMovement.Initialize(gameObject.GetComponent<CharacterController>(), moveAction, jumpAction, heightJump, speedMove);
        
        playerRotation = new PlayerRotation(); 
        playerRotation.Initialize(rotationAction, sensitivityLook, Directions.Yaw, this.transform);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        playerMovement.Update();
        playerRotation.Update();
        // debuging
        playerMovement.UpdateHeightJump(heightJump);
        playerMovement.UpdateSpeedMove(speedMove);
        playerRotation.ChangeSensitivity(sensitivityLook);
    }


    void FixedUpdate()
    {
    }
}
