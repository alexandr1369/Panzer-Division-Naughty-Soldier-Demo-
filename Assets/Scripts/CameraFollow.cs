using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // target obj transform

    [SerializeField] private float cameraDistance = 5f; // distance between target and camera
    [SerializeField] private float cameraYPosition; // camera Y position under the target

    [SerializeField] private float smoothSpeed = .25f; // lerp utility

    private void FixedUpdate()
    {
        // follow the player
        Vector3 desiredPosition = target.position - target.forward * cameraDistance + new Vector3(0, cameraYPosition, 0);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
