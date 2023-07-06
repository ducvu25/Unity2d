using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public Rigidbody2D r2;
    public Animator anim;
    Transform player;
    float time = 1f;
    public Transform shootpoint;
    public GameObject bullet;
    public bool faceright = true;
    public float speed = 50f, maxspeed = 1.5f;
    public AudioClip  m249;
    public AudioSource adisrc;
    // Use this for initialization
    void Start () {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        adisrc = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", Mathf.Abs(r2.velocity.x));
    }
    void FixedUpdate()
    {
        if (faceright)
        {
            //r2.AddForce((Vector2.right) * speed);
            //if (r2.velocity.x > maxspeed) r2.velocity = new Vector2(maxspeed, r2.velocity.y);

        }
        if (!faceright)
        {
            //r2.AddForce((Vector2.left) * speed);
            //if (r2.velocity.x < -maxspeed) r2.velocity = new Vector2(-maxspeed, r2.velocity.y);
        }
      
    }
    void upspeed(float maxS)
    {
        maxspeed = maxS;
    }
    void Shootene()
    {
        adisrc.PlayOneShot(m249, 1f);
        anim.SetBool("Attacking", true);
        if (bullet && shootpoint)
        {
            Instantiate(bullet, shootpoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            if (player.transform.position.x > transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                
            
                

            }
            if(player.transform.position.x < transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            time -= Time.deltaTime;
            if (time < 0)
            {
                Shootene();
                time = 1f;
            }

        }
    }
    

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            anim.SetBool("Attacking", false);
        }

    }
    //{
    //    if (collision.GetComponent<Collider2D>().tag == "zoneEnemy")
    //    {
    //        //gameObject.GetComponent<SpriteRenderer>().flipX = true;

    //        r2.velocity = new Vector2(0, r2.velocity.y);
    //        faceright = false;
    //        gameObject.GetComponent<SpriteRenderer>().flipX = true;

    //    }
    //}
}
