using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeed;
    public float rotationSpeed;

    private Rigidbody2D rb;
    private Transform t;
    private Player p;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
        p = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        float rotation = -Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");

        if (forward < 0)
        {
            rb.AddForce(t.up * (forward / 2) * forwardSpeed  * p.forwardSpeedModifier * Time.fixedDeltaTime);
        }
        else
        {
            rb.AddForce(t.up * forward * forwardSpeed * Time.fixedDeltaTime);
        }       

        rb.AddTorque(rotation * rotationSpeed * p.rotationSpeedModifier * Time.fixedDeltaTime);
    }
}
