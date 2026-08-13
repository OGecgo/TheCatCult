using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FOVDetection))]
public class FOVDetectionEditor: Editor
{

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void OnSceneGUI()
    {
        FOVDetection fov = (FOVDetection)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.fovConf.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.fovConf.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.fovConf.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.fovConf.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.fovConf.radius);
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.fovConf.closeRadius);

        if (fov.isTarget)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.posTarget);
        }
    }
}
