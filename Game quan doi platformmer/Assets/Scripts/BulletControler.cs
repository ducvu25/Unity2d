using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] GameObject player;
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
    public float Dame(){
        // if(type == 0){

        // }
        EnemyController enemyController = player.GetComponent<EnemyController>();
        if(enemyController != null){
            return enemyController.Dame();
        }
        return 0;
    }
}
