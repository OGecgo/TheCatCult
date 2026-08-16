using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class ObjDoor : MonoBehaviour, IInteractableObjDoor, ILookedObj, IPauseFeatures
{
    [SerializeField] private float speedOpen = 1;

    
    private int outlineLayer;
    private int defaultLayer;

    private bool isOpen;
    private bool featureIsPaused;
    private Quaternion closeRotation;
    private Quaternion openRotation;
    private Coroutine coroutine;

    public void IsNotLooked()
    {
        this.gameObject.layer = defaultLayer;
    }

    public void IsLooked()
    {
        this.gameObject.layer = outlineLayer;
    }

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }

    public void Interact()
    {        
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(CorutineMoveDoor());
    }

    private void Awake()
    {
        outlineLayer = LayerMask.NameToLayer("OutlineLayer");
        defaultLayer = LayerMask.NameToLayer("Default"); 
        isOpen = false;
        featureIsPaused = false;
        closeRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, -90f, 0f));
    }

    private IEnumerator CorutineMoveDoor()
    {
        Quaternion targetRotation = isOpen ? closeRotation : openRotation;
        isOpen = !isOpen;
        while (Quaternion.Angle(this.transform.rotation, targetRotation) > 0.01f)
        {
            // stop every frame they paused
            if (featureIsPaused) 
            {
                yield return null;
                continue;
            }
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * speedOpen);

            // wait for next frame
            yield return null;
        }
        this.transform.rotation = targetRotation;
    }
}
