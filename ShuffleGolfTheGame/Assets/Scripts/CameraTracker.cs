using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CameraTracker : MonoBehaviour
{
    public GameObject playerGO;
    public Players player;
    public TextMeshProUGUI StrokeCounter;
    public GameObject centre;

    public void LateUpdate()
    {
        transform.position = playerGO.transform.position;
        StrokeCounter.text = "Strokes: " + player.holeScore.ToString();
    }
}
