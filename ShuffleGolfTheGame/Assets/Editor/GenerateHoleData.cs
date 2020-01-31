using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GenerateHoleData : Editor
{
    public GameObject holesObject;
    [SerializeField]
    public List<Holes> holes = new List<Holes>();

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        holesObject = EditorGUILayout.ObjectField("Holes Object", holesObject, typeof(GameObject), true) as GameObject;
        GameManager script = target as GameManager;

        if (GUILayout.Button("Generate Hole Data"))
        {
            if (holes.Count > 0)
                holes.Clear();

            foreach (Transform hole in holesObject.transform)
            {
                Holes temp = new Holes();
                foreach (Transform setPiece in hole.transform)
                {
                    switch (setPiece.gameObject.name)
                    {
                        case ("Tee"):
                            temp.teepad = setPiece.gameObject;
                            break;
                        case ("FowardDirection"):
                            temp.fowardDirection = setPiece.gameObject;
                            break;
                        case ("Hole"):
                            temp.hole = setPiece.gameObject;
                            break;
                    }
                }
                holes.Add(temp);
            }

            script.holes = new List<Holes>(holes);
        }
    }
}
