using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;          // The player or target the camera will follow
    public Vector3 offset;            // Offset of the camera from the target
    public float followSpeed = 10f;   // Speed at which the camera follows the player
    public float mouseSensitivity = 100f;  // Mouse sensitivity for camera rotation

    private float pitch = 0f;         // Vertical rotation (up and down)
    private float yaw = 0f;           // Horizontal rotation (left and right)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor in the middle of the screen
        Cursor.visible = false;                    // Hide the cursor
    }

    void Update()
    {
        // Get mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate yaw (horizontal) and pitch (vertical) rotation
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -45f, 45f);  // Clamp pitch to prevent over-rotation

        // Calculate the new rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Apply the rotation to the camera and offset from the player
        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.LookAt(target.position);
    }
}
