using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteractor : MonoBehaviour, IUpdatable, IPauseUpdate
{
    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;
    [Header("General settings")]
    [SerializeField] private float _range = 5f;
  
    private Camera playerCemare;
    private ILookedObj lastLookedObj;
    // interactions
    private IInteractBullet bulletControl;
    private IInteractHealingPack healthControl;

    private bool updateIsPaused;
    
    public float range {get{return _range;}}

    public void UpdateIsPaused(bool value)
    {
        updateIsPaused = value;
    }

    public void ManualUpdate()
    {
        if (updateIsPaused) return;
        // execute interaction
        if (Physics.Raycast(playerCemare.transform.position, playerCemare.transform.forward, out RaycastHit hit, range))
        {
            if (hit.collider.TryGetComponent(out ILookedObj lookedObj))
            {
                // if from lookedObj to other lookedObj
                IsNotLooking();
                lookedObj.IsLooked();
                lastLookedObj = lookedObj;
            }
            else
            {
                // if from lookedObj to not looking obj
                IsNotLooking();
            }

            
            if (!interactAction.action.triggered) return;

            if (hit.collider.TryGetComponent(out IInteractableBullets interactBullet))
            {
                interactBullet.Interact(bulletControl) ;
            }
            else if (hit.collider.TryGetComponent(out IInteractableHealingPack interactHealth))
            {
                interactHealth.Interact(healthControl);
            }
            else if (hit.collider.TryGetComponent(out IInteractableDoor interactableDoor))
            {
                interactableDoor.Interact();
            }
            else if (hit.collider.TryGetComponent(out IInteractableButton interactableButton))
            {
                interactableButton.Interact();
            }
            else if (hit.collider.TryGetComponent(out IInteractableEndCollum interactableEndCollum))
            {
                interactableEndCollum.Interact();
            }
        }
        // if from looking ojb to nothing
        IsNotLooking();
    }
    
    private void Awake()
    {
        playerCemare = GetComponentInChildren<Camera>();
        bulletControl = GetComponent<IInteractBullet>();
        healthControl = GetComponent<IInteractHealingPack>();

    }

    private void IsNotLooking()
    {
        if((UnityEngine.Object)lastLookedObj != null)
        {
            lastLookedObj.IsNotLooked();
            lastLookedObj = null;
        }
    }

}
