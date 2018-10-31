using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private string name = "Mario";

    [SerializeField]
    private float jumpHeight = 5;

    [SerializeField]
    private float accelerationForce = 5;

    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    private float jumpForce = 10;

    private bool hasKey;

    private bool isOnGround;

    [SerializeField]
    private Rigidbody2D rigidbody2DInstance;

    private float horizontalInput;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    private Collider2D[] groundHitDetectionResults = new Collider2D[16];

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private Collider2D playerGroundCollider;

    //private Checkpoint currentCheckpoint;

    // Use this for initialization
    void Start()
    {
        rigidbody2DInstance = GetComponent<Rigidbody2D>();
        Debug.Log("Rawr");  //How to print to the console in Unity
        string pizza = "yum";
        Debug.Log(pizza);
    }

    // Update is called once per frame
    private void Update()
    {
        //transform.Translate(0, -.01f, 0); don't use because of physics
        //GetInput();
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();

    }

    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        Movement();
    }

    private void UpdatePhysicsMaterial()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }

        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }

    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
        //Debug.Log("IsOnGround?: " + isOnGround);
    }

    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rigidbody2DInstance.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Movement()
    {

        rigidbody2DInstance.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rigidbody2DInstance.velocity;
        clampedVelocity.x = Mathf.Clamp(rigidbody2DInstance.velocity.x, -maxSpeed, maxSpeed);
        rigidbody2DInstance.velocity = clampedVelocity;
    }
}
