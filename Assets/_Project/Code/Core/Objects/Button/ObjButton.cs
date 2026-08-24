using LineworkLite.Editor.FreeOutline;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjButton : MonoBehaviour, IInteractableButton, ILookedObj, IPauseFeatures
{
    [SerializeField] private MonoBehaviour _eventObj;
    [SerializeField] private MeshRenderer meshRenderer_On;
    [SerializeField] private MeshRenderer meshRenderer_Off;

    private int outlineLayer;
    private int noOutlineLayer;
    private bool isOn;
    private bool featureIsPaused;

    private IEvent eventObj;

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }
    
    public void IsNotLooked()
    {
        meshRenderer_Off.gameObject.layer = noOutlineLayer;
    }

    public void IsLooked()
    {
        // show interaction only if off
        if (meshRenderer_Off.enabled)
        {
            meshRenderer_Off.gameObject.layer = outlineLayer;
        }
    }

    public void Interact()
    {
        if (featureIsPaused) return;
        
        if (!isOn)
        {
            eventObj.OnTriggerEvent();
            meshRenderer_Off.enabled = false;
            meshRenderer_On.enabled = true;
            isOn = true;
        }
    }

    private void Awake()
    {
        outlineLayer = LayerMask.NameToLayer("OutlineLayer");
        noOutlineLayer = LayerMask.NameToLayer("NoOutlineLayer"); 
        eventObj = _eventObj.GetComponent<IEvent>();
        meshRenderer_Off.enabled = true;
        meshRenderer_On.enabled = false;
        isOn = false;
    }
}
