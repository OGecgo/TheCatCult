using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteractor : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;
    [Header("General settings")]
    [SerializeField] private float range = 5f;


  
    private Camera playerCemare;
    private ILookedObj lastLookedObj;
    // interactions
    private IInteractBullet bulletControl;

    

    private void Awake()
    {
        playerCemare = GetComponentInChildren<Camera>();
        bulletControl = GetComponent<IInteractBullet>();
    }

    private void Update()
    {
        // execute interaction
        if (Physics.Raycast(transform.position, playerCemare.transform.forward, out RaycastHit hit, range))
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
            if (hit.collider.TryGetComponent(out IInteractableObjBullets interact))
            {
                interact.Interact(bulletControl) ;
            }
        }
        // if from looking ojb to nothing
        IsNotLooking();
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
