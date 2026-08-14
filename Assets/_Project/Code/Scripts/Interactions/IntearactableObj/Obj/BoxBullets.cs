using System;
using UnityEngine;

public class BoxBullets : MonoBehaviour, IInteractableObjBullets, ILookedObj
{
    private int outlineLayer;
    private int defaultLayer;
 
    public void Interact(IInteractBullet interactBullet)
    {
        interactBullet.GetBunchOfBullets();
        Destroy(this.gameObject);
    }

    public void IsLooked()
    {
        this.gameObject.layer = outlineLayer;
    }
    public void IsNotLooked()
    {
        this.gameObject.layer = defaultLayer;
    }

    private void Awake()
    {
        outlineLayer = LayerMask.NameToLayer("OutlineLayer");
        defaultLayer = LayerMask.NameToLayer("InteractLayer"); 
    }

}
