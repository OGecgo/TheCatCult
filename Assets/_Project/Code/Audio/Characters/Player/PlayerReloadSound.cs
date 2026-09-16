using UnityEngine;

public class PlayerReloadSound : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private string reloadSoundName;


    private IAudioController audioController;
    private IBulletsAction bulletsAction;

    private void Awake()
    {
        audioController = _audioController.GetComponent<IAudioController>();
        bulletsAction = this.GetComponent<IBulletsAction>();
    }

    private void OnEnable()
    {
        bulletsAction.OnReloadBullets += OnReloadBulletsSound;
    }

    private void OnDisable()
    {
        bulletsAction.OnReloadBullets -= OnReloadBulletsSound;
    }

    private void OnReloadBulletsSound()
    {
        audioController.PlayOneShotSFX(reloadSoundName);
    }
}
