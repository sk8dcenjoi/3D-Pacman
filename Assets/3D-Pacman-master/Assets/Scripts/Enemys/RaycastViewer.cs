using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastViewer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(this.GetComponent<Vision>().Glasses.transform.position, -(this.GetComponent<Vision>().Glasses.transform.position - this.GetComponent<Vision>().plankton.transform.position), Color.green);
    }
}
