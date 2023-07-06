using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicontrol : MonoBehaviour {
    public Rigidbody2D r2;
    public Animator anim;
    public float speed = 50f,maxspeed = 2f;
    public GameObject Bomb;
    public Transform BombPoint;
    public float timeF = 2f;
    float time = 1f;
    public bool faceright = true;
    // Use this for initialization
    void Start () {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        //InvokeRepeating("bombfall", 1f, 1f);
        

    }
    void Update()
    { 
        
    }
	// Update is called once per frame
	void FixedUpdate () {
        time -= Time.deltaTime;
        if (time < 0)
        {
            bombfall();
            time = 1f;
        }
        if (faceright)
        {
            r2.AddForce((Vector2.right) * speed);
            if (r2.velocity.x > maxspeed) r2.velocity = new Vector2(maxspeed, r2.velocity.y);

        }
        if (!faceright)
        {
            r2.AddForce((Vector2.left) * speed);
            if (r2.velocity.x < -maxspeed) r2.velocity = new Vector2(-maxspeed, r2.velocity.y);
        }
    }

    public void bombfall()
    {
        if(Bomb & BombPoint)
        {
            Instantiate(Bomb, BombPoint.position, Quaternion.identity);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "wallheliR")
        {
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;

            r2.velocity = new Vector2(0, r2.velocity.y);
            faceright = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        if (collision.GetComponent<Collider2D>().tag == "wallheliL")
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            r2.velocity = new Vector2(0, r2.velocity.y);
            faceright = true;

        }

    }
}
