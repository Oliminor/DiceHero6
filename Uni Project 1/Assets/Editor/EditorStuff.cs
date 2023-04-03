/*
 Experiment with EditorWindow, at this state its not functioning 
 Originally this script Instantiate the spectator terrace to the 4 side based on the arena scaling
 but I changed something later and this script is broken
 */
using System.Collections;
using UnityEngine;
using UnityEditor;

public class EditorStuff : EditorWindow
{
    public Object terrace;
    public int test;

    [MenuItem("Window/MyEditor")]


    public static void ShowWindow()
    {
        GetWindow<EditorStuff>("MyEditor");
    }

    private void OnGUI()
    {
        GUILayout.Space(20);

        GUILayout.Label("Spawn terraces around the Arena", EditorStyles.label);

        terrace = EditorGUILayout.ObjectField(terrace, typeof(GameObject), true);

        GUILayout.Space(20);

        if (GUILayout.Button("Spawn Terraces"))
        {
            SpawnTerrace();
        }
    }

    void SpawnTerrace()
    {
        GameObject obj = Selection.activeGameObject;

        if (obj.tag == "arena")
        {
            int counter = 0;
            for (int i = 0; i < 4; i++)
            {
                float sizeX = obj.GetComponent<BoxCollider>().size.x * obj.transform.localScale.x / 2;
                float sizeZ = obj.GetComponent<BoxCollider>().size.z * obj.transform.localScale.z / 2;
                GameObject o = Instantiate(terrace, obj.transform.position, Quaternion.identity, obj.transform.parent) as GameObject;
                o.transform.localRotation = Quaternion.Euler(0, 0 + (i * 90), 0);

                switch (counter)
                {
                    case 0: 
                        o.transform.localPosition = new Vector3(0, 0, obj.transform.position.x - (sizeZ + 24));
                        o.transform.localScale = new Vector3(sizeX, 1, 1);
                        break;
                    case 1: 
                        o.transform.localPosition = new Vector3(obj.transform.position.z - (sizeX - 14), 0, 0);
                        o.transform.localScale = new Vector3(sizeZ, 1, 1);
                        break;
                    case 2: 
                        o.transform.localPosition = new Vector3(0, 0, obj.transform.position.x + (sizeZ - 24));
                        o.transform.localScale = new Vector3(sizeX, 1, 1);
                        break;
                    case 3: 
                        o.transform.localPosition = new Vector3(obj.transform.position.z + (sizeX + 14), 0, 0);
                        o.transform.localScale = new Vector3(sizeZ, 1, 1);
                        break;
                }
                counter++;
            }
        }
        else Debug.Log("The selected gameObject is not arena");
    }
}
