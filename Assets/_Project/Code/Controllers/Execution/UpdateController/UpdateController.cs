
using System;
using UnityEngine;


public class UpdateContorller : MonoBehaviour
{

    private IUpdatable[] updatable;

   private void Awake()
    {
        updatable = this.GetComponentsInChildren<IUpdatable>();
    }

    private void Update()
    {

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
