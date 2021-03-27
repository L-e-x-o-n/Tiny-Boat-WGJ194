using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed;
    public float rotationSpeed;

    private Rigidbody2D rb;
    private Transform t;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float rotation = -Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");

        if (forward < 0)
        {
            rb.AddForce(t.up * (forward / 2) * forwardSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.AddForce(t.up * forward * forwardSpeed * Time.fixedDeltaTime);
        }       

        rb.AddTorque(rotation * rotationSpeed * Time.fixedDeltaTime);
    }
}
