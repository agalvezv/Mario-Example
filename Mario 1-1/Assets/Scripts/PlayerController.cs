using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool facingRight;
    private bool GoombaDeath;
    private bool playerHit;
    public float speed;
    public float jump;
    public AudioClip CoinSound;
    public AudioClip JumpSound;
    public Text CountText;
    public Text WinText;
    AudioSource aScorce;
    GameObject obj;
    GameObject JumpSoundObj;
    int CoinCount;

	void Start (){

        CoinCount = 0; //Sets coin counter
        rb2d = GetComponent<Rigidbody2D>(); //Get RigidBody
        obj = GameObject.Find("CoinSound"); //Get Coin Sound Effect
        JumpSoundObj = GameObject.Find("JumpSound"); //Get Jump Sound Effect
        if (obj != null) 
            aScorce = obj.GetComponent<AudioSource>();
        CountText.text = "x " + CoinCount.ToString ();
        WinText.text = "";
	}
	

	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        //Move Code
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);
        GetComponent<Animator>().SetBool("MarioWalking", true);

        //Flipping Code
        if (facingRight == true && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == false && moveHorizontal < 0)
        {
            Flip();
        }



    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && playerHit == true)
        {
            Debug.Log("Goomba dead");
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                aScorce.clip = JumpSound;
                aScorce.Play();
                GetComponent<Animator>().SetBool("MarioJump", true);
                rb2d.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            aScorce.clip = CoinSound;
            aScorce.Play();
            CoinCount = CoinCount + 1;
            other.gameObject.SetActive(false);
            CountText.text = "x " + CoinCount.ToString();
        }

        if (other.gameObject.CompareTag("CoinBox"))
        {
            aScorce.clip = CoinSound;
            aScorce.Play();
            CoinCount = CoinCount + 1;
            other.gameObject.SetActive(false);
            CountText.text = "x " + CoinCount.ToString();
        }

        if (other.gameObject.CompareTag("Flag"))
        {
            aScorce.clip = CoinSound;
            aScorce.Play();
            WinText.text = "You Win! You Collected " + CoinCount.ToString() + " Coins!";
        }

        if (other.gameObject.CompareTag("Goomba"))
        {
            aScorce.clip = CoinSound;
            aScorce.Play();
            CoinCount = CoinCount + 1;
            other.gameObject.SetActive(false);
            CountText.text = "x " + CoinCount.ToString();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
