using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovement : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    [SerializeField] private float maxVeloxity;
    [SerializeField] private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");

        ApplyForwardThrust(yInput);
        ApplyRotation(transform, xInput * rotationSpeed);
        ClampVelocity();
    }

    private void ClampVelocity()
    {
        float xVelClamped = Mathf.Clamp(rigidBody.velocity.x, -maxVeloxity, maxVeloxity);
        float yVelClamped = Mathf.Clamp(rigidBody.velocity.y, -maxVeloxity, maxVeloxity);
        rigidBody.velocity = new Vector2(xVelClamped, yVelClamped);
    }

    private void ApplyForwardThrust(float factor)
    {
        // Up == forward in this setup
        Vector2 force = transform.up * factor;
        rigidBody.AddForce(force);

    }

    private void ApplyRotation(Transform t, float factor)
    {
        // Rotate is in oposite direction as expected
        t.Rotate(0, 0, -factor);
    }
}
