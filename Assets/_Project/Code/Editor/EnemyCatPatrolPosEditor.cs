using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyCatAction))]
public class EnemyCatPatrolPosEditor: Editor
{
    private float height = 5f;
    private void OnSceneGUI()
    {
        EnemyCatAction cat = (EnemyCatAction)target;
        // take pos patrol and show it 
        Vector3[] potArray = cat.patrolPositions;
        Handles.color = Color.red;
        foreach (Vector3 pot in potArray)
        {
            Handles.DrawLine(pot + Vector3.up * height, pot + Vector3.down * height);
        }

    } 
}
