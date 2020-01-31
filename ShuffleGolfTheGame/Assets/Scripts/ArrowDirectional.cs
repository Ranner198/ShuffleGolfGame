using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArrowDirectional : MonoBehaviour
{
    public GameObject centre;
        
    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 2;

            Vector3 direction = mousePos;
            centre.transform.rotation = Quaternion.LookRotation(direction, centre.transform.up);
        }
    }
}
