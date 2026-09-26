using UnityEngine;

public class EnemyCatActionAnimation : MonoBehaviour, IPauseFeatures
{
    [SerializeField] private MonoBehaviour _enemyCatAction;
    [SerializeField] private Animator animator;
    [SerializeField] private float crossfade = 0.2f;

    [Header("State Names in Animator")]
    [SerializeField] private string walk;
    [SerializeField] private string lookAround;
    [SerializeField] private string followPlayer;
    [SerializeField] private string none; 


    private IEnemyCatAction enemyCatAction;
    private string currentAnimation;

    public void FeatureIsPaused(bool value)
    {
        animator.speed = value ? 0f : 1f;
    }

    private void Awake()
    {
        enemyCatAction = _enemyCatAction.GetComponent<IEnemyCatAction>();
        currentAnimation = "";
    }

    private void OnEnable()
    {
        enemyCatAction.OnAction += OnActionAnimation;
    }
    private void OnDisable()
    {
        enemyCatAction.OnAction -= OnActionAnimation;
    }

    private void ChangeAnimation(string nameAnimation)
    {
        if (currentAnimation != nameAnimation)
        {
            currentAnimation = nameAnimation;
            animator.CrossFade(nameAnimation, crossfade);
        } 
    }

    private void OnActionAnimation(IEnemyCatAction.ActionType type)
    {
        switch (type)
        {
            case IEnemyCatAction.ActionType.FOLLOW_PLAYER:
                ChangeAnimation(followPlayer);
                break;
            case IEnemyCatAction.ActionType.PATH_WALKS:
                ChangeAnimation(walk);
                break;
            case IEnemyCatAction.ActionType.RANDOM_WALKS:
                ChangeAnimation(walk);
                break;
            case IEnemyCatAction.ActionType.LOOK_AROUND:
                ChangeAnimation(lookAround);
                break;
            case IEnemyCatAction.ActionType.NONE:
                ChangeAnimation(none);
                break;
        }
    }
}
