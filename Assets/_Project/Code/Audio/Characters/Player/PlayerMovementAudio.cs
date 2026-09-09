using UnityEngine;

public class PlayerMovementAudio : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private string soundJumpName;
    [SerializeField] private string soundRunName;
    [SerializeField] private string soundWalkName;
    [SerializeField] private string soundTouchGroundName;
    [SerializeField] private string channelName;

    private IPlayerMovement playerMovement;
    private IAudioController audioController;

    private void OnEnable()
    {
        playerMovement.OnJump += PlayJumpAudio;
        playerMovement.OnRun += PlayRunAudio;
        playerMovement.OnStopRun += StopRunAudio;
        playerMovement.OnWalk += PlayWalkAudio;
        playerMovement.OnStopWalk += StopWalkAudio;
        playerMovement.OnTouchGround += PlayTouchGroundAudio;
    }
    private void OnDisable()
    {
        playerMovement.OnJump -= PlayJumpAudio;
        playerMovement.OnRun -= PlayRunAudio;
        playerMovement.OnStopRun -= StopRunAudio;
        playerMovement.OnWalk -= PlayWalkAudio;
        playerMovement.OnStopWalk -= StopWalkAudio;
        playerMovement.OnTouchGround -= PlayTouchGroundAudio;
    }

    private void Awake()
    {
        playerMovement = this.GetComponent<IPlayerMovement>();
        audioController = _audioController.GetComponent<IAudioController>();
    }

    private void PlayJumpAudio()
    {
        audioController.PlayOneShotSFX(soundJumpName);   
    }
    
    private void PlayRunAudio()
    {
        // dont play again run if it played
        if  ( audioController.IsPlayedBackground(channelName) && audioController.GetPlayedClipBackground(channelName) == soundRunName)
        {
            return;
        }

        audioController.SetBackground(channelName, soundRunName);
        audioController.PlayBackground(channelName);
    }

    private void StopRunAudio()
    {
        // stop only if its run
        if  ( audioController.IsPlayedBackground(channelName) && audioController.GetPlayedClipBackground(channelName) == soundRunName)
        {
            audioController.PauseBackground(channelName);
        }        
    }

    private void PlayWalkAudio()
    {
        // dont play again walk if it played
        if  ( audioController.IsPlayedBackground(channelName) && audioController.GetPlayedClipBackground(channelName) == soundWalkName)
        {
            return;
        }
        audioController.SetBackground(channelName, soundWalkName);
        audioController.PlayBackground(channelName);
    }
    private void StopWalkAudio()
    {
        // stop only if its walk
        if  ( audioController.IsPlayedBackground(channelName) && audioController.GetPlayedClipBackground(channelName) == soundWalkName)
        {
            audioController.PauseBackground(channelName);
        }      
    }

    private void PlayTouchGroundAudio()
    {
        audioController.PlayOneShotSFX(soundTouchGroundName);   
    }
}
