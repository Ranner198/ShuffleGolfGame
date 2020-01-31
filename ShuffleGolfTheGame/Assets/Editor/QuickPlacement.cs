using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;
using System.Threading.Tasks;

#if UNITY_EDITOR
public class QuickPlacement : EditorWindow
{
    #region Class Variables 
    private Texture2D logo = null;
    private List<GameObject> prefab = null;
    private GameObject placeHolder = null;
    private GameObject parent = null;
    private int selected = 0, tempselected = -99;
    private int rotation = 0;
    private bool isDelegate = false;
    private bool setParent = false;
    private bool randomSize = false;
    private bool randomYRotation = false;
    private bool randomizeIndexValue = false;
    //private bool snapToGrid = false;
    private int index = 0;
    private int prefabLength = 0;
    private float randomRotationAmount = 45;    
    private float minVal = .5f, maxVal = 5, minSliderVal= 0.5f, maxSliderVal = 5;
    [HideInInspector]
    public float X = 0, Y = 0, Z = 0;
    private float RandomY=0;
    private float RandomSizeVal = 1;
    #endregion
    #region WindowInstantiator
    [MenuItem("Window/Quick Placement")]
    public static void ShowWindow ()
    {
        var window = GetWindow<QuickPlacement>("QuickPlacement");
        window.maxSize = new Vector2(400, 600);
        window.minSize = new Vector2(window.maxSize.x, 400);
    }
    #endregion
    #region Enable/Disable_Delegates
    void OnDestroy() { SceneView.onSceneGUIDelegate -= OnScene; DestroyImmediate(placeHolder); }
    void OnDisable() { SceneView.onSceneGUIDelegate -= OnScene; DestroyImmediate(placeHolder); }
    void OnEnable()
    {
        SceneView.onSceneGUIDelegate -= OnScene;
        SceneView.onSceneGUIDelegate += OnScene;
        isDelegate = true;
    }
    void ReEnable()
    {
        SceneView.onSceneGUIDelegate -= OnScene;
        SceneView.onSceneGUIDelegate += OnScene;
        isDelegate = true;
    }
    void DisableSceneView()
    {
        DestroyImmediate(placeHolder);
        SceneView.onSceneGUIDelegate -= OnScene;
        isDelegate = false;
    }

    private void Awake()
    {
        prefab = new List<GameObject>();
    }

    void OnScene(SceneView sceneview)
    {
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        Event e = Event.current;

        if (prefab.Count > 0)
        {
            if (prefab[0] != null)
            {
                if (placeHolder == null)
                {
                    Vector2 guiPosition = Event.current.mousePosition;
                    Ray ray = HandleUtility.GUIPointToWorldRay(guiPosition);
                    Physics.Raycast(ray);

                    if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity, 2))
                    {
                        placeHolder = Instantiate(prefab[index], hit.point, Quaternion.identity) as GameObject;
                        placeHolder.layer = 2;
                        SetChildrenToIgnoreRaycastLayer(placeHolder);
                    }
                    else
                    {
                        placeHolder = Instantiate(prefab[index], Vector3.zero, Quaternion.identity) as GameObject;
                        placeHolder.layer = 2;
                        SetChildrenToIgnoreRaycastLayer(placeHolder);
                    }

                    placeHolder.name = prefab[index].name;
                    if (randomYRotation)
                        Y = RandomY;
                    /*
                    if (snapToGrid)
                    {
                        Vector3 pos = placeHolder.transform.position;
                        placeHolder.transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.x), Mathf.Round(pos.x));
                    }
                    */
                }
                else
                {
                    Vector2 guiPosition = Event.current.mousePosition;
                    Ray ray = HandleUtility.GUIPointToWorldRay(guiPosition);
                    Physics.Raycast(ray);

                    if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity))
                    {
                        placeHolder.transform.position = hit.point;

                        switch (rotation)
                        {
                            case 0:
                            default:
                                // No rotation
                                YAxisRotation(placeHolder);
                                break;
                            case 1:
                                // Align with face
                                placeHolder.transform.rotation = Quaternion.LookRotation(placeHolder.transform.TransformDirection(Vector3.forward), hit.normal);
                                YAxisRotation(placeHolder);
                                break;
                            case 2:
                                // Random Rotation
                                placeHolder.transform.rotation = GetRandomRotation(placeHolder);
                                break;
                        }

                        if (randomSize)
                        {
                            if (RandomSizeVal < minSliderVal)
                                GetRandomSize();
                            Vector3 size = placeHolder.transform.localScale;
                            size = new Vector3(RandomSizeVal, RandomSizeVal, RandomSizeVal);
                            placeHolder.transform.localScale = size;
                        }
                        else
                        {
                            Vector3 size = placeHolder.transform.localScale;
                            size = new Vector3(RandomSizeVal, RandomSizeVal, RandomSizeVal);
                            placeHolder.transform.localScale = size;
                        }
                    }
                }

                if (e.type == EventType.ScrollWheel && e.alt && selected == 1)
                {
                    if (e.delta.y > 0)
                        index+=1;
                    else
                        index-=1;

                    if (index > prefabLength-1)
                        index = 0;                    
                    else if (index < 0)
                        index = prefabLength-1;

                    Event.current.Use();
                }

                if (e.type == EventType.ScrollWheel && e.control && selected == 1)
                {
                    if (e.delta.y > 0)
                        RandomSizeVal -= .25f;
                    else
                        RandomSizeVal += .25f;

                    if (RandomSizeVal <= 0)
                        RandomSizeVal = 0.01f;

                    Event.current.Use();
                }

                if (Event.current.keyCode == (KeyCode.RightArrow))
                {
                    Y += 2;
                    if (Y > 360)
                        Y = 0;
                    Event.current.Use();
                }
                if (Event.current.keyCode == (KeyCode.LeftArrow))
                {
                    Y -= 2;
                    if (Y < 0)
                        Y = 360;
                    Event.current.Use();
                }

                if (Event.current.keyCode == (KeyCode.LeftControl))
                {
                    if (tempselected == -99)
                        tempselected = rotation;
                    rotation = 1;
                    Repaint();
                }

                if (Event.current.keyCode == (KeyCode.BackQuote))
                {                    
                    if (randomSize)
                        GetRandomSize();
                    if (randomYRotation)
                    {
                        RandomYRotationGenerator();
                        Y = RandomY;
                    }
                    if (randomizeIndexValue)
                        RandomIndexValue();
                }

                if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.LeftControl)
                {
                    rotation = tempselected;
                    Repaint();
                    tempselected = -99;
                }

                if (Event.current.button == 0)
                {
                    if (e.type == EventType.MouseDown && !e.alt)
                    {
                        if (selected == 1)
                        {
                            Event.current.Use();
                            PlaceObject();                            
                        }
                        else if (selected == 0)
                        {
                            DisableSceneView(); // Disable the mouse input
                        }
                        HandleUtility.AddDefaultControl(0); // Consume Mouse Input
                    }
                }
            }

            if (prefab[index] != null && placeHolder != null)
            {
                if (placeHolder.gameObject.name != prefab[index].gameObject.name)
                    DestroyImmediate(placeHolder);
            }
        }
    }
    #endregion
    #region LocalMethods
    void PlaceObject()
    {
        Vector2 guiPosition = Event.current.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(guiPosition);
        Physics.Raycast(ray);

        if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity))
        {

            GameObject temp = Instantiate(prefab[index], hit.point, Quaternion.identity) as GameObject;
            // Assign Rotation
            switch (rotation)
            {
                case 0:
                default:
                    // No rotation
                    YAxisRotation(temp);
                    break;
                case 1:
                    // Align with face
                    temp.transform.rotation = Quaternion.LookRotation(temp.transform.TransformDirection(Vector3.forward), hit.normal);
                    YAxisRotation(temp);
                    break;
                case 2:
                    // Random Rotation
                    temp.transform.rotation = GetRandomRotation(temp);
                    break;
            }

            if (setParent)
            {
                if (parent != null)
                    temp.transform.parent = parent.transform;
            }

            if (randomSize)
            {
                Vector3 size = temp.transform.localScale;
                size = new Vector3(RandomSizeVal, RandomSizeVal, RandomSizeVal);
                temp.transform.localScale = size;
            }
            else
            {
                Vector3 size = temp.transform.localScale;
                size = new Vector3(RandomSizeVal, RandomSizeVal, RandomSizeVal);
                temp.transform.localScale = size;
            }

            temp.gameObject.name = prefab[index].name;
            DestroyImmediate(placeHolder);
        }

        // Reset Randomizers
        if (randomSize)
            GetRandomSize();
        if (randomYRotation)
            RandomYRotationGenerator();
        if (randomizeIndexValue)
            RandomIndexValue();

        Event.current.Use();
    }
    void YAxisRotation(GameObject rotator)
    {
        Vector3 m_rotation = rotator.transform.eulerAngles;
        m_rotation.y = Y;
        rotator.transform.rotation = Quaternion.Euler(m_rotation);
    }

    void RandomYRotationGenerator() { RandomY = Random.Range(0, 360); }

    void AddRandomRotationValue(GameObject rotator)
    {
        Vector3 m_rotation = rotator.transform.eulerAngles;
        m_rotation.y += RandomY;
        rotator.transform.rotation = Quaternion.Euler(m_rotation);
    }

    void SetChildrenToIgnoreRaycastLayer(GameObject T)
    {
        foreach (Transform child in T.transform)
        {            
            child.gameObject.layer = 2;
            SetChildrenToIgnoreRaycastLayer(child.gameObject);
        }
    }
    #endregion
    #region Randomizers
    void GetRandomSize()
    {
        if (minSliderVal < minVal || maxSliderVal == 0)
        {
            minSliderVal = minVal;
            maxSliderVal = maxVal;
        }
        RandomSizeVal = Random.Range(minSliderVal, maxSliderVal);
    }
    Quaternion GetRandomRotation(GameObject T) { return Quaternion.Euler(T.transform.rotation.x + Random.Range(-randomRotationAmount, randomRotationAmount), T.transform.rotation.y + Random.Range(-randomRotationAmount, randomRotationAmount), T.transform.rotation.z + Random.Range(-randomRotationAmount, randomRotationAmount)); }
    void RandomIndexValue() { index = Random.Range(0, prefab.Count); }
    #endregion
    #region GUI
    void OnSceneGUI()
    {
        if (selected == 1)
        {
            if (Event.current.type == EventType.Layout)
            {
                Event.current.Use();
                HandleUtility.AddDefaultControl(0);
            }
        }
    }
    void OnGUI () // Editor GUI
    {
        EventType e = Event.current.type;

        if (logo == null)
            logo = (Texture2D)Resources.Load("QuickPlacement", typeof(Texture2D));

        // Editor Icon
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label(logo, GUILayout.MaxHeight(200));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical(EditorStyles.helpBox);

        if (selected == 1 && !isDelegate)
            ReEnable();

        if (Event.current.type == EventType.Layout)
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));

        // Selections
        string[] options = new string[] { "  Disable", "  Place Gameobjects" };
        selected = GUILayout.SelectionGrid(selected, options, 1, EditorStyles.radioButton);

        prefabLength = Mathf.Max(0, EditorGUILayout.IntField("size", prefabLength));
        
        while (prefabLength < prefab.Count)
        {
            prefab.RemoveAt(prefab.Count - 1);
        }
        while (prefabLength > prefab.Count)
        {
            prefab.Add(null);
        }      
        
        for (int i = 0; i < prefab.Count; i++)
        {
            prefab[i] = EditorGUILayout.ObjectField(i+1 + ") GameObject:", prefab[i], typeof(GameObject), true) as GameObject;
        }

        string[] rotationalOptions = new string[] { "  No Alignment", "  Align with face", "  Random" };
        rotation = GUILayout.SelectionGrid(rotation, rotationalOptions, 1, EditorStyles.radioButton);

        // If Randomize Rotation
        if (rotation == 2)
            randomRotationAmount = EditorGUILayout.FloatField("Random Rotation Amount:", randomRotationAmount);

        setParent = EditorGUILayout.Toggle("Set Parent Object:", setParent);

        // If set parent is checked
        if (setParent)
            parent = EditorGUILayout.ObjectField("Parent Object:", parent, typeof(GameObject), true) as GameObject;

        randomSize = EditorGUILayout.Toggle("Randomize Size:", randomSize);

        randomYRotation = EditorGUILayout.Toggle("Randomize Y Rotation", randomYRotation);

        randomizeIndexValue = EditorGUILayout.Toggle("Randomize Spawned Prefab: ", randomizeIndexValue);        

        // If randomize size is selected
        if (randomSize)
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Random Size Slider: ");
            minVal = EditorGUILayout.FloatField("Minimum Slider Value", minVal);
            maxVal = EditorGUILayout.FloatField("Maximum Slider Value", maxVal);
            EditorGUILayout.MinMaxSlider(ref minSliderVal, ref maxSliderVal, minVal, maxVal);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.FloatField("Min Val", minSliderVal);
            EditorGUILayout.FloatField("Max Val", maxSliderVal);
            EditorGUI.EndDisabledGroup();
            GUILayout.EndVertical();
        }

        GUILayout.EndHorizontal();    
    }
    #endregion
}
#endif
