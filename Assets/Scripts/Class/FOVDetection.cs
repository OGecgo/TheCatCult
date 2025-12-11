using UnityEngine;
using System.Collections;


public class FOVDetection: MonoBehaviour, IFOVDetection
{
    private float _radius;
    [Range(0, 360)]
    private float _angle;
    private bool _isTarget;
    private Vector3 _posTarget;
    private LayerMask targetMask;
    private LayerMask obstructionMask;
    // private int lengthReadTargets; if i want to recognize more objects than one

    public float radius { get { return _radius; } set { _radius = value; } }
    public float angle { get { return _angle; } set { _angle = value; } }
    public bool isTarget { get { return _isTarget; } set { _isTarget = value; } }
    public Vector3 posTarget { get { return _posTarget;  }  set { _posTarget = value; } }





    public void Initialize(LayerMask targetMask, LayerMask obstructionMask, Transform transform, float radius, float angle)
    {
        this.targetMask = targetMask;
        this.obstructionMask = obstructionMask;
        this.transform.position = transform.position;
        this.isTarget = false;
        this.posTarget = Vector3.zero;
        this.radius = radius;
        this.angle = angle;

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
            yield return wait;
            FOVCheck();
        }
    }

    public void FOVCheck()
    {
        // work only for first detection.
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);
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
                isTarget = false;
                posTarget = Vector3.zero;
            }
        }
        else
        {
            isTarget = false;
            posTarget = Vector3.zero;
        }
    }
}
