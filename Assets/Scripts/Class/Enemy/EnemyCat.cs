using UnityEngine;

public class EnemyCat : MonoBehaviour, IUpdateManagerObject
{

    [Header("Enemy Cat Config Movement")]
    public float speedMove = 5f;
    public float heightJump = 1f;
    public float sensitivity = 4f;
    public float timerCatWillAttack = 0;
    // enemy go to player in time timer_see != 0
    private float timerSee;

    private IFOVDetection fovD;
    private IEnemyCatRotation rotation;
    private IEnemyCatMovement movement;


    public void ManualAwake()
    {
        fovD = this.GetComponent<FOVDetection>();
        movement = new EnemyCatMovement();
        movement.Initialize(gameObject.GetComponent<CharacterController>(), Vector3.zero, heightJump, speedMove);
        rotation = new EnemyCatRotation();
        rotation.Initialize(sensitivity, gameObject.GetComponent<Transform>());
        timerSee = 0;
    }
    public void ManualStart()
    {
        fovD.StartDetection();
    }
    public void ManualUpdate()
    {
        if (fovD.isTarget)
        {
            // posTarget is pointer of position from obj
            rotation.posTarget = fovD.posTarget;
            movement.UpdateMoveTo(Vector3.forward);
            timerSee = timerCatWillAttack; 
        }
        else if (timerSee > 0) // very not smart follow player from memory
        {
            timerSee -= Time.deltaTime;
            rotation.posTarget = Vector3.zero;
            movement.UpdateMoveTo(Vector3.forward); 
        }
        else
        {
            // here i can add logic for do nothing or if they remember where is it. to try find player
            // for now ai is very very silly
            // reset values
            rotation.posTarget = Vector3.zero;
            movement.UpdateMoveTo(Vector3.zero);
        } 
        rotation.Update();
        movement.Update();
    }
}
