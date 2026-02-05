using System.Security.Cryptography;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // Score tracking
    private int score = 0;

    // Movement variables
    public float moveSpeed = 5f;

    // Mouse look variables
    public float lookSpeed = 2f;
    public float lookXLimit = 90f;

    private float rotationX = 0f;

    // Gravity and Jumping

    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private Vector3 velocity;
    private bool isGrounded;

    // Reference to Character Controller
    private CharacterController controller;

    // Speed boost
    private float normalSpeed;
    public float boostMultiplier = 2f;

    void Start()
    {
        // Get the Character Controller component
        controller = GetComponent<CharacterController>();

     
            controller = GetComponent<CharacterController>();

            // Lock and hide the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Store the normal speed
            normalSpeed = moveSpeed;


    }

    void Update()
    {
        // Check if player is on the ground
        isGrounded = controller.isGrounded;

        // If on ground and falling, reset vertical velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value keeps us grounded
        }

        // Jump when spacebar pressed (only if on ground)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Jumped!");
        }

        // Get input from keyboard
        float horizontal = Input.GetAxis("Horizontal"); // A and D keys
        float vertical = Input.GetAxis("Vertical");     // W and S keys

        // Create movement direction
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Move the character
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        // Rotate camera up and down
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Rotate player left and right
        transform.Rotate(Vector3.up * mouseX);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply vertical movement
        controller.Move(velocity * Time.deltaTime);
    }

    // This method is called when player enters a trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            score++;
            Debug.Log("Collected! Score: " + score);
            Destroy(other.gameObject);

            // Check if all collectibles are collected
            if (score >= 10)
            {
                Debug.Log("YOU WIN! All collectibles collected!");
                // Could add more win behavior here
            }
        }


        // Speed zone check
        if (other.gameObject.CompareTag("SpeedZone"))
        {
            moveSpeed = normalSpeed * boostMultiplier;
            Debug.Log("Speed boost activated!");
        }

        // Return to normal speed when leaving speed zone
        if (other.gameObject.CompareTag("SpeedZone"))
        {
            moveSpeed = normalSpeed;
            Debug.Log("Speed boost ended");
        }
    }
}