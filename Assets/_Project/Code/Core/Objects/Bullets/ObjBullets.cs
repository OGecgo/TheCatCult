using System;
using System.Collections;
using UnityEngine;

public class ObjBullets : MonoBehaviour, IInteractableBullets, ILookedObj, IPauseFeatures
{
    private int outlineLayer;
    private int interactLayer;

    public bool featureIsPaused;
    public bool isInteracted;
 
    public event Action OnInteracted;

    public void Interact(IInteractBullet interactBullet)
    {
        if (featureIsPaused) return;
        if (isInteracted) return;

        interactBullet.GetBunchOfBullets();
        OnInteracted?.Invoke();
        isInteracted = true;
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
