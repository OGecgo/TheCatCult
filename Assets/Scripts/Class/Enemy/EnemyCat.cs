using UnityEngine;

public class EnemyCat : MonoBehaviour, IUpdateManagerObjectEnemyCat
{
    private IFOVDetection fovD;
    private IEnemyCatRotation rotation;
    private IEnemyCatMovement movement;

    public void InitializeFOVDirection(LayerMask targetMask, LayerMask obstructionMask, float radius, float angle)
    {
        fovD = GetComponent<FOVDetection>();
        // fovD = gameObject.AddComponent<FOVDetection>();
        fovD.Initialize(targetMask, obstructionMask, gameObject.transform, radius, angle);
    }
    public void InitializeMovement(float heightJump, float speedMove)
    {
        movement = new EnemyCatMovement();
        movement.Initialize(gameObject.GetComponent<CharacterController>(), Vector3.zero, heightJump, speedMove);
    }
    public void InitializeRotation(float sensitivity)
    {
        rotation = new EnemyCatRotation();
        rotation.Initialize(sensitivity, gameObject.GetComponent<Transform>());
    }

    public void ManualAwake()
    {
        
    }
    public void ManualStart()
    {
        fovD.StartDetection();
    }
    public void ManualUpdate()
    {
        if (fovD.isTarget)
        {
            movement.UpdateMoveTo(Vector3.forward);
        }
        else
        {
            movement.UpdateMoveTo(Vector3.zero);
        }
        rotation.posTarget = fovD.posTarget;
        rotation.Update();
        movement.Update();
    }
}
