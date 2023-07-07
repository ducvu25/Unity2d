using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    // Start is called before the first frame update
    Rigidbody2D myBody;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 directionPlus90 = Quaternion.Euler(0, 0, 0) * transform.right;
        myBody.AddForce(directionPlus90 * speed);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
