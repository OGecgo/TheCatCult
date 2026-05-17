using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHead : MonoBehaviour, IPlayerHead
{


    private float _sensitivity;
    private float _FOV;
    private InputActionReference _lookAction;

    public float sensitivity { get{return _sensitivity;} set{_sensitivity = value;} }
    public float FOV { get{ return _FOV; } set{ _FOV = value; } }
    public InputActionReference lookAction { get{ return _lookAction;} set{ _lookAction = value; }}

    private IPlayerRotation playerRotation;
    private Camera player_camera;
    private PlayerCameraConf config;
    private float lastWindowSpect;

    public void ManualAwake()
    {
        playerRotation = new PlayerRotation(lookAction, sensitivity, new Vector3(0, 1, 0), this.transform);
        player_camera = GetComponentInChildren<Camera>();
        config = Resources.Load<PlayerCameraConf>("Configuration/Player/PlayerCameraConf");
        lastWindowSpect = (float)Screen.width / (float)Screen.height;    
    }
    public void ManualStart()
    {
        AdjustCamera();
    } 
    public void ManualUpdate()
    {
        playerRotation.Update();

        float newWindowSpect = (float)Screen.width / (float)Screen.height;
        if (newWindowSpect != lastWindowSpect)
            lastWindowSpect = newWindowSpect;
            AdjustCamera();


        // // only for debuging Debuging
        // playerRotation.ChangeSensitivity(sensitivity);
    }
 

    private void AdjustCamera()
    {
        float targetSpect = config.cameraDimensions.x / config.cameraDimensions.y;
        float scaleHeight = lastWindowSpect / targetSpect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = player_camera.rect;
 
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
 
            player_camera.rect = rect;
        }
        else if (scaleHeight > 1.0f)
        {
            float scalewidth = 1.0f / scaleHeight;
 
            Rect rect = player_camera.rect;
 
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
 
            player_camera.rect = rect;
        }

    }
}
