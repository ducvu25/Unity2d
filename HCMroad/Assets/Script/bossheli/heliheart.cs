using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heliheart : MonoBehaviour
{
    public Sprite[] Heartsprite;

    public bosshelicontrol boss;

    public Image Heart;
    
    // Use this for initialization
    void Start()
    {  
        
        boss = GameObject.FindGameObjectWithTag("bossheli").GetComponent<bosshelicontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.ourHealth > 15)
            boss.ourHealth = 15;


        if (boss.ourHealth < 0)
            boss.ourHealth = 0;
        if(boss.ourHealth <= 15 && boss.ourHealth >= 13) gameObject.GetComponent<SpriteRenderer>().sprite = Heartsprite[5];
        if (boss.ourHealth <= 12 && boss.ourHealth >= 10) gameObject.GetComponent<SpriteRenderer>().sprite = Heartsprite[4];
        if (boss.ourHealth <= 9 && boss.ourHealth >= 7) gameObject.GetComponent<SpriteRenderer>().sprite = Heartsprite[3];
        if (boss.ourHealth <= 6 && boss.ourHealth >= 4) gameObject.GetComponent<SpriteRenderer>().sprite = Heartsprite[2];
        if (boss.ourHealth <= 3 && boss.ourHealth >= 0) gameObject.GetComponent<SpriteRenderer>().sprite = Heartsprite[1];




    }
}
