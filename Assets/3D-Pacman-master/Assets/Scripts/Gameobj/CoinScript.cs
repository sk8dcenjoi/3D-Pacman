using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CoinScript : MonoBehaviour


{
    public float speed;
    public float CoinSpeed = 10f;
    public GameObject Player;
    private void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        this.gameObject.transform.Rotate(0, CoinSpeed, 0);

        if (this.transform.tag == "specialCoin" )
        {
            if (this.transform.localPosition.y <= 1)
            {

                this.GetComponent<Rigidbody>().AddForce(0, 30, 0);
            }
            if (this.transform.localPosition.y >= 2.5)
            {
                this.GetComponent<Rigidbody>().AddForce(0, -30, 0);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("coincollision" +other.gameObject.name);


        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);

        }
    }

}
