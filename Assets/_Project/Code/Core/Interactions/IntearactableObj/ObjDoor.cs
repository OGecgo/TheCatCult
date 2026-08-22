using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class ObjDoor : MonoBehaviour, IInteractableObjDoor, ILookedObj, IPauseFeatures
{
    private enum allowValues {negative = -1, positive = 1}

    [SerializeField] private float speedOpen = 3f;
    [SerializeField] private allowValues directionOpen = allowValues.negative;
    
    private int outlineLayer;
    private int noOutlineLayer;

    private bool isOpen;
    private bool featureIsPaused;
    private Quaternion closeRotation;
    private Quaternion openRotation;
    private Coroutine coroutine;

    public void IsNotLooked()
    {
        this.gameObject.layer = noOutlineLayer;
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
        noOutlineLayer = LayerMask.NameToLayer("NoOutlineLayer"); 
        isOpen = false;
        featureIsPaused = false;
        closeRotation = transform.localRotation;
        openRotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0f, 0f, (int)directionOpen * 90f));
    }

    private IEnumerator CorutineMoveDoor()
    {
        Quaternion targetRotation = isOpen ? closeRotation : openRotation;
        isOpen = !isOpen;
        while (Quaternion.Angle(this.transform.localRotation, targetRotation) > 0.01f)
        {
            // stop every frame they paused
            if (featureIsPaused) 
            {
                yield return null;
                continue;
            }
            this.transform.localRotation = Quaternion.Lerp(this.transform.localRotation, targetRotation, Time.deltaTime * speedOpen);

            // wait for next frame
            yield return null;
        }
        this.transform.localRotation = targetRotation;
    }
}
