using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

    //'bool seen' determines enemy behavior
    public bool seen = false;
    public GameObject plankton;
    public string planktonTag;
    public float jumpHeight = 2;

    //vision center must be child of the enemy.  x= 0.  y set to floor level and z = glasses.z + (enemy.y + glasses.y)/1.73
    public GameObject VisionCenter;

    //Glasses must be a child of the enemy slightly infront (z) and above the enemy (y) (x=0).  Currently set to 1.5x enemy height (total height, not y coordinate)
    public GameObject Glasses;

    private RaycastHit hit;
    
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Physics.Raycast(Glasses.transform.position, -(Glasses.transform.position-plankton.transform.position), out hit))
        {
            if (hit.collider.CompareTag(planktonTag))
            {
                Vector3 targetDir = plankton.transform.position - Glasses.transform.position;
                Vector3 coneCenter = VisionCenter.transform.position - Glasses.transform.position;
                float angle = Vector3.Angle(targetDir, coneCenter);

                if (angle < 30.0f || (plankton.transform.position.y > jumpHeight && targetDir.magnitude < 20))
                {
                    seen = true;
                    Debug.Log("      SEEN");
                }
                else
                {
                    seen = false;
                    Debug.Log("Unseen: Not In Cone");
                }
            }
            else
            {
                seen = false;
                Debug.Log("Unseen: Not Plankton");
            }
        }
        else
        {
            Debug.Log("Nothing Hit");
        }
    }
}
