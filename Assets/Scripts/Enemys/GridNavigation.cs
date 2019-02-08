using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNavigation : MonoBehaviour {
    private int gridDimensionX=4;
    private int gridDimensionZ=3;
    private int currentDimensionX;
    private int currentDimensionZ;
    private float moveLength = 0.1f;
    private GameObject target;
    private bool previouslySeen = false;
    public string navpoint;
    private GameObject[,] grid;
    private Vector3[,] initialpositon;
    private GameObject previousTarget;
    public float radius;


    // Use this for initialization
    void Start () {
        grid = new GameObject[gridDimensionX,gridDimensionZ];
        initialpositon = new Vector3[gridDimensionX, gridDimensionZ];
        for (int i = 0; i < gridDimensionX; i++)
        {
            for (int j = 0; j < gridDimensionZ; j++)
            {
                grid[i , j] = GameObject.Find(navpoint + " (" + (3 * i + j) + ")");
                initialpositon[i, j] = grid[i, j].transform.position;
                grid[i, j].transform.Translate(radius, 0, 0);
            }
        }
        target = grid[0, 1];
        currentDimensionX = 0;
        currentDimensionZ = 1;
        shiftTarget();
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Vision>().seen)
        {
            if (!previouslySeen)
            {
                //alert player
            }
            target = GetComponent<Vision>().plankton;
            Debug.Log("Plankton");
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            previouslySeen = true;
        }
        else if (previouslySeen)
        {
            float min = (transform.position - grid[0, 0].transform.position).magnitude;
            int minX = 0;
            int minZ = 0;
            for (int i = 0; i < gridDimensionX; i++)
            {
                for (int j = 0; j < gridDimensionZ; j++)
                {
                    if ((transform.position-grid[i, j].transform.position).magnitude < min)
                    {
                        minX = i;
                        minZ = j;
                    }
                }
            }
            currentDimensionX = minX;
            currentDimensionZ = minZ;
            target = grid[currentDimensionX, currentDimensionZ];
            Debug.Log(currentDimensionX + " " + currentDimensionZ);
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            previouslySeen = false;
        }
        else if((transform.position - grid[currentDimensionX, currentDimensionZ].transform.position).magnitude > 0.1)
        {
            //if (target != previousTarget)
            //{
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                Debug.Log(currentDimensionX + " " + currentDimensionZ);
            //}
        }
        else if ((transform.position - grid[currentDimensionX, currentDimensionZ].transform.position).magnitude < 0.1)
        {
            //find new navpoint
            int move = (int)Random.Range(0, 3.999f);
            switch (move)
            {
                case 0:
                    currentDimensionX = (currentDimensionX + 1) % gridDimensionX;
                    break;
                case 1:
                    currentDimensionZ = (currentDimensionZ + 1) % gridDimensionZ;
                    break;
                case 2:
                    if (currentDimensionX > 0)
                    {
                        currentDimensionX = (currentDimensionX - 1) % gridDimensionX;
                    }
                    break;
                case 3:
                    if (currentDimensionZ > 0)
                    {
                        currentDimensionZ = (currentDimensionZ - 1) % gridDimensionZ;
                    }
                    break;
            }
            target = grid[currentDimensionX, currentDimensionZ];
            //if (target != previousTarget)
            //{
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                Debug.Log(currentDimensionX + " " + currentDimensionZ);
            //}
        }
        //move
        transform.Translate(transform.InverseTransformVector((target.transform.position - transform.position).normalized.x * moveLength * Time.deltaTime, 0, (target.transform.position - transform.position).normalized.z * moveLength * Time.deltaTime));
        //transform.Translate( * moveLength * Time.deltaTime);
        previousTarget = target;
        
        
    }
    private void shiftTarget()
    {
        if (target != GetComponent<Vision>().plankton)
        {
            target.transform.LookAt(initialpositon[currentDimensionX, currentDimensionZ]);
            target.transform.Translate(radius * 1.73f / 2f, 0, radius / 2);
        }
        Invoke("shiftTarget", Random.Range(1,3));
    }
}
