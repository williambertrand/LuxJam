using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMovement : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    [SerializeField] private float maxVeloxity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speed = 40.0f;

    private float boostFactor;

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
        boostFactor = 1.0f;
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            boostFactor = 5.0f;
        }
        else
        {
            boostFactor = 1.0f;
        }


    }

    private Vector3 boostScale = new Vector3(2.0f, 2.0f, 1.0f);
    private Vector3 normalScale = new Vector3(1.4f, 1.0f, 1.0f);

    private void ClampVelocity()
    {
        float xVelClamped = Mathf.Clamp(rigidBody.velocity.x, -maxVeloxity, maxVeloxity);
        float yVelClamped = Mathf.Clamp(rigidBody.velocity.y, -maxVeloxity, maxVeloxity);
        rigidBody.velocity = new Vector2(xVelClamped, yVelClamped);
    }
    private void ApplyForwardThrust(float factor)
    {
        if(factor >= 0.01f)
        {
            thruster.SetActive(true);
            if(boostFactor > 1.0f)
            {
                thruster.transform.localScale = boostScale;
            } else
            {
                thruster.transform.localScale = normalScale;
            }
            lastThrust = Time.time;
        }
        // Up == forward in this setup
        Vector2 force = transform.up * factor * boostFactor * speed;
        rigidBody.AddForce(force);

    }

    private void ApplyRotation(Transform t, float factor)
    {
        // Rotate is in oposite direction as expected
        t.Rotate(0, 0, -factor);
    }
}
