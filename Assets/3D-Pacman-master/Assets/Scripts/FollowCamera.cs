using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform PlayerTransform;

    private Vector3 cameraOffset;
    private Vector3 altCameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public float RotationsSpeed = 5.0f;
    private float viewRange = 30f;
    public float speedH = 2.0f;
    private Transform cameraTransform;
    private Vector3 LookAtMe;
    //Vector3 PreviousPlayerPosition;
    Vector3 newPos;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        cameraOffset = transform.position - PlayerTransform.position;
        altCameraOffset = transform.position - PlayerTransform.position; 
        //PreviousPlayerPosition = PlayerTransform.position;


    }


    // LateUpdate is called after Update methods
    void LateUpdate()
    {
        
        if (RotateAroundPlayer)
        {
            Quaternion camH = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, this.transform.up);
            Quaternion camV = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationsSpeed, -1 * this.transform.right);

            
            cameraOffset = camV * camH * cameraOffset;
            newPos = PlayerTransform.position + cameraOffset;
            //top
            /*
            if (newPos.y - PlayerTransform.position.y > 8.0f || newPos.y - PlayerTransform.position.y < 0.0f)
            {
                altCameraOffset = (new Quaternion(-1, 0, 0, 0) * camV) * cameraOffset;
                cameraOffset = altCameraOffset;
                newPos = PlayerTransform.position + altCameraOffset;
                Debug.Log("bad camera");
                //newPos = transform.position + (PlayerTransform.position - PreviousPlayerPosition);
            }
            */
            //bottom
        }
        
        
        
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        LookAtMe = (PlayerTransform.position + (PlayerTransform.position - transform.position));
        //PlayerTransform.LookAt(new Vector3(LookAtMe.x, PlayerTransform.position.y, LookAtMe.z));

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
        //PreviousPlayerPosition = PlayerTransform.position;
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

