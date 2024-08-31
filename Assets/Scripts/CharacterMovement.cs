using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Rigidbody hips; // Assign the hips Rigidbody of the ragdoll in the inspector

    private Vector3 moveDirection;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        hips.AddTorque(Vector3.up * 100f, ForceMode.VelocityChange);

        // Get input for movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate the move direction relative to the camera
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0f; // Keep the direction parallel to the ground
        right.y = 0f; // Keep the direction parallel to the ground

        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * vertical + right * horizontal).normalized;

        // Apply movement and rotation
        if (moveDirection != Vector3.zero)
        {
            // Move the character's hips in the move direction
            hips.MovePosition(hips.position + moveDirection * moveSpeed * Time.deltaTime);

            // Rotate the character towards the move direction
            RotateTowards(moveDirection);
        }
    }

    void RotateTowards(Vector3 targetDirection)
    {
        // Calculate the torque required to rotate towards the target direction
        Vector3 torqueDirection = Vector3.Cross(transform.forward, targetDirection);
        hips.AddTorque(torqueDirection * rotationSpeed, ForceMode.VelocityChange);
    }
}
