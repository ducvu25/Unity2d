using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] float dame = 25f;
    [SerializeField] float speed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;
    [SerializeField] float max = 0, min = 0;
    float time;
    float timeTurning;

    bool facingRight = true;
    GameObject player;
    ManagerController managerController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        managerController = GameObject.FindWithTag("Manager").GetComponent<ManagerController>();
        time = Random.Range(3, 5);
        timeTurning = Random.Range(10, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if(managerController.Pause())
            return;
        time -= Time.deltaTime;
        if(time <= 0){
            this.Attack();
            time = Random.Range(3, 5);
        }
        this.Run();
    }
    void Run(){
        if(player.transform.position.x < transform.position.x + 10 && player.transform.position.x > transform.position.x - 10)
            managerController.PlaySound(6);
        float move_x = Time.deltaTime;
        timeTurning -= move_x;
        if(transform.position.x <= min || transform.position.x >= max || timeTurning <= 0){
            timeTurning = Random.Range(10, 20);
            facingRight = !facingRight;
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
        }
        if(facingRight){
            move_x *= speed;
        }else{
             move_x *= -speed;
        }
        //myAnimation.SetBool("Run", true);
        transform.position = transform.position + new Vector3(move_x, 0, 0);
    }
    void Attack(){
        if(facingRight)
            Instantiate(bullet, gun.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        else
            Instantiate(bullet, gun.transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
        
    }
    public float Dame(){
        return dame;
    }
}
