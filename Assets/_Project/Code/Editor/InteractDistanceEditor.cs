using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using System.Drawing.Imaging;

[CustomEditor(typeof(PlayerInteractor))]
public class InteractDistanceEditor: Editor
{
    private void OnSceneGUI()
    {
        PlayerInteractor player = (PlayerInteractor)target;
        Camera cameraPlayer = player.GetComponentInChildren<Camera>();

        // position what looks camera (with distance)
        float r = player.range;
        Vector3 lookedPos = cameraPlayer.transform.position + (cameraPlayer.transform.rotation * (Vector3.forward * r));

        Handles.color = Color.green;
        Handles.DrawLine(cameraPlayer.transform.position, lookedPos);
    }
}
