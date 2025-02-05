using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    private bool isLocked = false;
    private Vector3 lockedPosition;

    void LateUpdate()
    {
        if (isLocked)
        {
            transform.position = Vector3.Lerp(transform.position, lockedPosition, smoothSpeed * Time.deltaTime);
        }
        else if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }

    public void LockCamera()
    {
        isLocked = true;
        lockedPosition = transform.position;
    }

    public void UnlockCamera()
    {
        isLocked = false;
    }
}
