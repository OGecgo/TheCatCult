using UnityEngine;

public class PlayerHealthSound : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private string playerHit;
    [SerializeField] private string playerHeal;


    private IAudioController audioController;
    private ILifeAction playerLife;
    private void Awake()
    {
        playerLife = this.GetComponent<ILifeAction>();
        audioController = _audioController.GetComponent<IAudioController>();
    }

    private void OnEnable()
    {
        playerLife.OnIsHeal += OnIsHealSound;
        playerLife.OnIsHit += OnIsHitSound;
    }

    private void OnDisable()
    {
        playerLife.OnIsHeal -= OnIsHealSound;
        playerLife.OnIsHit -= OnIsHitSound;
    }

    private void OnIsHealSound()
    {
        audioController.PlayOneShotSFX(playerHeal);
    }

    private void OnIsHitSound()
    {
        audioController.PlayOneShotSFX(playerHit);
    }
}
