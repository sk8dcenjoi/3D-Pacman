using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Abilities : MonoBehaviour
{

    private bool shrinkActive;
    public float shrinkSize;
    private bool growActive;
    public float growSize;
    public float rateOfGrowth;
    public float size;

    // Use this for initialization
    void Start()
    {
        size = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (shrinkActive)
        {
            Shrink();
        }

        if (growActive)
        {
            Grow();
        }
    }

    private void Shrink()
    {
        if (transform.localScale.x >= shrinkSize)
        {
            transform.localScale -= new Vector3(rateOfGrowth, rateOfGrowth, rateOfGrowth);
        }
    }

    private void Grow()
    {
        if (transform.localScale.x <= growSize)
        {
            transform.localScale += new Vector3(rateOfGrowth, rateOfGrowth, rateOfGrowth);
        }
    }


    void OnGUI()
    {
        Event e = Event.current;
        switch (e.keyCode)
        {
            case KeyCode.Alpha1:
                Debug.Log("Detected key code: " + e.keyCode);
                StartCoroutine("Resize", 0);
                break;
            case KeyCode.Alpha2:
                Debug.Log("Detected key code: " + e.keyCode);
                StartCoroutine("Resize", 1);
                break;
            default:
                Debug.Log("Detected key code: " + e.keyCode);
                break;

        }
    }

    private IEnumerator Resize(int num)
    {

        if (num == 0)
        {
            shrinkActive = true;
            yield return new WaitForSeconds(2);
            shrinkActive = false;
            yield return new WaitForSeconds(2f);

        }

        if (num == 1)
        {
            growActive = true;
            yield return new WaitForSeconds(3f);
            growActive = false;
            yield return new WaitForSeconds(2f);
        }

    }
}
