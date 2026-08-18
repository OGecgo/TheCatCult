using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCatPatrol : MonoBehaviour, IUpdatable
{
    private enum AvailableValues {bothDirections, oneDirection}

    [Header("General settings")]
    [SerializeField] private WalkConf walkConf;
    [SerializeField] private RunConf runConf;
    [SerializeField] private RotationConf rotationConf;  
    [Header("Patrol settings")]
    [SerializeField] private Vector3[] patrolPositions;
    [SerializeField] private float timeRandomWalk = 2f;
    [SerializeField] private float timeThinking = 2f;

    private bool isLostPath;

    private IEnemyCatStateMachinePatrol patrolMachin;
    private IUpdatable followPlayer;
    private IUpdatable pathWalks;
    private IUpdatable randomWalks;
    
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

        patrolMachin.ManualUpdate();
        rotation.ManualUpdate();
        movement.ManualUpdate();
    }

   private void Awake()
    {
        fovD = this.GetComponent<IFOVDetection>();
        movement = new EnemyCatMovement
        (
            gameObject.GetComponent<ICharacterGravity>(),
            new EnemyCatMovementConfRecord(walkConf, runConf)
        );
        rotation = new EnemyCatRotation(rotationConf, gameObject.GetComponent<Transform>());

        // initialization state machin
        followPlayer = new StateEnemyCatFollowPlayer(fovD, movement, rotation);
        pathWalks = new StateEnemyCatPathWalk(movement, rotation, patrolPositions, this.transform);
        randomWalks = new StateEnemyCatRandomWalk(rotation, movement, timeRandomWalk, timeThinking); // for now do noting

        patrolMachin = new EnemyCatStateMachinePatrol();
        patrolMachin.SetState(pathWalks);

        isLostPath = false;
    }    

   private void Start()
    {
        fovD.StartDetection();
    }

}
