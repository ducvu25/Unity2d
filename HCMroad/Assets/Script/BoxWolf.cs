using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWolf : MonoBehaviour
{
    public GameObject DieEffect;

    public int Health = 100;
    public AudioClip died;
    public AudioSource adisrc;
    void Start()
    {
        adisrc = GetComponent<AudioSource>();
    }
    // Update is called once per frame

    void Update()
    {
        if (Health <= 0)
        {
            adisrc.PlayOneShot(died, 0.7f);
            Instantiate(DieEffect, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }

    void DamageW(int damage)
    {
        
        Health -= damage;
        gameObject.GetComponent<Animation>().Play("redflash");
    }
}
    