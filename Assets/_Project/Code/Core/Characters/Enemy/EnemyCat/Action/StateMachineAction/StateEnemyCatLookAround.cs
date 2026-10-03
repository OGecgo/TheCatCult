using System;
using UnityEngine;

public class StateEnemyCatLookAround : IStateEnemyCatAction
{
    private float timeUpdateRotation;
    private float countTimeUpdateRotation;
    private int directoinsCount;
    private float directionsAngle; 
    private System.Random rand;
    private IEnemyCatRotation rotation;
    private IEnemyCatMovement movement;
    public StateEnemyCatLookAround(IEnemyCatRotation rotation, IEnemyCatMovement movement)
    {
        this.rotation = rotation;
        this.movement = movement;
        timeUpdateRotation = 0.5f; // every 1 sec enemy update look
        countTimeUpdateRotation = 0f;
        directoinsCount = 5; 
        directionsAngle = 360f / directoinsCount;
        rand = new System.Random();
    }

    public void Enter()
    {
        countTimeUpdateRotation = 0f;
    }

    public void ManualUpdate()
    {
        if (countTimeUpdateRotation <= 0f)
        {
            countTimeUpdateRotation = timeUpdateRotation;
            rotation.angle = rand.Next(directoinsCount) * directionsAngle;
        }
        movement.typeMovement = IEnemyCatMovement.TypeMovement.WHAIT;
        countTimeUpdateRotation -= Time.deltaTime;
    }
}
