using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundMenu: MonoBehaviour
{

    [SerializeField] private MonoBehaviour _audioController;
    [SerializeField] private string buttonSoundName;

    private IAudioController audioController;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        audioController = _audioController.GetComponent<IAudioController>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        audioController.PlayOneShotSFX(buttonSoundName);
    }
}
