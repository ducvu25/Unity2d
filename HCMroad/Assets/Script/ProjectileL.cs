using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileL : MonoBehaviour {
    public GameObject DieEffect;
    Rigidbody2D m_rb;
    public float speed;
    public float timeToDestroy;
    public int dmg = 50;
    // Use this for initialization
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, timeToDestroy);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_rb.velocity = Vector2.left * speed;

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(DieEffect, transform.position, Quaternion.identity);
        if (collision.GetComponent<Collider2D>().tag == "Ground")
        {
            Destroy(gameObject, 0);
        }

        if (collision.isTrigger != true && collision.CompareTag("Wolf"))
        {
            collision.SendMessageUpwards("DamageW", dmg);
            Destroy(gameObject, 0);
        }
        if (collision.isTrigger != true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("Damage", dmg);
            Destroy(gameObject, 0);
        }
        if (collision.isTrigger != true && collision.CompareTag("bossheli"))
        {
            Instantiate(DieEffect, transform.position, Quaternion.identity);
            collision.SendMessageUpwards("Damageboss", 1);
            Destroy(gameObject);
        }
    }
}
