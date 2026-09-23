
using UnityEngine;


// work with view
public class PlayerView : MonoBehaviour
{
    
    [SerializeField] private WindowConf winConf;

    // testing if window dimantions is changed 
    private float lastWindowSpect;
    private Camera objCamera;



    private void Awake()
    {
        objCamera = GetComponent<Camera>();
        lastWindowSpect = (float)Screen.width / (float)Screen.height;    
    } 
    private void Start()
    {
        AdjustCamera();
        objCamera.fieldOfView = winConf.FOV;  
    } 
    private void Update()
    {
        // after resize change the window proportions 
        float newWindowSpect = (float)Screen.width / (float)Screen.height;
        if (!Mathf.Approximately(newWindowSpect, lastWindowSpect))
        {
            lastWindowSpect = newWindowSpect;
            AdjustCamera();
        }
    }
 

    private void AdjustCamera()
    {

        float targetSpect = winConf.cameraDimensions.x / winConf.cameraDimensions.y;
        float scaleHeight = lastWindowSpect / targetSpect;

        Rect rect = new Rect(0, 0, 1.0f, 1.0f);

        if (scaleHeight < 1.0f)
        {
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f; 
        }
        else if (scaleHeight > 1.0f)
        {
            float scalewidth = 1.0f / scaleHeight;
  
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
        }

        objCamera.rect = rect;

    }
}
