using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewController : MonoBehaviour {
    public GameObject cam;
    public GameObject[] legs;
    public GameObject hitbox;
    public bool flyActive;
    public float walkSpeed = 10;
    public float sprintSpeed = 5;
    public float jumpHeight=7f;
    public float forwardInput;
    public float turnInput;
    public float verticalInput;
    public float sensitivityX = 5f;
    public float RotationsSpeed = 5.0f;
    Rigidbody plankBody;
    Quaternion targetRotate;


	// Use this for initialization
	void Start () {

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
	void Update () {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (flyActive == false)
        {
            UpDown();
            Jump();
            Duck();
        }
        if (flyActive)
        {
            Fly();


        }
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
        var vec = transform.forward * forwardInput * 5 + transform.right * turnInput * 5;

        plankBody.MovePosition (transform.position + vec * Time.deltaTime);
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
            plankBody.AddForce(new Vector3(0,jumpHeight,0), ForceMode.Impulse);
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
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "specialCoin")
        {

            StartCoroutine(AbilityList(0));
            Debug.Log("atecoin");
        }
    }
    private void Fly()
    {
        plankBody.useGravity = false;
        var vec = transform.forward * forwardInput * 5 + transform.right * turnInput * 5 + transform.up * verticalInput *5;

        plankBody.transform.position = transform.position + transform.up * 5 * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("upvector");
        }
        plankBody.MovePosition(transform.position + vec * Time.deltaTime);
    }


    private IEnumerator AbilityList(int num)
    {
        if (num == 0)
        {
            Debug.Log("Fly countdown");
            flyActive = true;
            yield return new WaitForSeconds(10f);
            
            flyActive = false;
            Debug.Log("fly over");
            plankBody.useGravity = true;

            yield return new WaitForSeconds(2f);

        }
    }
}
