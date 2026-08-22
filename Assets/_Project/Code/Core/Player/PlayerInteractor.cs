using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteractor : MonoBehaviour, IUpdatable
{
    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;
    [Header("General settings")]
    [SerializeField] private float _range = 5f;


  
    private Camera playerCemare;
    private ILookedObj lastLookedObj;
    // interactions
    private IInteractBullet bulletControl;
    private IInteractHealth healthControl;
    
    public float range {get{return _range;}}

    public void ManualUpdate()
    {
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

            if (hit.collider.TryGetComponent(out IInteractableObjBullets interactBullet))
            {
                interactBullet.Interact(bulletControl) ;
            }
            else if (hit.collider.TryGetComponent(out IInteractableObjHeal interactHealth))
            {
                interactHealth.Interact(healthControl);
            }
            else if (hit.collider.TryGetComponent(out IInteractableObjDoor interactableDoor))
            {
                interactableDoor.Interact();
            }
            else if (hit.collider.TryGetComponent(out IInteractableObjButton interactableButton))
            {
                interactableButton.Interact();
            }
            else if (hit.collider.TryGetComponent(out IInteractableObjEndCollum interactableEndCollum))
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
        healthControl = GetComponent<IInteractHealth>();

    }

    private void IsNotLooking()
    {
        if(lastLookedObj != null)
        {
            lastLookedObj.IsNotLooked();
            lastLookedObj = null;
        }
    }

}
