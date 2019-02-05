using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlanktonHUD : MonoBehaviour
{
    public int badCoinCount;
    public int coinCount;
    public int count;
    public Slider healthBar;
    public Slider sprintBar;
    public bool Powerup = false;
    public Text countText;
    public AudioClip death;
    public AudioClip win;
    public AudioClip pickupSound;
    public AudioClip pickupSoundBad;
    public AudioClip pickupSoundSpecial;


    // Use this for initialization
    void Start()
    {

        badCoinCount = 0;
        coinCount = 0;
        count = 0;
        SetCountText();

         Powerup = false;
}
    // Update is called once per frame
    void LateUpdate()
    {
        if(healthBar.value == 0)
        {
            Debug.Log("you should die here");
            StartCoroutine(Loser());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }
    private void OnCollisionEnter(Collision col)
    {


        
        if (col.gameObject.tag == "NastyPatty")
        {
            //AudioSource.PlayClipAtPoint(pickupSoundBad, this.transform.position);
            badCoinCount++;
            Debug.Log("Bad food = "+badCoinCount);

            if (Powerup == false)
            {
                healthBar.value -= 20;
            }
            if (Powerup == true)
            {
                healthBar.value += 100;

            }
            Debug.Log(Powerup + "lost hp?");

        }


        if (col.gameObject.tag == "Burger")
        {
            //AudioSource.PlayClipAtPoint(pickupSound, this.transform.position);
            coinCount++;
            count = count + 1;
            SetCountText();

            if (healthBar.value + 20 > healthBar.maxValue) { 
                healthBar.value = 100;

            }

        else
        {
                healthBar.value += 20;
                Debug.Log("gain hp");


            }
            if (coinCount >= 20)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("you should win here");
                StartCoroutine(Winner());
            }

        }

        if (col.gameObject.tag == "specialCoin")
        {
            //AudioSource.PlayClipAtPoint(pickupSoundSpecial, this.transform.position);
            healthBar.value = 100;
        }
    }
    void SetCountText ()
    {
        countText.text = "Krabby Patties Stolen: " + count.ToString();

    }

        private IEnumerator PowerUp()
    {
        Powerup = true;
        Debug.Log("timer initiated");

        yield return new WaitForSeconds(5);
        Debug.Log("timer complete");
      
        Powerup = false;

    }

    private IEnumerator Winner()
    {
        AudioSource.PlayClipAtPoint(win, this.transform.position);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameWon");
    }
    private IEnumerator Loser()
    {
        Debug.Log("dead?");
        AudioSource.PlayClipAtPoint(death, this.transform.position);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }
}
