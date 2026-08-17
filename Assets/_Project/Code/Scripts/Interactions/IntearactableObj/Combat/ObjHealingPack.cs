using UnityEngine;

public class ObjHealingPack : MonoBehaviour, IInteractableObjHeal, ILookedObj, IPauseFeatures
{

    private int outlineLayer;
    private int interactLayer;

    private bool featureIsPaused;
    public void Interact(IInteractHealth interactHealth)
    {
        if (featureIsPaused) return;

        interactHealth.GetHealingPack();
        Destroy(this.gameObject);
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
    }

}
