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
                float distanceToTarget = Vector3.Distance(transform.position, targetTransfrom.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                { 
                    isTarget = true;
                    posTarget = targetTransfrom.position;
                } 
                else
                {
                    isTarget = false;
                    posTarget = Vector3.zero;
                }
            }
            else 
            {
                // if player too close. see it
                rangeCheck = Physics.OverlapSphere(transform.position, close_radius, targetMask); 
                if (rangeCheck.Length != 0)
                { 
                    isTarget = true;
                    posTarget = targetTransfrom.position;
                } 
                else
                {
                    isTarget = false;
                    posTarget = Vector3.zero;
                }
            }
        }
        else
        {
            isTarget = false;
            posTarget = Vector3.zero;
        }
    }
}
