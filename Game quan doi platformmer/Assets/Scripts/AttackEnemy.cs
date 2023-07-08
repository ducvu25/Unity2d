using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    Transform parent; 
    EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent;
        enemyController = parent.gameObject.GetComponent<EnemyController>();
        if(enemyController.Type() == 0){
            gameObject.GetComponents<Collider2D>()[0].enabled = true;
            gameObject.GetComponents<Collider2D>()[1].enabled = false;
        }else{
            gameObject.GetComponents<Collider2D>()[1].enabled = true;
            gameObject.GetComponents<Collider2D>()[0].enabled = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player"){
            enemyController.Attack();
        }
    }
}
