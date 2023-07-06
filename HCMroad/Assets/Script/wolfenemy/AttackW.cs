using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackW : MonoBehaviour
{
    public float attackdelay = 0.3f;
    public bool attackP = false;
    public Animator anim;

    public Collider2D trigger;
    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        trigger.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "player")
        {
            attackP = true;
            trigger.enabled = true;
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
                    trigger.enabled = false;
                }
            }
            
            anim.SetBool("AttackPlayer", attackP);
        }
    }
}
