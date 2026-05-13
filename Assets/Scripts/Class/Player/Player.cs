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
    public float speedMove = 10f;
    public float heightJump = 1.5f;
    [Header("Player view")]
    public float sensitivity = 2f;
    public Vector2 speedDirections = new Vector2(1f, 1f);


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
        playerMovement = new PlayerMovement();
        playerMovement.Initialize(gameObject.GetComponent<CharacterController>(), moveAction, jumpAction, heightJump, speedMove);
        
        playerRotation = new PlayerRotation(); 
        playerRotation.Initialize(rotationAction, sensitivity, Directions.Yaw, new Vector2(speedDirections.x, 0f), transform);

        playerHead = GetComponentInChildren<PlayerHead>();
        playerHead.speedDirections = speedDirections.y;
        playerHead.sensitivity = sensitivity; 
        playerHead.ManualAwake();
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
        playerHead.ManualUpdate();



        // debuging
        playerMovement.UpdateHeightJump(heightJump);
        playerMovement.UpdateSpeedMove(speedMove);
        playerRotation.ChangeSensitivity(sensitivity);
    }


    void FixedUpdate()
    {
    }
}
