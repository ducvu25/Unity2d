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
        }else if(enemyController.Type() == 1){
            gameObject.GetComponents<Collider2D>()[1].enabled = true;
            gameObject.GetComponents<Collider2D>()[0].enabled = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player"){
            if(enemyController.Type() == 2){
                if((parent.position.x - other.gameObject.transform.position.x > 0 && parent.position.x - other.gameObject.transform.position.x > 1.2f)
                || (parent.position.x - other.gameObject.transform.position.x < 0 && parent.position.x - other.gameObject.transform.position.x < -1.2f)){
                    enemyController.SetFace();
                    return;
                }
            }
            enemyController.Attack();
        }
    }
}
