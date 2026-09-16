using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ObjHealingPack : MonoBehaviour, IInteractableHealingPack, ILookedObj, IPauseFeatures
{

    private int outlineLayer;
    private int interactLayer;

    private bool featureIsPaused;
    private bool isInteracted;

    public event Action OnInteracted;

    public void Interact(IInteractHealingPack interactHealth)
    {
        if (featureIsPaused) return;
        if (isInteracted) return;

        interactHealth.GetHealingPack();
        isInteracted = true;
        OnInteracted?.Invoke();
        StartCoroutine(WaitCoroutine());
    }

    public void IsLooked()
    {
        this.gameObject.layer = outlineLayer;
    }
    public void IsNotLooked()
    {
        this.gameObject.layer = interactLayer;
    }


    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;   
    }

    private void Awake()
    {
        outlineLayer = LayerMask.NameToLayer("OutlineLayer");
        interactLayer = LayerMask.NameToLayer("InteractLayer"); 
        featureIsPaused = false;
        isInteracted = false;
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
