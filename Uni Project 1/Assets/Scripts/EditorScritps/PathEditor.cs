/*
 This scripts is helps to generate nodes and router for the none playable characters (crowd)
 ExecuteInEditMode means this scripts also run while its in editor mode (the Update is different than the play mode, only updates when
 we changes something on the Inspector UI.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PathEditor : MonoBehaviour
{
    public bool pathBuild;
    [SerializeField] GameObject crowd;
    [SerializeField] Color pathColor;
    public List<Vector3> routePoints;
    public List<GameObject> tempCrowd;


    // Add a node to the mouse position
    public void AddNode()
    {
#if UNITY_EDITOR
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);


        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            routePoints.Add(hitInfo.point);
        }
#endif
    }

    public void DeleteLastNode()
    {
        routePoints.Remove(routePoints[routePoints.Count - 1]);
    }

    public void ClearNodes()
    {
        routePoints.Clear();
        routePoints.Add(Vector3.zero);
    }

    public void ClearCrowd()
    {
        for (int i = 0; i < tempCrowd.Count; i++)
        {
            DestroyImmediate(tempCrowd[i]);
        }
    }

    // Delete every npc first and replace them to the new node positions
    public void SpawnCrowd()
    {
        for (int i = 0; i < tempCrowd.Count; i++)
        {
            DestroyImmediate(tempCrowd[i]);
        }

        tempCrowd.Clear();

        for (int i = 0; i < routePoints.Count - 1; i++)
        {
            GameObject go = Instantiate(crowd, routePoints[i], Quaternion.identity, transform);
            tempCrowd.Add(go);
            go.GetComponent<crowd>().path = routePoints;
            go.GetComponent<crowd>().pathIndex = i;
        }
    }

    // Path Gizmos for visual representation
    void OnDrawGizmos()
    {
        for (int i = 0; i < routePoints.Count - 1; i++)
        {
            Gizmos.color = pathColor;
            Gizmos.DrawLine(routePoints[i], routePoints[i + 1]);
        }

        for (int i = 1; i < routePoints.Count - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(routePoints[i], 2);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(routePoints[0], 4);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(routePoints[routePoints.Count - 1], 4);

    }
}

