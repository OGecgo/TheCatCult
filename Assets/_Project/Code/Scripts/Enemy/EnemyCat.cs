using UnityEngine;

public class EnemyCat : MonoBehaviour, IEnemyCat
{

    [Header("Enemy Cat Config Movement")]
    [SerializeField] WalkConf walkConf;
    [SerializeField] RunConf runConf;
    public float sensitivity = 4f;
    public float timerCatWillAttack = 0f;
    // enemy go to player untile timer goes 0
    private float timerSee;

    private IFOVDetection fovD;
    private IEnemyCatRotation rotation;
    private IEnemyCatMovement movement;


    public void ManualAwake()
    {
        fovD = this.GetComponent<FOVDetection>();
        movement = new EnemyCatMovement
        (
            gameObject.GetComponent<CharacterController>(),
            new EnemyCatConfRecord(walkConf, runConf)
        );
        rotation = new EnemyCatRotation(sensitivity, gameObject.GetComponent<Transform>());
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
            movement.typeMovement = TypeMovement.RUN;
            timerSee = timerCatWillAttack; 
        }
        else if (timerSee > 0) // very not smart follow player from memory
        {
            timerSee -= Time.deltaTime;
            rotation.posTarget = Vector3.zero;
            movement.typeMovement = TypeMovement.RUN;
        }
        else
        {
            // here i can add logic for do nothing or if they remember where is it. to try find player
            // for now ai is very very silly
            // reset values
            rotation.posTarget = Vector3.zero;
            movement.typeMovement = TypeMovement.WHAIT;
        } 
        rotation.ManualUpdate();
        movement.ManualUpdate();
    }
}
