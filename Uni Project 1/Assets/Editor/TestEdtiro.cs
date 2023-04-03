/*
 Custom editor to expand the PathEditor with buttons and Handles

 Preventing the mouse to select something from the scene is from this site:
 https://answers.unity.com/questions/173950/disable-mouse-selection-in-editor-view.html
 */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathEditor)), CanEditMultipleObjects]
public class TestEdtiro : Editor
{
    PathEditor pathE;
    private void OnEnable()
    {
        pathE = (PathEditor)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Spawn Crowd"))
        {
            pathE.SpawnCrowd();
        }

        if (GUILayout.Button("Clear Crowd"))
        {
            pathE.ClearCrowd();
        }

        if (GUILayout.Button("Clear Nodes"))
        {
            pathE.ClearNodes();
        }
    }

    protected virtual void OnSceneGUI()
    {
        // Handles to freely change the nodes position
        for (int i = 0; i < pathE.routePoints.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 position = Handles.PositionHandle(pathE.routePoints[i], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                pathE.routePoints[i] = position;
            }
        }

        // Right mouse button: Add a note to the mouse position
        // Left mouse button: delete the last node I added
        // HandleUtility FocusType.Passive prevents to select other objects on the scene
        if (pathE.pathBuild)
        {
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                pathE.AddNode();
            }

            if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
            {
                pathE.DeleteLastNode();
            }

            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
    }
}
