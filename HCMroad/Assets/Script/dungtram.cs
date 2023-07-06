using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungtram : MonoBehaviour
{
    public gamemaster gm;
    public GameObject leu;
    public PlayerController pl;
   
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<gamemaster>();
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true && col.CompareTag("Player") && gm.points>= 10)
        {
            Destroy(gameObject);
            gm.points = gm.points - 10;
            leu.SetActive(true);
            pl.heal();
        }
    }
}
