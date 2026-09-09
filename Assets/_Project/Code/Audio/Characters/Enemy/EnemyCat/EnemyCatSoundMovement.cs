using UnityEngine;

public class EnemyCatSoundMovement : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private AudioSource enemyAudiotSource;
    [SerializeField] private string soundRunName;
    [SerializeField] private string soundWalkName;
    [SerializeField] private string soundTouchGroundName;

    private IEnemyCatMovement enemyCatMovement;
    // only for took clips
    private IAudioController audioController;
    private AudioClip soundRun;
    private AudioClip soundWalk;
    private AudioClip soundTouchGround;

    private void Awake()
    {
        enemyCatMovement = this.GetComponent<IEnemyCatMovement>();
        audioController = _audioController.GetComponent<IAudioController>();
    }

    private void Start()
    {
        soundRun = audioController.GetClip(soundRunName);
        soundWalk = audioController.GetClip(soundWalkName);
        soundTouchGround = audioController.GetClip(soundTouchGroundName);
    }

    private void OnEnable()
    {
        enemyCatMovement.OnRun += PlayRunAudio;
        enemyCatMovement.OnStopRun += StopRunAudio;
        enemyCatMovement.OnWalk += PlayWalkAudio;
        enemyCatMovement.OnStopWalk += StopWalkAudio;
        enemyCatMovement.OnTouchGround += PlayTouchGroundAudio;
    }
    private void OnDisable()
    {
        enemyCatMovement.OnRun -= PlayRunAudio;
        enemyCatMovement.OnStopRun -= StopRunAudio;
        enemyCatMovement.OnWalk -= PlayWalkAudio;
        enemyCatMovement.OnStopWalk -= StopWalkAudio;
        enemyCatMovement.OnTouchGround -= PlayTouchGroundAudio;
    }

    private void PlayRunAudio()
    {
        // dont play again run if it played
        if (enemyAudiotSource.isPlaying && enemyAudiotSource.clip == soundRun)
        {
            return;
        }
        enemyAudiotSource.clip = soundRun;
        enemyAudiotSource.Play();
    }

    private void StopRunAudio()
    {
        // stop only if its run
        if (enemyAudiotSource.isPlaying && enemyAudiotSource.clip == soundRun)
        {
            enemyAudiotSource.Stop();
        }
    }

    private void PlayWalkAudio()
    {
        Debug.Log("hellow ??");
        // dont play again walk if it played
        if (enemyAudiotSource.isPlaying && enemyAudiotSource.clip == soundWalk)
        {
            return;
        }
        enemyAudiotSource.clip = soundWalk;
        enemyAudiotSource.Play();
    }

    private void StopWalkAudio()
    {
        // stop only if its walk
        if (enemyAudiotSource.isPlaying && enemyAudiotSource.clip == soundWalk)
        {
            enemyAudiotSource.Stop();
        }
    }

    private void PlayTouchGroundAudio()
    {
        enemyAudiotSource.PlayOneShot(soundTouchGround);
    }
}

// Functions in this file:
// - Awake()
// - Start()
// - OnEnable()
// - OnDisable()
// - PlayRunAudio()
// - StopRunAudio()
// - PlayWalkAudio()
// - StopWalkAudio()
// - PlayTouchGroundAudio()
