using UnityEngine;

public class PlayerShootSound : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private string shootSoundName;


    private IAudioController audioController;
    private IAttackAction attackAction;

    private void Awake()
    {
        audioController = _audioController.GetComponent<IAudioController>();
        attackAction = this.GetComponent<IAttackAction>();
    }


    private void OnEnable()
    {
        attackAction.OnAttack += OnAttackSound;
    }

    private void OnDisable()
    {
        attackAction.OnAttack -= OnAttackSound;
    }

    private void OnAttackSound()
    {
        audioController.PlayOneShotSFX(shootSoundName);
    }
}
