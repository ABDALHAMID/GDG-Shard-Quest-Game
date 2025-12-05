using UnityEngine;

public class SimpleCharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;                // Normal walking speed
    public float sprintMultiplier = 2f;     // Sprint speed multiplier
    public float mouseSensitivity = 3f;     // Mouse rotation sensitivity

    [Header("Jump & Gravity Settings")]
    public float jumpForce = 5f;            // Jump strength
    public float gravity = -9.81f;          // Gravity force
    public LayerMask groundLayer;           // Layer(s) considered as ground

    private float verticalVelocity = 0f;    // Current vertical speed
    private bool isGrounded;                // Ground check flag

    [Header("Ground Check Settings")]
    public Transform groundCheck;           // Empty GameObject at player’s feet
    public float groundDistance = 0.2f;     // Radius of ground check sphere

    void Update()
    {
        // --- Rotate player with mouse ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        // --- Sprint check ---
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier;
        }

        // --- Move player relative to facing direction ---
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = transform.TransformDirection(movement); // Align movement with player rotation

        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

        // --- Ground check ---
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Keeps player "stuck" to ground
        }

        // --- Jump ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelocity = jumpForce;
        }

        // --- Apply gravity ---
        verticalVelocity += gravity * Time.deltaTime;

        // --- Apply vertical movement ---
        transform.Translate(Vector3.up * verticalVelocity * Time.deltaTime);
    }
}