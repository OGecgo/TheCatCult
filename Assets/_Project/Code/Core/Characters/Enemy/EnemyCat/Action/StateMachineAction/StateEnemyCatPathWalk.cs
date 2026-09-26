using System;
using System.IO;
using System.Net.Http.Headers;
using UnityEngine;

public class StateEnemyCatPathWalk: IStateEnemyCatAction
{

    private IEnemyCatMovement movement;
    private IEnemyCatRotation rotation;
    private Vector3[] pathPoints;
    private Transform positionEnemyCat;

    private int posPathPoints;
    private int directionMove;

    public StateEnemyCatPathWalk(
        IEnemyCatMovement movement, 
        IEnemyCatRotation rotation, 
        Vector3[] pathPoints, 
        Transform positionEnemyCat
    )
    {
        this.movement = movement;
        this.rotation = rotation;
        this.pathPoints = pathPoints;
        this.positionEnemyCat = positionEnemyCat;

        posPathPoints = 0;
        directionMove = 1;
    }

    public void Enter()
    {
        posPathPoints = 0;
        directionMove = 1;
    }


    public void ManualUpdate()
    {
        if (pathPoints.Length == 0) return;
        rotation.posTarget = pathPoints[posPathPoints];
        movement.typeMovement = IEnemyCatMovement.TypeMovement.WALK;

        if (Vector3.Distance(rotation.posTarget, positionEnemyCat.position) < 1f)
        {
            posPathPoints += directionMove;
            if (posPathPoints >= pathPoints.Length || posPathPoints < 0)
            {
                directionMove = -directionMove;
                posPathPoints += directionMove; // go back (dot go out of memory)
            }
        }
    }

}
