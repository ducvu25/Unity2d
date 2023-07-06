using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Wolfcontroller wolf;
    public float speed = 50f, maxspeed = 3, jumpPow = 300f;
    public bool Ground = true, faceright = true, doublejump = false;
    public GameObject Projectile;
    public GameObject ProjectileL;
    public Transform shootingPoint;
    public int ourHealth;
    public int maxhealth = 3;
    public float timedownheart = 1f;
    public Rigidbody2D r2;
    public Animator anim;
    public AudioClip m416;
    public AudioSource adisrc;
    public Text text;
    public int firstair = 99;
    bool SP = true;
    public float delayJump = 0.3f;
    Vector3 save;
    // Use this for initialization
    void Start()
    {
        wolf = gameObject.GetComponent<Wolfcontroller>();
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        ourHealth = maxhealth;
        adisrc = GetComponent<AudioSource>();
        save = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = firstair.ToString();
        anim.SetBool("Ground", Ground);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));
        delayJump -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Ground)
            {
                Ground = false;
                doublejump = true;
                r2.AddForce(Vector2.up * jumpPow);
                delayJump = 0.3f;

            }

            else
            {
                if (doublejump)
                {
                    doublejump = false;
                    r2.velocity = new Vector2(r2.velocity.x, 0);
                    r2.AddForce(Vector2.up * jumpPow );
                    delayJump = 0.3f;
                }
            }
        }
        if (Input.GetKeyDown("f"))
        {
            heal();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        r2.AddForce((Vector2.right) * speed * h);

        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);

        if (h > 0 && !faceright)
        {
            FlipL();
        }

        if (h < 0 && faceright)
        {
            FlipR();
        }

        if (Ground)
        {
            r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
        }

        if (ourHealth <= 0)
        {
            Death();
        }
       if(ourHealth <= 2)
        {
            heal();
        }
        if(transform.position.y < -11){
            anim.SetBool("Die", true);
            Invoke("Death", 1f);
        }
    }
    public void heal()
    {
        firstair--;
        ourHealth = maxhealth;
    }
    public void FlipR()
    {
        faceright = false;
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void FlipL()
    {
        faceright = true;
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }
    void downheathplayer(int heath)
    {
  
        ourHealth = ourHealth - heath;
    }
    public void Death()
    {
        transform.position = save;
        ourHealth = maxhealth;
        anim.SetBool("Die", false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Shoot() {
        adisrc.PlayOneShot(m416, 1f);
        if ( shootingPoint &&faceright)
        {
            Instantiate(Projectile, shootingPoint.position, Quaternion.identity);
        }
        else
        {
            Instantiate(ProjectileL, shootingPoint.position, Quaternion.identity);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "wall")
        {
            collision.SendMessageUpwards("upspeed", 2);
        }
        if (collision.GetComponent<Collider2D>().tag == "wallL")
        {
            collision.SendMessageUpwards("upspeed", 2);

        }
        if (collision.GetComponent<Collider2D>().tag == "Water")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        if (collision.GetComponent<Collider2D>().tag == "Wolf")
        {
            collision.SendMessageUpwards("Attack", 1);
            ourHealth -= 1;
            //downheathplayer(1);
            //if (ourHealth <= 3)
            //{
            //    ourHealth = 5;
            //}
        }
        if (collision.GetComponent<Collider2D>().tag == "supplystation")
        {
            if (SP)
            {
                firstair++;
                ourHealth = maxhealth;
                SP = false;
            }
            //downheathplayer(1);
            //if (ourHealth <= 3)
            //{
            //    ourHealth = 5;
            //}
        }
        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Wolf")
        {
            collision.SendMessageUpwards("Attack", 1);
            
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Wolf")
        {
            collision.SendMessageUpwards("Attack", -1);

        }
    }
    void DamageP(int damage)
    {
        ourHealth -= damage;
        gameObject.GetComponent<Animation>().Play("redflash");
    }
    public void Knockback(float Knockpow, Vector2 Knockdir)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(Knockdir.x * -100, Knockdir.y * Knockpow));
    }
    public void Save(){
        save  =transform.position;
        Debug.Log("Save");
    }
}