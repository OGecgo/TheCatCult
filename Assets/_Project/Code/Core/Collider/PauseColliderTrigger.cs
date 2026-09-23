using System;
using UnityEngine;
using UnityEngine.AI;

public class PauseColliderControl: MonoBehaviour
{
    [SerializeField] private MonoBehaviour _pauseController;
    [SerializeField] private LayerMask playerLayers;


    private IPauseController pauseController;
    // that values is not movable
    private Vector3 boxCenter;
    private Vector3 boxHalfExtents;
    private Quaternion boxRotation;

    private void Awake()
    {
        pauseController = _pauseController.GetComponent<IPauseController>();

        BoxCollider box = GetComponent<BoxCollider>();
        boxCenter = box.transform.TransformPoint(box.center);
        boxHalfExtents = Vector3.Scale(box.size, box.transform.lossyScale) * 0.5f;
        boxRotation = box.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayers) != 0)
        {
            ActionObjs(pauseController.UnpauseObjFeature, pauseController.UnpauseObjUpdate);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayers) != 0)
        {
            ActionObjs(pauseController.PauseObjFeature, pauseController.PauseObjUpdate);
        }
    }

    private void ActionObjs(Action<IPauseFeatures> ObjFeature, Action<IPauseUpdate> ObjUpdate)
    {
        Collider[] hitCollider = Physics.OverlapBox(boxCenter, boxHalfExtents, boxRotation);

        for (int i = 0; i < hitCollider.Length; i++)
        {
            Collider hit = hitCollider[i];
            // if collider deleted
            if ((UnityEngine.Object)hit == null) continue;
            // dont pause player
            if (((1 << hit.gameObject.layer) & playerLayers) != 0) continue;

            IPauseFeatures[] features = hit.GetComponentsInChildren<IPauseFeatures>();
            IPauseUpdate[] updates = hit.GetComponentsInChildren<IPauseUpdate>();
            foreach (IPauseFeatures p in features)
            {
                ObjFeature.Invoke(p);
            }
            foreach (IPauseUpdate p in updates)
            {
                ObjUpdate.Invoke(p);
            }            
        }
    }



    
}
