using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sound : MonoBehaviour {
    public AudioClip one;
    public AudioClip two;
    public AudioClip three;
    private float count = 0;
    private int selector = 7;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (count == 0){
            StartCoroutine("PlaySound");
            count = 1;
        }
	}

    private IEnumerator PlaySound()
    {
        selector = Random.Range(0, 2);
        yield return new WaitForSecondsRealtime(10);
        if (selector == 0)
        {
            AudioSource.PlayClipAtPoint(one, this.transform.position);
        }
        else if (selector == 1)
        {
            AudioSource.PlayClipAtPoint(two, this.transform.position);
        }
        else
            AudioSource.PlayClipAtPoint(three, this.transform.position);
        count = 0;
    }
}
