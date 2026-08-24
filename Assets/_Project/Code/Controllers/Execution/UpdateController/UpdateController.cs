
using System;
using UnityEngine;


public class UpdateContorller : MonoBehaviour, IPauseUpdate
{
    private bool objIsPosed;

    private IUpdatable[] updatable;

    public void UpdateIsPaused(bool value)
    {
        objIsPosed = value;
    }

   private void Awake()
    {
        objIsPosed = false;
        updatable = this.GetComponentsInChildren<IUpdatable>();
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
        updatable = null;
    }



}
