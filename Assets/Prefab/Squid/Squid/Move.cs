using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    Animator anim;
    public float speed;
    public GameObject head;
    bool walking;
    bool left;

    // Use this for initialization
    void Start()
    {
        head.transform.Rotate(0, 3 * Time.deltaTime,0 , Space.World);
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //This deactivates headturn while chasing
        if (walking == false)
        {
            if (head.transform.rotation.z > .25f)
            {
                left = false;
            }
            if (head.transform.rotation.z < (-.25f))
            {
                left = true;
            }

            if (left)
            {
                head.transform.Rotate(0, 20 * Time.deltaTime, 0, Space.World);
            }
            else
            {

                head.transform.Rotate(0, -20 * Time.deltaTime, 0, Space.World);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            walking = true;
            anim.SetBool("Walking", true);
            if (Input.GetKey(KeyCode.UpArrow))
            {

                anim.speed = 1;
                anim.SetFloat("Direction", 1);
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetFloat("Direction", -1);
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
        else
        {
            anim.SetBool("Walking", false);
            walking = false;
        }
    }
}
