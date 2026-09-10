using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation: MonoBehaviour, IUpdatable
{
    [SerializeField] private InputActionReference rotationAction;

    [SerializeField] private RotationConf headRotationConf;
    [SerializeField] private RotationConf bodyRotationConf;
    [SerializeField] private Transform transformHead;
    [SerializeField] private Transform transformBody;


    private IRotationControl rotationControlHead;
    private IRotationControl rotationControlBody;

    public void ManualUpdate()
    {
        Vector2 rotationDelta = rotationAction.action.ReadValue<Vector2>();
        rotationControlHead.UpdateLocalRotation(rotationDelta);
        rotationControlBody.UpdateLocalRotation(rotationDelta);
    }

    private void OnEnable()
    {
        rotationAction.action.Enable();
    }

    private void OnDisable()
    {
        rotationAction.action.Disable();
    }

    private void Awake()
    {
        rotationControlHead = new RotationControl(headRotationConf, transformHead);
        rotationControlBody = new RotationControl(bodyRotationConf, transformBody);
    }

} 
