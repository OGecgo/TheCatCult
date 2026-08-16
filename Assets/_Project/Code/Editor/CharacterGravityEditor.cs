using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CharacterGravity))]
public class CharacterGravityEditor: Editor
{

    private void OnSceneGUI()
    {
        Handles.color = Color.blue;

        CharacterGravity player = (CharacterGravity)target;
        
        Vector3 lookedUp = player.transform.position + player.groundNormal;
        Handles.DrawLine(player.transform.position, lookedUp);

        // first velocity
        Vector3 lookedForward = player.transform.position + player.velocity;
        Handles.DrawLine(player.transform.position, lookedForward);
    }
}
