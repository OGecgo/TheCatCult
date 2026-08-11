using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTransform : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference moveAction;
    public InputActionReference runAction;
    public InputActionReference jumpAction;
    public InputActionReference rotationAction;
    
    [Header("Movement settings")]
    [SerializeField] public WalkConf walkConf;
    [SerializeField] public RunConf runConf;
    [SerializeField] public JumpConf jumpConf;
    [SerializeField] public FallConf fallConf;
    [Header("Rotation settins")]
    [SerializeField] public RotationConf headRotationConf;
    [SerializeField] public RotationConf bodyRotationConf;

    private IPlayerMovement playerMovement;
    private IPlayerRotation bodyRotation;
    private IPlayerRotation headRotation;

    public void OnEnable()
    {
        moveAction.action.Enable();
        runAction.action.Enable();
        jumpAction.action.Enable();
        rotationAction.action.Enable();
    }

    public void OnDisable()
    {
        moveAction.action.Disable();
        runAction.action.Disable();
        jumpAction.action.Disable();
        rotationAction.action.Disable();
    }

    public void Awake()
    {
        playerMovement = new PlayerMovement
        (
            this.GetComponent<ICharacterGravity>(),
            new InputActionMovementRecord(moveAction, runAction, jumpAction),
            new PlayerConfRecord(walkConf, runConf, jumpConf, fallConf)
        );

        bodyRotation = new PlayerRotation
        (
            rotationAction,
            bodyRotationConf,
            transform
        );

        headRotation = new PlayerRotation
        (
            rotationAction,
            headRotationConf,
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
