using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTransform : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference runAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference rotationAction;
    
    [Header("Movement settings")]
    [SerializeField] private WalkConf walkConf;
    [SerializeField] private RunConf runConf;
    [SerializeField] private JumpConf jumpConf;
    [SerializeField] private FallConf fallConf;
    [Header("Rotation settins")]
    [SerializeField] private RotationConf headRotationConf;
    [SerializeField] private RotationConf bodyRotationConf;

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
