using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_wall : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D ob)
    {
        if (ob.gameObject.tag == "bomb")// vật chạm là bomb
        {
            Destroy(ob.gameObject);// biến mất

        }
    }
}
