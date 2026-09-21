using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseController : MonoBehaviour, IPauseController
{
    [Tooltip("Object will be local paused by default\n Also parent object should be child object fo PauseController")]
    [SerializeField] private GameObject parentObj;
    
    // bool is isPausedLocal
    private Dictionary<IPauseUpdate, bool> pauseUpdate;
    private Dictionary<IPauseFeatures, bool> pauseFeatures;

    public bool isPausedGlobal {get; private set;} 
    public void PauseGame()
    {
        isPausedGlobal = true;

        // pose the world
        foreach(IPauseUpdate update in pauseUpdate.Keys)
        {
            if ((UnityEngine.Object)update != null) 
            {
                update.UpdateIsPaused(true);
            }
        }

        foreach (IPauseFeatures feature in pauseFeatures.Keys)
        {
            if ((UnityEngine.Object)feature != null) 
            {
                feature.FeatureIsPaused(true);
            }
        }

    }

    // if local is paused. they dont upose the object
    public void UnpauseGame()
    {
        isPausedGlobal = false;

        // run the game
        foreach(KeyValuePair<IPauseUpdate, bool> update in pauseUpdate)
        {
            if ((UnityEngine.Object)update.Key != null && !update.Value)
            {
                update.Key.UpdateIsPaused(false);
            }
        }

        foreach (KeyValuePair<IPauseFeatures, bool> feature in pauseFeatures)
        {
            if ((UnityEngine.Object)feature.Key != null && !feature.Value)
            {
                feature.Key.FeatureIsPaused(false);
            }
        }

    }


    public void PauseObjFeature(IPauseFeatures obj)
    {
        if ((UnityEngine.Object)obj != null)
        {
            obj.FeatureIsPaused(true);
            pauseFeatures[obj] = true;
        }
    }

    public void UnpauseObjFeature(IPauseFeatures obj)
    {
        if ((UnityEngine.Object)obj != null)
        {
            if (!isPausedGlobal) obj.FeatureIsPaused(false);
            pauseFeatures[obj] = false;
        }
    }
    public void PauseObjUpdate(IPauseUpdate obj)
    {
        if ((UnityEngine.Object)obj != null)
        {
            obj.UpdateIsPaused(true);
            pauseUpdate[obj] = true;
        }
    }
    public void UnpauseObjUpdate(IPauseUpdate obj)
    {
        if ((UnityEngine.Object)obj != null)
        {
            if (!isPausedGlobal) obj.UpdateIsPaused(false);
            pauseUpdate[obj] = false;
        }
    }

    private void Awake()
    {
        pauseUpdate = new Dictionary<IPauseUpdate, bool>();
        pauseFeatures = new Dictionary<IPauseFeatures, bool>();
        
        // set default values
        foreach(IPauseUpdate p in GetComponentsInChildren<IPauseUpdate>())
        {
            pauseUpdate.Add(p, false);
        }
        foreach(IPauseFeatures p in GetComponentsInChildren<IPauseFeatures>())
        {
            pauseFeatures.Add(p, false);
        }
        // set default true values for parent object
        foreach(IPauseUpdate p in parentObj.GetComponentsInChildren<IPauseUpdate>())
        {
            PauseObjUpdate(p);
        }
        foreach(IPauseFeatures p in parentObj.GetComponentsInChildren<IPauseFeatures>())
        {
            PauseObjFeature(p);
        }
    }
}
