using UnityEngine;

public class ObjBullets : MonoBehaviour, IInteractableBullets, ILookedObj, IPauseFeatures
{
    private int outlineLayer;
    private int interactLayer;

    public bool featureIsPaused;
 
    public void Interact(IInteractBullet interactBullet)
    {
        if (featureIsPaused) return;

        interactBullet.GetBunchOfBullets();
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
