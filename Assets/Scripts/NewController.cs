using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewController : MonoBehaviour {
    public GameObject cam;
    public GameObject[] legs;
    public GameObject hitbox;
    public float walkSpeed = 10;
    public float sprintSpeed = 5;
    public float jumpHeight=2.5f;
    public float forwardInput;
    public float turnInput;
    public float sensitivityX = 5f;
    public float RotationsSpeed = 5.0f;
    Rigidbody rigBody;
    Quaternion targetRotate;


	// Use this for initialization
	void Start () {
        Debug.Log("PlayersInitiate");
        legs = GameObject.FindGameObjectsWithTag("PlayerLeg");

        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Cursor.lockState = CursorLockMode.Locked;
        targetRotate = transform.rotation;
        if (GetComponent<Rigidbody>())
        {
            rigBody = GetComponent<Rigidbody>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        UpDown();
        Turn();
        Jump();
        Duck();
	}
    void LateUpdate()
    {

        float lh = Input.GetAxisRaw("Horizontal");


        var newRotation = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z);


        if (lh != 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newRotation), RotationsSpeed * sensitivityX * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            this.GetComponent<Rigidbody>().MoveRotation(transform.rotation);
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    void UpDown()
    {
        //rigBody.AddForce(new Vector3(forwardInput, 0, 0), ForceMode.Acceleration);
        Debug.Log("forward initate");
        var vec = transform.forward * forwardInput * 5;
      // vec.y = rigBody.velocity.y;
       //rigBody.velocity = vec;
    }
    void Turn()
    {
        var vec = transform.right * forwardInput * 5;
    }
    void Jump()
    {
        Debug.Log("Jump initate");
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -Vector3.up, .5f + 0.1f))
        {
            rigBody.AddForce(new Vector3(0,jumpHeight,0), ForceMode.Impulse);
        }
    }

    void Duck()
    {
        Debug.Log("duck initiate");
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("crouch");
            while (legs[0].transform.localScale.x >= .25f){
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
