
using UnityEngine;

public class EnemyCatAction : MonoBehaviour, IUpdatable
{
    [Header("Patrol settings")]
    [SerializeField] private Vector3[] patrolPositions;
    [SerializeField] private float timeRandomWalk = 2f;
    [SerializeField] private float timeThinking = 2f;
    [Header("If enemycat take damage")]
    [SerializeField] private float timeLookAround = 5f;

    private bool isLostPath;
    private float countTimeLookAround;

    private IUpdatableStateMachine<IUpdatable> patrolMachin;
    private IUpdatable followPlayer;
    private IUpdatable pathWalks;
    private IUpdatable randomWalks;
    private IUpdatable lookAround;
    
    private ILifeAction enemyCatLife; 

    private IFOVDetection fovD;
    private IEnemyCatRotation rotation;
    private IEnemyCatMovement movement;
    
    public void ManualUpdate()
    {
        if (fovD.isTarget)
        {
            patrolMachin.SetState(followPlayer);
            isLostPath = true;
        }
        else 
        {
            if (!isLostPath) patrolMachin.SetState(pathWalks);
            // if player targeted. enemy lost they path and start random walks
            else patrolMachin.SetState(randomWalks);
        }

        // if hit cat. they start look around
        if (countTimeLookAround > 0f && !fovD.isTarget)
        {
            patrolMachin.SetState(lookAround);
            countTimeLookAround -= Time.deltaTime;
        }
        patrolMachin.ManualUpdate();
    }

    private void OnEnable()
    {
        enemyCatLife.OnIsHit += IsAttacked;
    }

    private void OnDisable()
    {
        enemyCatLife.OnIsHit -= IsAttacked;
    }

    private void Awake()
    {
        fovD = this.GetComponent<IFOVDetection>();
        movement = this.GetComponent<IEnemyCatMovement>();
        rotation = this.GetComponent<IEnemyCatRotation>();
        enemyCatLife = this.GetComponent<ILifeAction>();

        isLostPath = false;
        countTimeLookAround = 0f;
    }    

    private void Start()
    {

        // initialization state machin
        followPlayer = new StateEnemyCatFollowPlayer(fovD, movement, rotation);
        pathWalks = new StateEnemyCatPathWalk(movement, rotation, patrolPositions, this.transform);
        randomWalks = new StateEnemyCatRandomWalk(rotation, movement, timeRandomWalk, timeThinking); // for now do noting
        lookAround = new StateEnemyCatLookAround(rotation, movement);

        patrolMachin = new UpdatableStateMachine<IUpdatable>();
        patrolMachin.SetState(pathWalks);

        // fov detection
        fovD.StartDetection();
    }

    private void IsAttacked()
    {
        countTimeLookAround = timeLookAround;   
    }

}
