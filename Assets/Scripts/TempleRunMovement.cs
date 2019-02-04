using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRunMovement : MonoBehaviour
{
    public GameObject cam;
    public GameObject[] legs;
    public GameObject hitbox;
    public float walkSpeed = 10;
    public float sprintSpeed = 5;
    public float jumpHeight = 2.5f;
    public float forwardInput;
    public float turnInput;
    public float sensitivityX = 5f;
    public float RotationsSpeed = 5.0f;
    Rigidbody plankBody;
    Quaternion targetRotate;


    // Use this for initialization
    void Start()
    {
        Debug.Log("PlayersInitiate");
        legs = GameObject.FindGameObjectsWithTag("PlayerLeg");
        hitbox = GameObject.FindGameObjectWithTag("Hitbox");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Cursor.lockState = CursorLockMode.Locked;
        targetRotate = transform.rotation;
        if (GetComponent<Rigidbody>())
        {
            //assigns rigidbody
            plankBody = this.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        UpDown();
        Turn();
        Jump();
        Duck();
    }
   
    void UpDown()
    {
        //rigBody.AddForce(new Vector3(forwardInput, 0, 0), ForceMode.Acceleration);
        var vec = transform.forward * forwardInput * 5;

        plankBody.MovePosition(transform.position + vec * Time.deltaTime);
    }
    void Turn()
    {
        //var vec = transform.right * turnInput * 5;
        //rigBody.velocity = vec;
    }
    void Jump()
    {
        Debug.Log("Jump initate");
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -Vector3.up, .5f + 0.1f))
        {
            plankBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }

    void Duck()
    {
        Debug.Log("duck initiate");
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("crouch");
            while (legs[0].transform.localScale.x >= .25f)
            {
                legs[0].transform.localScale = new Vector3(legs[0].transform.localScale.x - .01f, legs[0].transform.localScale.y, legs[0].transform.localScale.z);
                legs[1].transform.localScale = new Vector3(legs[1].transform.localScale.x - .01f, legs[1].transform.localScale.y, legs[1].transform.localScale.z);
            }
            //legs[0].transform.localScale = new Vector3(.25f, legs[0].transform.localScale.y, legs[0].transform.localScale.z);
            //legs[1].transform.localScale = new Vector3(.25f,legs[1].transform.localScale.y, legs[1].transform.localScale.z);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Debug.Log("stand");
            legs[0].transform.localScale = new Vector3(1f, legs[0].transform.localScale.y, legs[0].transform.localScale.z);
            legs[1].transform.localScale = new Vector3(1f, legs[1].transform.localScale.y, legs[1].transform.localScale.z);
        }

    }
}
