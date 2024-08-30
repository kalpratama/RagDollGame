using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // The target for the camera to follow (character)
    public float distance = 5.0f;  // Distance between the camera and the character
    public float height = 2.0f;    // Height offset of the camera from the character
    public float smoothSpeed = 0.125f; // Speed of the camera's smooth movement

    private Vector3 offset;

    void Start()
    {
        // Calculate the initial offset based on the set distance and height
        offset = new Vector3(0, height, -distance);
    }

    void LateUpdate()
    {
        // Desired camera position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Ensure the camera always looks at the target
        transform.LookAt(target);
    }
}
