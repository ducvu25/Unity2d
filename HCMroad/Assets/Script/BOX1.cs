using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOX1 : MonoBehaviour
{
    public int Health = 100;
    public GameObject DieEffect;
    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Instantiate(DieEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    void Damage(int damage)
    {
        Health -= damage;
    }
}
