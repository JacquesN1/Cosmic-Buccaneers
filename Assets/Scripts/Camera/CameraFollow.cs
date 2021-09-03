using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = .15f;
    Vector3 velocity = Vector3.zero;

    //screen clamping
    public bool yMaxEnabled = false;
    public float yMaxValue = 0;
    public bool yMinEnabled = false;
    public float yMinValue = 0;
    public bool xMaxEnabled = false;
    public float xMaxValue = 0;
    public bool xMinEnabled = false;
    public float xMinValue = 0;

    private void FixedUpdate()
    {
        //move camera to player position
        Vector3 targetPos = target.position;

        // clamp y axis
        if (yMinEnabled && yMaxEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);
        }
        else if (yMinEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, target.position.y);
        }
        else if (yMaxEnabled)
        {
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, yMaxValue);
        }

        // clamp x axis
        if (xMinEnabled && xMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);
        }
        else if (xMinEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, target.position.x);
        }
        else if (xMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, yMaxValue);
        }

        // set target z position to cameras current z position
        targetPos.z = transform.position.z;

        //smooth camera movement
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

}
