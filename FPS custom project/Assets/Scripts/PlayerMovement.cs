using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float speed = 12f; // f so complier knows its a float
    [SerializeField] private float gravity = -9.8f; // Downward force value due to gravity
    [SerializeField] private float jumpHeight = 3f;

    private Vector3 velocity; //stores velocity

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask; // Control what objects the sphere should check for
    private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        // Radius is groundDistance and layer mask is groundMask
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // We do -2 so that we register were fully on the ground
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Player direction based of X and Z input
        Vector3 move = transform.right * x + transform.forward * z;

        // Reference to character controller
        controller.Move(move * speed * Time.deltaTime); // Time.deltaTime for frame rate independant

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // jumpHeight is decided, the rest is based on formulas
        }

        // Increase velocity
        velocity.y += gravity * Time.deltaTime;

        // Add velocity to player and times by Time.deltaTime because of physics of a free fall
        controller.Move(velocity * Time.deltaTime);
    }
}