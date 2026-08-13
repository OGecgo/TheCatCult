
using System;
using UnityEngine;


public class UpdateManager : MonoBehaviour, IPauseUpdate
{
    private bool objIsPosed;

    private IUpdatable[] updatable;
    private IPauseFeatures[] features;

    public void ObjIsPaused(bool value)
    {
        objIsPosed = value;
        foreach(IPauseFeatures f in features)
        {
            if ((UnityEngine.Object)f != null) f.FeatureIsPaused(value);
        }
    }

   private void Awake()
    {
        objIsPosed = false;
        updatable = this.GetComponentsInChildren<IUpdatable>();
        features = this.GetComponentsInChildren<IPauseFeatures>();
    }

    private void Update()
    {
        if (objIsPosed) return;

        foreach (IUpdatable u in updatable)
        {
            if ((UnityEngine.Object)u != null) u.ManualUpdate();
        }
    }

    private void OnDestroy()
    {
        updatable     = null;
        features = null;
    }



}
