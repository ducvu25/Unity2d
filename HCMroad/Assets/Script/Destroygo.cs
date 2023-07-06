using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroygo : MonoBehaviour
{
    public gamemaster gm;
    public AudioSource adisrc;
    public AudioClip go;
    // Start is called before the first frame update
    void Start()
    {
        adisrc = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<gamemaster>();

    }

    // Update is called once per frame
     void OnTriggerEnter2D(Collider2D col)
    {
        adisrc.PlayOneShot(go, 1f);
        if (col.isTrigger != true && col.CompareTag("Player"))
        {
            Destroy(gameObject);
            gm.points += 1;
            

        }
    }

}