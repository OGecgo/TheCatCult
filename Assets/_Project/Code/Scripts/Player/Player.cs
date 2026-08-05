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

    [Header("Player rotation")]
    public float sensitivity = 2f;
    // value between 0 andd 1
    public Vector2 speedDirections = new Vector2(1f, 1f);


    private IPalyerMovement playerMovement;
    private IPlayerRotation bodyRotation;
    private IPlayerRotation headRotation;

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

        bodyRotation = new PlayerRotation
        (
            rotationAction,
            sensitivity * speedDirections.x,
            new Vector3(1, 0, 0), transform
        );

        headRotation = new PlayerRotation
        (
            rotationAction,
            sensitivity * speedDirections.y,
            new Vector3(0, 1, 0),
            // 0 is the head
            this.transform.GetChild(0)
        );
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        bodyRotation.ManualUpdate();
        headRotation.ManualUpdate();
        playerMovement.ManualUpdate();
    }
}
