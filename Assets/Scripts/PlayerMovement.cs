using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Cam;
    public float WalkSpeed = 10;
    public float RotationsSpeed = 5.0f;
    public float sensitivityX = 5f;
    float p_WalkLR;
    float p_WalkFB;


    void Start()
    {
        Cam = GameObject.FindGameObjectWithTag("Main Camera");
        Cursor.lockState = CursorLockMode.Locked;

    }

    void LateUpdate()
    {

        float lh = Input.GetAxisRaw("Horizontal");


        var newRotation = new Vector3(Cam.transform.eulerAngles.x, Cam.transform.eulerAngles.y, transform.eulerAngles.z);


        if (lh != 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newRotation), RotationsSpeed * sensitivityX * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            this.GetComponent<Rigidbody>().MoveRotation(transform.rotation);
        }

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
    }


    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.W))
        {
                       transform.Translate(Vector3.forward * WalkSpeed * Time.deltaTime);
           // transform.Translate(new Vector3(transform.position.x - Cam.transform.position.x, 0, transform.position.z - Cam.transform.position.z).normalized * WalkSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
                       transform.Translate(-Vector3.forward * WalkSpeed * Time.deltaTime);
            //transform.Translate(-1 * new Vector3(transform.position.x - Cam.transform.position.x, 0, transform.position.z - Cam.transform.position.z).normalized * WalkSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * WalkSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * WalkSpeed * Time.deltaTime);
            
        }
        /*if (Input.GetKey(KeyCode.LeftControl))
        {
            
            while (transform.localScale.y > .5f)
            {
                transform.localScale += new Vector3(0, -.00001f, 0);
            }
        }*/
    }
}