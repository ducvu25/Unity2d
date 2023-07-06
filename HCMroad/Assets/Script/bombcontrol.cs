using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombcontrol : MonoBehaviour {
    public GameObject execEffect;
    Rigidbody2D m_rb;
    public int dmg = 2;
    public Animator anim;
    bool exec=false;
    // Use this for initialization
    void Start () {
        m_rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        Destroy(gameObject, 7f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Ground")
        {
            Instantiate(execEffect, transform.position, Quaternion.identity);
            Destroy(gameObject, 0);
        }
        if (collision.isTrigger != true && collision.CompareTag("Player"))
        {
            Instantiate(execEffect, transform.position, Quaternion.identity);
            collision.SendMessageUpwards("DamageP", dmg);
            Destroy(gameObject, 0);
        }

    }
}
