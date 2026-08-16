using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerInteractor))]
public class InteractDistanceEditor: Editor
{
    private void OnSceneGUI()
    {
        PlayerInteractor player = (PlayerInteractor)target;
        Camera cameraPlayer = player.GetComponentInChildren<Camera>();

        // position what looks camera (with distance)
        Vector3 lookedPos = cameraPlayer.transform.position + (cameraPlayer.transform.rotation * (Vector3.forward * player.range));

        Handles.color = Color.green;
        Handles.DrawLine(cameraPlayer.transform.position, lookedPos);
    }
}
