using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SearchFeature : EditorWindow
{

    public Object ObjectName;

    public Object ComponentName;

    public string StringName;

    private int selected = 0;

    private bool findAll = false;

    public Vector2 scrollPos;

    private GameObject[] X;
    private List<GameObject> references;

    [MenuItem("Window/Search")]
    public static void ShowWindow ()
    {
        GetWindow<SearchFeature>("Search");
    }

    void OnGUI () 
    {
        try
        {
            if (references.Count == 0)
                Debug.Log("oof");
        }
        catch (System.Exception)
        {
            references = new List<GameObject>();
        }
        
            

        string[] options = new string[] { "  Search By Object", "  Search By Component", "  Search By GameObject String" };
        selected = GUILayout.SelectionGrid(selected, options, 1, EditorStyles.radioButton);

        findAll = EditorGUILayout.Toggle("Search in Assets Folder", findAll);

        if (selected == 0)
        {
            ObjectName = EditorGUILayout.ObjectField("Label:", ObjectName, typeof(Component), true);
        }
        else if (selected == 1)
        {
            ComponentName = EditorGUILayout.ObjectField("Label:", ComponentName, typeof(Object), true);
        }
        else if (selected == 2)
        {
            StringName = EditorGUILayout.TextField("Label:", StringName);
        }


        GUILayout.Label("Find the Objects with listed componets in scene: ", EditorStyles.boldLabel);

        if (GUILayout.Button("Search"))
        {
            switch(selected)
            {
                case 0:
                    if (ObjectName != null)
                        Search(ObjectName);
                break;
                case 1:
                    if (ComponentName.name.Length == 0)
                        throw new System.Exception("Error please add a Componet to search");
                    else
                        Search(ComponentName);
                break;

                case 2:
                    if (StringName.Length == 0)
                        throw new System.Exception("Error please add a String to search");
                    else
                        Search(StringName);
                break;
                
                case 3:
                    throw new System.Exception("Not Yet Implemented");

                default: {return;} // Fail Safe Case
            }
        }

        if (GUILayout.Button("Clear"))
        {
            if (references.Count > 0)
                references.Clear();
        }
                
        GUILayout.Space(20);

        scrollPos = EditorGUILayout.BeginScrollView (scrollPos, false, false);

        int Counter = 0;
        EditorGUILayout.BeginVertical();
        if (references.Count > 0)
        {
            X = new GameObject[references.Count];
            foreach (GameObject T in references)
            {
                X[Counter] = (GameObject)EditorGUILayout.ObjectField(T, typeof(GameObject), false);
                Counter++;
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView(); 
    }

    // Search for all gameobjects in the scene that have the componet searched for
    void Search(Object m_object) {

        if (references.Count > 0)
            references.Clear();

        System.Type myType = System.Type.GetType(m_object.name);       
        List<GameObject> objectsInScene = new List<GameObject>();        
        Object[] obj = Find();
        foreach (Object go in obj)
        {
            try
            {             
                if (GameObject.Find(go.name).GetComponent(myType))
                    references.Add(go as GameObject);
            }
            catch (System.Exception) { /*Oof*/ }                                    
        }   

        if (references.Count == 0)
        {
            Debug.Log("No references found in the scene");
        }     

    }

    void Search(string name) {
        if (references.Count > 0)
            references.Clear();
        Object[] obj = Find();
        foreach (Object go in obj)
        {
            if (go.name.ToLower().IndexOf(name.ToLower()) != -1)
            {
                references.Add(go as GameObject);                 
            }
        }   
        if (references.Count == 0)
        {
            Debug.Log("No references found in the scene");
        }     
    }

    Object[] Find() 
    {
        Object[] obj;
        if (findAll)
            obj = Resources.FindObjectsOfTypeAll(typeof (GameObject));            
        else
            obj = GameObject.FindObjectsOfType(typeof (GameObject));
        return obj;   
    }
}
