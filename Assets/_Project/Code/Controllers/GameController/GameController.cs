using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : 
MonoBehaviour, IGameController,
IWorldDeathMenuUI, IWorldPauseMenuUI, IEffectUI
{
    [SerializeField] private InputActionReference pauseAction;

    private IPauseController PauseController;

    // IEnterable is used on every game state
    private IStateMachine<IStateGame> stateMachine;
    private IStateGame play;
    private IStateGame pause;
    private IStateGame death;
    private IStateGame wait;

    public event Action OnDeath;
    public event Action OnPause;
    public event Action OnUnpause;
    public event Action OnDisableEffect;
    public event Action OnEnableEffect;

    private void OnEnable()
    {
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.Disable();
    }

    private void Start()
    {
        // event take link after OnEnable()
        play = new StatePlay(OnEnableEffect, OnDisableEffect);
        pause = new StatePause(OnPause, OnUnpause);
        death = new StateDeath(OnDeath);
        wait = new StateWait(OnEnableEffect, OnDisableEffect);
        stateMachine.SetState(play);
    }

    private void Awake()
    {
        PauseController = GetComponent<IPauseController>();
        stateMachine = new StateMachine<IStateGame>();
    }

    private void Update()
    {
        if (pauseAction.action.triggered)
        {
            if (stateMachine.currentState == pause) PlayMode();
            else PauseMode();
        }
    }

    public void DeathMode()
    {
        if (stateMachine.currentState != pause && stateMachine.currentState != wait)
        {
            PauseController.PauseGame();
            stateMachine.SetState(death);
        }
        else Debug.LogWarning("GameController:: Death state not setted");
    }
    
    public void PlayMode()
    {
        if (stateMachine.currentState != death)
        {
            PauseController.UnpauseGame();
            stateMachine.SetState(play);
        }   
        else Debug.LogWarning("GameController:: Play state not setted");
    }

    public void PauseMode()
    {
        if (stateMachine.currentState != death && stateMachine.currentState != wait)
        {
            PauseController.PauseGame();
            stateMachine.SetState(pause);
        }      
        else Debug.LogWarning("GameController:: Pause state not setted");
    }

    public void WaitMode()
    {
        if (stateMachine.currentState == play)
        {
            PauseController.PauseGame();
            stateMachine.SetState(wait);
        }
        else Debug.LogWarning("GameController:: Wait state not setted");
    }
}
