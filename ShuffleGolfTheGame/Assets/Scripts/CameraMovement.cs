using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public float speed, minDistance, maxDistance;

    public float rotation;

    void LateUpdate()
    {
        Vector3 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.RotateAround(target.transform.position, Vector3.up, -input.x * speed * Time.deltaTime);
        transform.RotateAround(target.transform.position, transform.right, input.y * speed * Time.deltaTime);

        Vector2 scrollWheel = Input.mouseScrollDelta;
        transform.position += transform.forward * scrollWheel.y*25 * Time.deltaTime;

        float distance = (target.transform.position - transform.position).magnitude;
        if (distance > maxDistance)
        {
            transform.position += transform.forward * 2;
        }
        if (distance < minDistance)
        {
            transform.position -= transform.forward/2;
        }
    }
}
