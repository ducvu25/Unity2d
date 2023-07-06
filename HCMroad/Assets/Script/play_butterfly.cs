using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class play_butterfly : MonoBehaviour
{
   
    public Rigidbody2D butterfly;
    public Transform bfly;// tọa độ của butterfly
    public float speed = 6f, maxspeed = 2f;
    public bool faceright = true;
    float pR = 0, pL=0;
    // Use this for initialization
    void Start()
    {
        butterfly = gameObject.GetComponent<Rigidbody2D>();
        pR = bfly.position.x + 10;
        pL = bfly.position.x - 10;
    }
   
    void Update()
    {
        if (bfly.position.x <= pL)//nếu tọa độ x <=700 quay ngược lại
        {
            butterfly.velocity = new Vector2(0, butterfly.velocity.y);
            faceright = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            if (bfly.position.x >= pR)//nếu tọa độ x >=860 quay ngược lại
            {
                butterfly.velocity = new Vector2(0, butterfly.velocity.y);
                faceright = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (faceright)
        {
            butterfly.AddForce((Vector2.right) * speed);
            if (butterfly.velocity.x > maxspeed) butterfly.velocity = new Vector2(maxspeed, butterfly.velocity.y);

        }
        if (!faceright)
        {
            butterfly.AddForce((Vector2.left) * speed);
            if (butterfly.velocity.x < -maxspeed) butterfly.velocity = new Vector2(-maxspeed, butterfly.velocity.y);

        }

    }
}
