using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovement : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    [SerializeField] private float maxVeloxity;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private GameObject thruster;
    [SerializeField] private float lastThrust;
    [SerializeField] private float showThrustFor;


    #region Singleton
    public static PlayerShipMovement Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        thruster.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");

        ApplyForwardThrust(yInput);
        ApplyRotation(transform, xInput * rotationSpeed);
        ClampVelocity();

        if(Time.time - lastThrust >= showThrustFor)
        {
            thruster.SetActive(false);
        }
    }

    private void ClampVelocity()
    {
        float xVelClamped = Mathf.Clamp(rigidBody.velocity.x, -maxVeloxity, maxVeloxity);
        float yVelClamped = Mathf.Clamp(rigidBody.velocity.y, -maxVeloxity, maxVeloxity);
        rigidBody.velocity = new Vector2(xVelClamped, yVelClamped);
    }
    private float additionalThrust = 2.0f;
    private void ApplyForwardThrust(float factor)
    {
        if(factor >= 0.01f)
        {
            thruster.SetActive(true);
            lastThrust = Time.time;
        }
        // Up == forward in this setup
        Vector2 force = transform.up * factor * additionalThrust;
        rigidBody.AddForce(force);

    }

    private void ApplyRotation(Transform t, float factor)
    {
        // Rotate is in oposite direction as expected
        t.Rotate(0, 0, -factor);
    }
}
