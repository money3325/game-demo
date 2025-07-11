using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [Header("¸úËæÉèÖÃ")]
    [SerializeField] public Vector3 offset = new Vector3(0, 0, -10); // ¸ÄÎªpublic
    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] Vector2 maxBoundary;
    [SerializeField] Vector2 minBoundary;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBoundary.x, maxBoundary.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBoundary.y, maxBoundary.y);

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }
}
