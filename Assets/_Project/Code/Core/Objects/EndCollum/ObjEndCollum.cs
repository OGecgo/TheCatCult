using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ObjEndCollum : MonoBehaviour, IInteractableEndCollum, IPauseFeatures, ILookedObj
{
    [SerializeField] private InputActionReference endGame;
    [SerializeField] private MonoBehaviour _gameController;
    [SerializeField] private MonoBehaviour playerDialog;
    [SerializeField] private string textForView;

    private int outlineLayer;
    private int noOutlineLayer;
    private bool featureIsPaused;
    private IDialogShow dialogShow;
    private IGameController gameController;

    public event Action OnInteracted;

    public void IsNotLooked()
    {
        this.gameObject.layer = noOutlineLayer;
    }

    public void IsLooked()
    {
        this.gameObject.layer = outlineLayer;
    }

    public void FeatureIsPaused(bool value)
    {
        featureIsPaused = value;
    }
    public void Interact()
    {
        if (featureIsPaused) return;

        dialogShow.ShowSmoothText(textForView);
        gameController.WaitMode();
        OnInteracted?.Invoke();

        StartCoroutine(CoroutineEnd());
    }

    private void OnEnable()
    {
        endGame.action.Enable();
    }

    private void OnDisable()
    {
        endGame.action.Disable();
    }

    private void Awake()
    {
        outlineLayer = LayerMask.NameToLayer("OutlineLayer");
        noOutlineLayer = LayerMask.NameToLayer("NoOutlineLayer"); 
        featureIsPaused = false;
        dialogShow = playerDialog.GetComponent<IDialogShow>();
        gameController = _gameController.GetComponent<IGameController>();
    }

    private IEnumerator CoroutineEnd()
    {
        while (true)
        {
            if (endGame.action.triggered)
            {
                if (dialogShow.isShowedText)
                {
                    SceneManager.LoadSceneAsync("EndComic");
                    break;
                }
                else
                {
                    dialogShow.ShowMessage(textForView);
                }                
            }
            yield return null;
        }
    }

}
