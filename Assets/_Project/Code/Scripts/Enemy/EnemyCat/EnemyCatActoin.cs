using UnityEngine;

public class EnemyCatActoin : MonoBehaviour, IUpdatable, IStartable, IAwakable
{
    [Header("General settings")]
    [SerializeField] private WalkConf walkConf;
    [SerializeField] private RunConf runConf;
    [SerializeField] private RotationConf rotationConf;  

    // for now that
    private float timerAttack = 0f;
    private float timer;
    
    private IFOVDetection fovD;
    private IEnemyCatRotation rotation;
    private IEnemyCatMovement movement;
    
    public void ManualAwake()
    {
        fovD = this.GetComponent<IFOVDetection>();
        movement = new EnemyCatMovement
        (
            gameObject.GetComponent<ICharacterGravity>(),
            new EnemyCatConfRecord(walkConf, runConf)
        );
        rotation = new EnemyCatRotation(rotationConf, gameObject.GetComponent<Transform>());
        timer = 0f;
    }    

    public void ManualStart()
    {
        fovD.StartDetection();
    }

    public void ManualUpdate()
    {
        AttackUpdate();
    }
 
    private void AttackUpdate()
    { 
        if (fovD.isTarget)
        {
            // posTarget is pointer of position from obj
            rotation.posTarget = fovD.posTarget;
            movement.typeMovement = TypeMovement.RUN;
            timer = timerAttack; 
        }
        else if (timer > 0) // very not smart follow player from memory
        {
            timer -= Time.deltaTime;
            rotation.posTarget = Vector3.zero;
            movement.typeMovement = TypeMovement.WALK;
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
