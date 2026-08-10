using UnityEngine;
using System.Collections;

// find only the first object

public class FOVDetection: MonoBehaviour, IFOVDetection
{

    [SerializeField] public FOVDetectionConf fovConf;


    private bool _isTarget; 
    private Vector3 _posTarget;
    private Collider[] rangeCheck;


    public bool isTarget { get { return _isTarget; } set { _isTarget = value; } }
    public Vector3 posTarget { get { return _posTarget;  }  set { _posTarget = value; } }


    public void Start()
    {
        this.isTarget = false;
        this.posTarget = Vector3.zero;
        rangeCheck = new Collider[1];
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
        // work only for first detection.
        int rangeCount = Physics.OverlapSphereNonAlloc(transform.position, fovConf.radius, rangeCheck, fovConf.targetMask);
        if (rangeCount != 0)
        {
            Transform targetTransfrom = rangeCheck[0].transform;
            Vector3 directionToTarget = (targetTransfrom.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < fovConf.angle / 2)
            {
                checkObjectsBetween(targetTransfrom.position, directionToTarget);
            }
            else 
            {
                // if player too close. see it
                rangeCount = Physics.OverlapSphereNonAlloc(transform.position, fovConf.closeRadius, rangeCheck, fovConf.targetMask); 
                if (rangeCount != 0)
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
        if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, fovConf.obstructionMask))
        { 
            checkTrue(positionTarget);
        }
        else
        {
            checkFalse();
        }
    }

}


