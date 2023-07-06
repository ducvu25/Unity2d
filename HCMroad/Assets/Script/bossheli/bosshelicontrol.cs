using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosshelicontrol : MonoBehaviour
{
    public GameObject DieEffect;
    public AudioClip m416;
    public AudioSource adisrc;
    //public Transform canon;
    public float minDistance, bulletSpeed = 4f;
    public GameObject bullet;
    public GameObject bullet1;
    public GameObject bullet2;
    public Transform shootingPoint;
    Transform player;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    Vector3 direction;
    public float time = 1f,timemove = 3f;
    public int ourHealth;
    public int maxhealth = 15;
    Vector3 move;
    // Use this for initialization
    void Start()
    {
        move = this.transform.position;
        ourHealth = maxhealth;
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        adisrc = GetComponent<AudioSource>();
        
    }
    private void moveboss()
    {
        rb.AddForce(Vector2.up * 0.5f);
        //move.y += 1f;
        //this.transform.position = move;
        timemove -= Time.deltaTime;
        if (timemove < 0)
        {
            timemove = 3f;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1f);
            //rb.AddForce(rb.velocity.x , );
        }
    }
    // Update is called once per frame
    void Update()
    {
        moveboss();
        direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float rot_z = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)-180;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        //rb.rotation = angle;
        //direction.Normalize();
        //movement = direction;

    }
    void Damageboss(int damage)
    {
        ourHealth -= damage;
        gameObject.GetComponent<Animation>().Play("redflash");
    }
    void FixedUpdate()
    {
        //InvokeRepeating("Shootheli", 0.5f, 0.5f);
        //Shootheli();
        if (ourHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Instantiate(DieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Shootheli()
    {
        if (bullet && shootingPoint)
        {
            adisrc.PlayOneShot(m416, 1f);
            Instantiate(bullet, shootingPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            Instantiate(bullet1, shootingPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            Instantiate(bullet2, shootingPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();

        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    //public void Shoot()
    //{
    //    if (bullet && shootingPoint)
    //    {
    //        Instantiate(bullet, shootingPoint.position, Quaternion.identity);
    //    }
    //}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            //InvokeRepeating("Shootheli", 0.5f, 0.5f);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                Shootheli();
                time = 1f;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            //time -= Time.deltaTime;
            //if (time < 0)
            //{
            //    Shootheli();
            //    time = 1f;
            //}
        }
    }
}
