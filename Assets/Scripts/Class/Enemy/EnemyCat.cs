using UnityEngine;

public class EnemyCat : MonoBehaviour, IUpdateManagerObject
{

    [Header("Enemy Cat Config Movement")]
    public float speedMove = 5f;
    public float heightJump = 1f;
    public float sensitivity = 4f;
    public float speedDirections = 1f;




    private IFOVDetection fovD;
    private IEnemyCatRotation rotation;
    private IEnemyCatMovement movement;


    public void ManualAwake()
    {
        fovD = this.GetComponent<FOVDetection>();
        movement = new EnemyCatMovement();
        movement.Initialize(gameObject.GetComponent<CharacterController>(), Vector3.zero, heightJump, speedMove);
        rotation = new EnemyCatRotation();
        rotation.Initialize(sensitivity, speedDirections, gameObject.GetComponent<Transform>());
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
