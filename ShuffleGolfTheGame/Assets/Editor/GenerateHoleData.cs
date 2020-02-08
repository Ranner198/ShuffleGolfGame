using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GenerateHoleData : Editor
{    
    [SerializeField]
    public List<Holes> holes = new List<Holes>();

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameManager script = target as GameManager;

        if (GUILayout.Button("Generate Hole Data"))
        {
            script.GenerateHoleData();
        }
    }
}
