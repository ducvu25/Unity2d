using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletene : MonoBehaviour {
    public GameObject DieEffect;
    GameObject target;
    Rigidbody2D rb;
    public float speed;
    public float timeToDestroy;
    public int dmg = 1;
    public float vt = 0;
    Vector3 direction;
    // Use this for initialization
    void Start () {
        //m_rb = GetComponent<Rigidbody2D>();
        
        

        //m_rb.rotation = angle;
        //Destroy(gameObject, timeToDestroy);
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float rot_z =( Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg )-90 ;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z +vt);
        Vector2 moveDir = (target.transform.position - transform.position ).normalized * (speed + 2);
      
        rb.velocity = new Vector2(moveDir.x + vt, moveDir.y + vt );

        Destroy(this.gameObject, 5);
    }
	
	// Update is called once per frame
	void Update () {
       
        
    }
    private void FixedUpdate()
    {
        //rb.velocity = direction * speed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Ground")
        {
            Instantiate(DieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            Instantiate(DieEffect, transform.position, Quaternion.identity);
            collision.SendMessageUpwards("DamageP", 1);
            Destroy(gameObject);
        }
    }

    }
