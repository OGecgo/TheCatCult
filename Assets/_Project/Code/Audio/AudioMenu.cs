using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private MonoBehaviour _gameController;
    [SerializeField] private string deathClipName;
    [SerializeField] private string menuSourceName;
    [SerializeField] private string menuClipName;

    private IGameController gameController;
    private IAudioController audioController;


    private void Awake()
    {
        gameController = _gameController.GetComponent<IGameController>();
        audioController = _audioController.GetComponent<IAudioController>();
    }

    private void OnEnable()
    {
        gameController.OnDeath += OnDeathSound;
        gameController.OnPause += OnMenuSound;
        gameController.OnUnpause += OnExitMenuSound;
    }

    private void OnDisable()
    {
        gameController.OnDeath -= OnDeathSound;
        gameController.OnPause -= OnMenuSound;
        gameController.OnUnpause -= OnExitMenuSound;
    }

    private void OnMenuSound()
    {
        audioController.SetBackground(menuSourceName, menuClipName);
        audioController.PlayBackground(menuSourceName, 2f);
    }

    private void OnExitMenuSound()
    {
        audioController.PauseBackground(menuSourceName);
    }

    private void OnDeathSound()
    {
        audioController.PlayOneShotSFX(deathClipName);
    }
}


