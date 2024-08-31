using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollFootstepMovement : MonoBehaviour
{
    public Rigidbody leftFoot;
    public Rigidbody rightFoot;
    public float stepSpeed = 5f;
    public float liftHeight = 0.3f;
    public float stepDuration = 0.3f;

    private bool isLeftFootMoving = false;
    private bool isRightFootMoving = false;

    void Update()
    {
        // Calculate the positions to move the feet
        Vector3 leftFootTargetPos = CalculateFootTargetPosition(leftFoot.transform);
        Vector3 rightFootTargetPos = CalculateFootTargetPosition(rightFoot.transform);

        // Move the feet alternately
        if (!isLeftFootMoving && !isRightFootMoving)
        {
            if (ShouldMoveLeftFoot())
            {
                StartCoroutine(MoveFoot(leftFoot, leftFootTargetPos));
            }
            else if (ShouldMoveRightFoot())
            {
                StartCoroutine(MoveFoot(rightFoot, rightFootTargetPos));
            }
        }
    }

    Vector3 CalculateFootTargetPosition(Transform foot)
    {
        Vector3 footTargetPos = foot.position;
        footTargetPos.y += liftHeight; // Add a lift height for stepping
        return footTargetPos;
    }

    IEnumerator MoveFoot(Rigidbody foot, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = foot.position;

        if (foot == leftFoot)
        {
            isLeftFootMoving = true;
        }
        else if (foot == rightFoot)
        {
            isRightFootMoving = true;
        }

        while (elapsedTime < stepDuration)
        {
            foot.MovePosition(Vector3.Lerp(startPosition, targetPosition, elapsedTime / stepDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foot.MovePosition(targetPosition);

        if (foot == leftFoot)
        {
            isLeftFootMoving = false;
        }
        else if (foot == rightFoot)
        {
            isRightFootMoving = false;
        }
    }

    bool ShouldMoveLeftFoot()
    {
        // Calculate conditions for the left foot to step forward
        return leftFoot.transform.position.z < rightFoot.transform.position.z;
    }

    bool ShouldMoveRightFoot()
    {
        // Calculate conditions for the right foot to step forward
        return rightFoot.transform.position.z < leftFoot.transform.position.z;
    }
}
