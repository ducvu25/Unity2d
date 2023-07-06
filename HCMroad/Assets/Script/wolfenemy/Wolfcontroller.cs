using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolfcontroller : MonoBehaviour
{
    public float attackdelay = 0.3f;
    public Rigidbody2D r2;
    public Animator anim;
    public float speed = 50f, maxspeed = 3;
    public int ourHealth;
    public int maxhealth = 2;
    public bool faceright = true;
    public Collider2D triggerW;
    public bool attackP = false;
    public PlayerController player;
    // Use this for initialization
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        ourHealth = maxhealth;
        triggerW.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        gameObject.GetComponent<Animation>().Play("redflash");
        //anim.SetBool("Ground", Ground);
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //r2.AddForce((Vector2.right) * speed);
        //if (r2.velocity.x > maxspeed && faceright)
        //    r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        //if (r2.velocity.x < -maxspeed && !faceright)
        //    r2.velocity = new Vector2(-maxspeed, r2.velocity.y);
        if (faceright)
        {
            r2.AddForce((Vector2.right) * speed);
            if(r2.velocity.x > maxspeed) r2.velocity = new Vector2(maxspeed, r2.velocity.y);

        }
        if (!faceright)
        {
            r2.AddForce((Vector2.left) * speed);
            if (r2.velocity.x <- maxspeed) r2.velocity = new Vector2(-maxspeed, r2.velocity.y);
        }
    }
    void upspeed(float maxS)
    {
        maxspeed = maxS;
    }
    void Attack(int at)
    {
        if(at == 1)
        {
            attackP = true;
            triggerW.enabled = true;
            attackdelay = 0.3f;

            if (attackP)
            {
                if (attackdelay > 0)
                {
                    attackdelay -= Time.deltaTime;

                }
                else
                {
                    attackP = false;
                    triggerW.enabled = false;
                }
            }
            //player.Knockback(800f, player.transform.position);
            //gameObject.SendMessageUpwards("downheathplayer", 1);
            anim.SetBool("AttackPlayer", attackP);
        }
        else
        {
            attackP = false;
            anim.SetBool("AttackPlayer", attackP);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "wall")
        {
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;

            r2.velocity = new Vector2(0, r2.velocity.y);
            faceright = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        if (collision.GetComponent<Collider2D>().tag == "wallL")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            r2.velocity = new Vector2(0, r2.velocity.y);
            faceright = true;


        }
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("DamageP", 1);
  
        }
        //if (collision.GetComponent<Collider2D>().tag == "player")
        //{
        //    anim.SetBool("AttackPlayer", true);
        //    //maxspeed = 2;
        //    //r2.AddForce((Vector2.right) * speed);
        //    //if (r2.velocity.x > maxspeed) r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        //}
    }
    //void OnCollisionEnter2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<Collider2D>().tag == "player")
    //    {
    //        maxspeed = 2;
    //        r2.AddForce((Vector2.right) * speed);
    //        if (r2.velocity.x > maxspeed) r2.velocity = new Vector2(maxspeed, r2.velocity.y);
    //    }
    //}
    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<Collider2D>().tag == "wall")
    //    {
    //        //gameObject.GetComponent<SpriteRenderer>().flipX = true;
    //        if (faceright) faceright = !faceright;
    //        if (!faceright) faceright = true;
    //    }
    //}
    public void FlipR()
    {
        r2.velocity = new Vector2(0, r2.velocity.y);
        faceright = false;
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void FlipL()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        r2.velocity = new Vector2(0, r2.velocity.y);
        faceright = true;

    }
}
