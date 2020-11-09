using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;

    [SerializeField] private Transform playerBody;

    private float xRotation = 0f; // Variable to rotate aroound the X axis

    // Start is called before the first frame update
    void Start()
    {
        // Lock the cursor into position and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Times by Time.deltaTime so that we can rotate independant of current frame rate
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Times.deltaTime is the time since the last update was called
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limits the rotation

        // Quaternion is responsible for rotating in Unity
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Specify the axis to rotate the player
        playerBody.Rotate(Vector3.up * mouseX); // Vector3.up refers to the Y axis
    }
}