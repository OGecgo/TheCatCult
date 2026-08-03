using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHead : MonoBehaviour, IPlayerHead
{
    [SerializeField] public WindowConf windowConf;
    //for testing if window dimantions is changed 
    private float lastWindowSpect;
    private float _sensitivity;
    private float _FOV;
    private InputActionReference _rotationAction;

    private IPlayerRotation playerRotation;
    private Camera playerCamera;


    public float sensitivity { get{return _sensitivity;} set{_sensitivity = value;} }
    public float FOV { get{ return _FOV; } set{ _FOV = value; } }
    public InputActionReference rotationAction { get{ return _rotationAction;} set{ _rotationAction = value; }}
    public void ManualAwake()
    {
        playerRotation = new PlayerRotation(rotationAction, sensitivity, new Vector3(0, 1, 0), this.transform);
        playerCamera = GetComponentInChildren<Camera>();
        lastWindowSpect = (float)Screen.width / (float)Screen.height;    
    }
    public void ManualStart()
    {
        AdjustCamera();
    } 
    public void ManualUpdate()
    {
        playerRotation.ManualUpdate();

        float newWindowSpect = (float)Screen.width / (float)Screen.height;
        if (newWindowSpect != lastWindowSpect)
        {
            lastWindowSpect = newWindowSpect;
            // set into window nxm game view what is presetted from winConf
            AdjustCamera();
        }
    }
 

    private void AdjustCamera()
    {

        float targetSpect = windowConf.cameraDimensions.x / windowConf.cameraDimensions.y;
        float scaleHeight = lastWindowSpect / targetSpect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = playerCamera.rect;
 
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
 
            playerCamera.rect = rect;
        }
        else if (scaleHeight > 1.0f)
        {
            float scalewidth = 1.0f / scaleHeight;
 
            Rect rect = playerCamera.rect;
 
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
 
            playerCamera.rect = rect;
        }

    }
}
