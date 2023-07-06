using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public PlayerController player;

    // Use this for initialization
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground"))
            player.Ground = true;    
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Ground" && player.delayJump < 0) player.Ground = true;
        if (collision.GetComponent<Collider2D>().tag == "save") player.Save();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.GetComponent<Collider2D>().tag == "Ground") player.Ground = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().tag == "Ground") player.Ground = false;
    }
}
