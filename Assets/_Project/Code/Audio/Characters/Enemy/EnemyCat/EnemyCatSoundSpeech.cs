using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCatSoundSpeech : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioSource audioSFX;
    [SerializeField] private string wasHitSoundName;
    [SerializeField] private string followPlayerSoundName;
    [SerializeField] private string pathWalksSoundName;
    [SerializeField] private string randomWalksSoundName;
    

    private ILifeAction enemyCatLife;
    private IEnemyCatAction enemyCatAction;
    private IAudioController audioController;

    private AudioClip wasHitSound;
    private AudioClip followPlayerSound;
    private AudioClip pathWalksSound;
    private AudioClip randomWalksSound;

    private void Awake()
    {
        enemyCatLife = this.GetComponent<ILifeAction>();
        enemyCatAction = this.GetComponent<IEnemyCatAction>();
        audioController = _audioController.GetComponent<IAudioController>();
    }

    private void Start()
    {
        wasHitSound = audioController.GetClip(wasHitSoundName);
        followPlayerSound = audioController.GetClip(followPlayerSoundName);
        pathWalksSound = audioController.GetClip(pathWalksSoundName);
        randomWalksSound = audioController.GetClip(randomWalksSoundName);
    }

    private void OnEnable()
    {
        enemyCatLife.OnIsHit += OnIsHitSound;
        enemyCatAction.OnAction += OnActionSound;
    }

    private void OnDisable()
    {
        enemyCatLife.OnIsHit -= OnIsHitSound;
        enemyCatAction.OnAction -= OnActionSound;
    }

    private void OnIsHitSound()
    {
        audioSFX.PlayOneShot(wasHitSound);
    }

    private void OnActionSound(IEnemyCatAction.ActionType type)
    {
        switch (type)
        {
            case IEnemyCatAction.ActionType.FOLLOW_PLAYER:
                if (audioPlayer.clip != followPlayerSound || !audioPlayer.isPlaying)
                {
                    audioPlayer.clip = followPlayerSound;
                    audioPlayer.Play();                    
                }
                break;
            case IEnemyCatAction.ActionType.PATH_WALKS:
                if (audioPlayer.clip != pathWalksSound || !audioPlayer.isPlaying)
                {
                    audioPlayer.clip = pathWalksSound;
                    audioPlayer.Play();                    
                }
                break;
            case IEnemyCatAction.ActionType.RANDOM_WALKS:
                if (audioPlayer.clip != randomWalksSound || !audioPlayer.isPlaying)
                {
                    audioPlayer.clip = randomWalksSound;
                    audioPlayer.Play();                    
                }
                break;
            case IEnemyCatAction.ActionType.LOOK_AROUND:
                audioPlayer.Pause();
                break;
            default:
                audioPlayer.Pause();
            break;
        }
    }


}
