using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public string planktonTag;
    public float movementLength;
    public GameObject Bomb;
    private Vector3 previousLocation;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        previousLocation = transform.position;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        dropBombs();
	}
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(planktonTag))
        {
            if (other.gameObject.GetComponent<PlanktonHUD>().Powerup)
            {
                rb.AddForce(new Vector3(0, 200, 0));
            }
        }
    }
    private void dropBombs()
    {
        if ((previousLocation-transform.position).magnitude > movementLength)
        {
            //Instantiate(Bomb, new Vector3(transform.position.x+1,transform.position.y+1,transform.position.z), new Quaternion());
            Instantiate(Bomb, transform.position - (transform.position - previousLocation).normalized/1.5f + new Vector3(0,0.5f,0), new Quaternion());
            previousLocation = transform.position;
        }
    }
}
