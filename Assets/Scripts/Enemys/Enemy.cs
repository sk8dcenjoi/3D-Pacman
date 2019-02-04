using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public string planktonTag;
    public Vector3 movementLength;

    private Vector3 previousLocation;

	// Use this for initialization
	void Start () {
        previousLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        dropBombs();
	}
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(planktonTag))
        {
            //alter for other scripts
            /*
            if (other.gameObject.GetComponent<PlanktonScript>().isMega())
            {
                //something happens to enemy
            }
            else
            {
                //planton lose heath
            }
            */
        }
    }
    private void dropBombs()
    {
        if ((previousLocation-transform.position).magnitude > movementLength.magnitude)
        {
            //Instantiate(Bomb, transform.position, new Quaternion());
        }
    }
}
