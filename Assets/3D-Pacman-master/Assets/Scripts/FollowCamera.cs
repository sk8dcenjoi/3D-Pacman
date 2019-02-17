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
	private Vector3 currentRotation;
    //Vector3 PreviousPlayerPosition;
    Vector3 newPos;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        cameraOffset = transform.position - PlayerTransform.position;
        altCameraOffset = transform.position - PlayerTransform.position; 
		currentRotation = transform.rotation.eulerAngles;
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
		transform.position = new Vector3 (transform.position.x, Mathf.Clamp (transform.position.y, 1f, 6f), transform.position.z);
        LookAtMe = (PlayerTransform.position + (PlayerTransform.position - transform.position));
        //PlayerTransform.LookAt(new Vector3(LookAtMe.x, PlayerTransform.position.y, LookAtMe.z));

        if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
        //PreviousPlayerPosition = PlayerTransform.position;
    }
}

