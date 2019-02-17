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
    public AudioSource mainAudio;
    public bool canPlay = true;
    public AudioClip death;
    public AudioClip win;
    public AudioClip pickupSound;
    public AudioClip pickupSoundBad;
    public AudioClip pickupSoundSpecial;
    public AudioClip Idle;
    private Animator animator;
    private float newcount = 0;


    // Use this for initialization
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
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
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == -178524345 && newcount == 0f)
        {
            newcount = 1f;
            StartCoroutine("IdleMethod");
        }
    }
    private void OnTriggerEnter(Collider col)
    {



        if (col.gameObject.tag == "NastyPatty")
        {
			if(canPlay && healthBar.value != 0)
            {
                mainAudio.clip = pickupSoundBad;
                mainAudio.Play();
                canPlay = false;
                StartCoroutine(SoundPlayer());
            }
            //AudioSource.PlayClipAtPoint(pickupSoundBad, this.transform.position);
            badCoinCount++;
            Debug.Log("Bad food = " + badCoinCount);

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
            if (canPlay && coinCount != 20)
            {
                mainAudio.clip = pickupSound;
                mainAudio.Play();
                canPlay = false;
                StartCoroutine(SoundPlayer());
            }
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
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            if(Powerup == true)
            {
                healthBar.value += 0;
            }
            else
            {
                healthBar.value -= 10;
            }
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
		mainAudio.clip = win;
		mainAudio.Play ();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameWon");
    }
    private IEnumerator Loser()
    {
        Debug.Log("dead?");
		mainAudio.clip = death;
		mainAudio.Play ();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }
    private IEnumerator IdleMethod()
    {
        yield return new WaitForSeconds(15);
        newcount = 0f;
        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == -178524345)
        {
            mainAudio.clip = Idle;
            mainAudio.Play();
        }
    }
    private IEnumerator SoundPlayer()
    {
        yield return new WaitForSeconds(5);
        canPlay = true;
    }
}
