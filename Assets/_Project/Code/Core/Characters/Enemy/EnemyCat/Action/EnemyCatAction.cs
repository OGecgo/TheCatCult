
using System;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class EnemyCatAction : MonoBehaviour, IEnemyCatAction, IUpdatable, IPauseUpdate
{
    [Header("Patrol settings")]
    [SerializeField] public Vector3[] patrolPositions;
    [SerializeField] private float timeRandomWalk = 2f;
    [SerializeField] private float timeThinking = 2f;
    [Header("If enemycat take damage")]
    [SerializeField] private float timeLookAround = 5f;

    private bool isLostPath;
    private float countTimeLookAround;

    private IUpdatableStateMachine<IStateEnemyCatAction> patrolMachin;
    private IStateEnemyCatAction followPlayer;
    private IStateEnemyCatAction pathWalks;
    private IStateEnemyCatAction randomWalks;
    private IStateEnemyCatAction lookAround;
    
    private ILifeAction enemyCatLife; 

    private IFOVDetection fovD;
    private IEnemyCatRotation rotation;
    private IEnemyCatMovement movement;
    private bool updateIsPaused;

    
    public event Action<IEnemyCatAction.ActionType> OnAction;


    public void UpdateIsPaused(bool value)
    {
        updateIsPaused = value;
        OnAction?.Invoke(IEnemyCatAction.ActionType.NONE);
    }

    public void ManualUpdate()
    {
        if (updateIsPaused) return;

        IEnemyCatAction.ActionType type;
        if (fovD.isTarget)
        {
            type = IEnemyCatAction.ActionType.FOLLOW_PLAYER;
            isLostPath = true;
        }
        else 
        {
            if (!isLostPath)
            {
                if (patrolPositions.Length == 0) type = IEnemyCatAction.ActionType.NONE;
                else type = IEnemyCatAction.ActionType.PATH_WALKS;
            } 
            // if player targeted. enemy lost they path and start random walks
            else
            {
                type = IEnemyCatAction.ActionType.RANDOM_WALKS;
            } 
        }

        // if hit cat. they start look around
        if (countTimeLookAround > 0f && !fovD.isTarget)
        {
            type = IEnemyCatAction.ActionType.LOOK_AROUND;
            countTimeLookAround -= Time.deltaTime;
        }

        SetStateValue(type);

        OnAction?.Invoke(type);
        patrolMachin.ManualUpdate();
    }

    private void SetStateValue(IEnemyCatAction.ActionType type)
    {
        switch (type)
        {
            case IEnemyCatAction.ActionType.FOLLOW_PLAYER:
                patrolMachin.SetState(followPlayer);
                break;
            case IEnemyCatAction.ActionType.PATH_WALKS:
                patrolMachin.SetState(pathWalks);
                break;
            case IEnemyCatAction.ActionType.RANDOM_WALKS:
                patrolMachin.SetState(randomWalks);
                break;
            case IEnemyCatAction.ActionType.LOOK_AROUND:
                patrolMachin.SetState(lookAround);
                break;
        }
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
        updateIsPaused = false;
    }    



    private void Start()
    {

        // initialization state machin
        followPlayer = new StateEnemyCatFollowPlayer(fovD, movement, rotation);
        pathWalks = new StateEnemyCatPathWalk(movement, rotation, patrolPositions, this.transform);
        randomWalks = new StateEnemyCatRandomWalk(rotation, movement, timeRandomWalk, timeThinking); // for now do noting
        lookAround = new StateEnemyCatLookAround(rotation, movement);

        patrolMachin = new UpdatableStateMachine<IStateEnemyCatAction>();
        patrolMachin.SetState(pathWalks);

        // fov detection
        fovD.StartDetection();
    }

    private void IsAttacked()
    {
        countTimeLookAround = timeLookAround;   
    }

}
