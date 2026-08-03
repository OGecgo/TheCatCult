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
    [SerializeField] public WalkConf walkConf;
    [SerializeField] public RunConf runConf;
    [SerializeField] public JumpConf jumpConf;
    [SerializeField] public FallConf fallConf;

    [Header("Player View")]
    public float sensitivity = 2f;
    // value between 0 andd 1
    public Vector2 speedDirections = new Vector2(1f, 1f); 
    public float FOV = 70f;

    private IPlayerHead playerHead;
    private IPalyerMovement playerMovement;
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
        playerMovement = new PlayerMovement
        (
            this.GetComponent<CharacterController>(),
            new InputActionMovementRecord(moveAction, runAction, jumpAction),
            new PlayerConfRecord(walkConf, runConf, jumpConf, fallConf)
        );
        playerRotation = new PlayerRotation(rotationAction, sensitivity * speedDirections.x, new Vector3(1, 0, 0), transform); 

        playerHead = GetComponentInChildren<PlayerHead>();
        playerHead.sensitivity = sensitivity * speedDirections.y; 
        playerHead.FOV = FOV;
        playerHead.rotationAction = rotationAction;
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
        playerHead.ManualUpdate();
        playerRotation.ManualUpdate();
        playerMovement.ManualUpdate();
    }

}
