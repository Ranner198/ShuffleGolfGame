using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Players : MonoBehaviour
{
    public GameObject character;
    public GameObject cameraFocusPoint;
    public Camera playerCamera;    
    public int index;
    public Color color;
    public int strokeCount;
    public int holeScore;
    public bool finished;
}
