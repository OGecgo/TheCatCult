using System;
using Unity.VisualScripting;
using UnityEngine;


// for now nothing
public class StateEnemyCatRandomWalk: IUpdatable
{
    private float timeDistance;
    // think == wait
    private float timeThink;
    private float countTimeDistance;
    private float countTimeThinnk;
    private IEnemyCatRotation rotation;   
    private IEnemyCatMovement movement;
    private System.Random rand;

    public StateEnemyCatRandomWalk(
        IEnemyCatRotation rotation, 
        IEnemyCatMovement movement, 
        float timeDistance, 
        float timeThink
        )
    {
        this.rotation = rotation;
        this.movement = movement;
        this.timeDistance = timeDistance;
        this.timeThink = timeThink;
        rand = new System.Random();
        countTimeDistance = timeDistance;
        countTimeThinnk = timeThink;
    }

    public void ManualUpdate()
    {
        // step 1: think
        if (countTimeThinnk > 0f)
        {
            countTimeThinnk -= Time.deltaTime;
        }
        // step 2: move
        else if (countTimeDistance > 0f)
        {
            countTimeDistance -= Time.deltaTime;
            movement.typeMovement = IEnemyCatMovement.TypeMovement.WALK;
        }
        // step 3: choice new direction
        else
        {
            rotation.angle = rand.Next(361);
            countTimeDistance = timeDistance;
            countTimeThinnk = timeThink;
        }
    }
}
