using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public Players player;
    public Vector3 lastSafeSpot;
    public bool windingUp = false, stopped = false;
    public LayerMask walls;
    public LayerMask OB;

    private void Start()
    {
        lastSafeSpot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }

        if (Input.GetMouseButtonDown(0) && !windingUp && stopped && !player.finished)
        {
            windingUp = true;
            Debug.DrawLine(Input.mousePosition + transform.position, transform.position, Color.red, Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(0) && windingUp && stopped)
        {
            var mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 2;

            Shoot(Clamp(mousePos));
        }

        if (rb.velocity.magnitude > .01f)
        {
            windingUp = true;
            stopped = false;
        }

        if (rb.velocity.magnitude < .01f && !stopped)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, .05f, OB))
            {
                Reset();
                return;
            }
            stopped = true;
            windingUp = false;
            lastSafeSpot = transform.position;
        }
    }
    public void Reset()
    {
        rb.velocity = Vector3.zero;
        player.strokeCount++;
        player.holeScore++;
        transform.position = lastSafeSpot;
    }
    public void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rb.velocity, Color.red, Time.deltaTime, false);
        if (Physics.Raycast(transform.position, rb.velocity, out RaycastHit hit, .35f, walls))
        {
            Collision(hit);
        }
    }
    public void Collision(RaycastHit hit)
    {
        float speed = rb.velocity.magnitude;
        var dir = Vector3.Reflect(rb.velocity.normalized, hit.normal);
        rb.velocity = dir * (speed/2);
    }
    public Vector3 Clamp(Vector3 speed)
    {
        float width = Screen.width;
        float height = Screen.height;
        Vector3 newSpeed = Vector3.zero;
        newSpeed.x = speed.x / width;
        newSpeed.y = speed.y / height;        
        return newSpeed*20;
    }

    void Shoot(Vector3 speed)
    {
        player.strokeCount++;
        player.holeScore++;
        Vector3 targetDirection = new Vector3(-speed.x, 0f, -speed.y);
        targetDirection = player.playerCamera.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;        
        rb.velocity = targetDirection;
    }
}
