using System.Collections;
using UnityEngine;


// readtion to take datamage
public class PlayerLifeReaction : MonoBehaviour
{
    [Header("Player take damage settigs")]
    [SerializeField] private float pushUp = 3f;
    [SerializeField] private float pushBack = 5f;
    [SerializeField] private float timerInvincible = 1f;

    private ICharacterGravity characterGravity;
    private int playerInvisibleLayer;
    private int PlayerLayer;
    private Coroutine coroutine;
    private ILifeAction playerAttacked;

    private void OnEnable()
    {
        playerAttacked.OnIsAttacked += UpdateReactionAttacked;
    }
    private void OnDisable()
    {
        playerAttacked.OnIsAttacked -= UpdateReactionAttacked;
    }

    private void Awake()
    {
        playerAttacked = this.GetComponent<ILifeAction>();
        characterGravity = this.GetComponent<ICharacterGravity>();
        playerInvisibleLayer = LayerMask.NameToLayer("PlayerInvisibleLayer");
        PlayerLayer = LayerMask.NameToLayer("PlayerLayer");
    }

    private void UpdateReactionAttacked()
    {
        // push back
        characterGravity.PushUp(pushUp);
        characterGravity.MoveForward(new Vector3(0, 0, -pushBack));
        // ivisibility
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(CoroutineInvisiblePlayer());
    }

    private IEnumerator CoroutineInvisiblePlayer()
    {
        this.gameObject.layer = playerInvisibleLayer;
        yield return new WaitForSeconds(timerInvincible);
        this.gameObject.layer = PlayerLayer;

    }
    
}
