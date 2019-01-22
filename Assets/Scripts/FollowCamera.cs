using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform PlayerTransform;

    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public float RotationsSpeed = 5.0f;

    public float speedH = 2.0f;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        cameraOffset = transform.position - PlayerTransform.position;
    }


    // LateUpdate is called after Update methods
    void LateUpdate()
    {

        if (RotateAroundPlayer)
        {
            Quaternion camH = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, this.transform.up);
            Quaternion camV = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationsSpeed, -1 * this.transform.right);

            cameraOffset = camV * camH * cameraOffset;
        }

        Vector3 newPos = PlayerTransform.position + cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
    }
}


/*public class FollowCamera : MonoBehaviour
{

    public float sensitivity = 4.0f;
    private Vector3 mouseOrigin;
    private bool isRotating;
    public GameObject cam;

    void Start()
    {
    }

    protected float ClampAngle(float angle, float min, float max)
    {

        angle = NormalizeAngle(angle);
        if (angle > 180)
        {
            angle -= 360;
        }
        else if (angle < -180)
        {
            angle += 360;
        }

        min = NormalizeAngle(min);
        if (min > 180)
        {
            min -= 360;
        }
        else if (min < -180)
        {
            min += 360;
        }

        max = NormalizeAngle(max);
        if (max > 180)
        {
            max -= 360;
        }
        else if (max < -180)
        {
            max += 360;
        }

        return Mathf.Clamp(angle, min, max);
    }

    protected float NormalizeAngle(float angle)
    {
        while (angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;
        return angle;
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        if (!Input.GetMouseButton(0))
            isRotating = false;

        if (isRotating)
        {

            cam.transform.localEulerAngles = new Vector3(0, ClampAngle(cam.transform.localEulerAngles.y, -45, 45), 0);
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            transform.RotateAround(transform.position, transform.right, -pos.y * sensitivity);
            transform.RotateAround(transform.position, Vector3.up, pos.x * sensitivity);
        }
    }
}*/

