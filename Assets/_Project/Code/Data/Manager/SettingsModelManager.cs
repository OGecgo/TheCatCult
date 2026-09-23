using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsModelManager : MonoBehaviour
{
    [Header ("soundSettings")]
    [SerializeField] private MonoBehaviour _slidersGame;
    [SerializeField] private MonoBehaviour _sliderBackground;
    [SerializeField] private MonoBehaviour _sliderSound;

    [SerializeField] private SettingsConf defaultSettingsConf;

    [Header ("Data save")]
    [SerializeField] private Button buttonAply;
    [Tooltip("gameController can be null")]
    [SerializeField] private MonoBehaviour _gameController;

    private IGameData<SettingsModel> objData;

    private IVolumeSoundSliderUIValues sliderGame;
    private IVolumeSoundSliderUIValues sliderBackground;
    private IVolumeSoundSliderUIValues sliderSound;
    private IGameController gameController;

    private void Awake()
    {
        objData = new GameDataJSON<SettingsModel>();
        sliderGame       = _slidersGame.GetComponent<IVolumeSoundSliderUIValues>();
        sliderBackground = _sliderBackground.GetComponent<IVolumeSoundSliderUIValues>();
        sliderSound      = _sliderSound.GetComponent<IVolumeSoundSliderUIValues>();     
        if ((UnityEngine.Object)_gameController != null) gameController = _gameController.GetComponent<IGameController>();
    }

    private void Start()
    {
        // load data
        if(FileController<SettingsModel>.ReadFile(defaultSettingsConf.fileName + ".json", objData))
        {
            // settings model is struct
            SettingsModel model = objData.GetObject();

            sliderGame.volume = model.gameVolume;
            sliderBackground.volume = model.backgroundVolume;
            sliderSound.volume = model.soundVolume;            
        }
        else
        {
            sliderGame.volume = defaultSettingsConf.gameVolume;
            sliderBackground.volume = defaultSettingsConf.backgroundVolume;
            sliderSound.volume = defaultSettingsConf.soundVolume;  

            objData.SaveData(new SettingsModel(defaultSettingsConf.gameVolume, defaultSettingsConf.backgroundVolume, defaultSettingsConf.soundVolume)); 
        }
    }

    private void OnEnable()
    {
        buttonAply.onClick.AddListener(ApplyData);
        if ((UnityEngine.Object)gameController != null) gameController.OnUnpause += ApplyData;
    }

    private void OnDisable()
    {
        buttonAply.onClick.RemoveListener(ApplyData);
        if ((UnityEngine.Object)gameController != null) gameController.OnUnpause -= ApplyData;
    }

    private void ApplyData()
    {

        objData.SaveData(new SettingsModel(sliderGame.volume, sliderBackground.volume, sliderSound.volume));
        FileController<SettingsModel>.WriteFile(defaultSettingsConf.fileName + ".json", objData);
    }


}
