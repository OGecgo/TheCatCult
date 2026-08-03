using UnityEngine;
using System.Collections;


public class FOVDetection: MonoBehaviour, IFOVDetection
{


    [Header("FOV Config")]
    public float radius = 10f;
    public float close_radius = 3f;
    [Range(0, 360)]
    public float angle = 70f;
    public LayerMask targetMask;
    public LayerMask obstructionMask;


    private bool _isTarget; 
    private Vector3 _posTarget;
    // private int lengthReadTargets; if i want to recognize more objects than one. Now not work

    public bool isTarget { get { return _isTarget; } set { _isTarget = value; } }
    public Vector3 posTarget { get { return _posTarget;  }  set { _posTarget = value; } }


    public void Start()
    {
        this.isTarget = false;
        this.posTarget = Vector3.zero;
    }


    // updated after 0.2 seconds
    public void StartDetection()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait; // wait for 0.2 sec (example)
            FOVCheck();
        }
    }

    public void FOVCheck()
    {
        Collider[] rangeCheck;
        // work only for first detection.
        rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeCheck.Length != 0)
        {
            Transform targetTransfrom = rangeCheck[0].transform;
            Vector3 directionToTarget = (targetTransfrom.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                checkObjectsBetween(targetTransfrom.position, directionToTarget);
            }
            else 
            {
                // if player too close. see it
                rangeCheck = Physics.OverlapSphere(transform.position, close_radius, targetMask); 
                if (rangeCheck.Length != 0)
                { 
                    checkObjectsBetween(targetTransfrom.position, directionToTarget);
                } 
                else
                {
                    checkFalse();
                }
            }
        }
        else
        {
            checkFalse();
        }
    }

    private void checkFalse()
    {
        isTarget = false;
        posTarget = Vector3.zero;
    } 

    private void checkTrue(Vector3 position)
    {
        isTarget = true;
        posTarget = position;
    }

    private void checkObjectsBetween(Vector3 positionTarget, Vector3 directionToTarget)
    {
        float distanceToTarget = Vector3.Distance(transform.position, positionTarget);
        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
        { 
            checkTrue(positionTarget);
        }
        else
        {
            checkFalse();
        }
    }

}


