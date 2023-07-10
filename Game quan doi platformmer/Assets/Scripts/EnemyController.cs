using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float dame = 100f;
    [SerializeField] float maxHp = 100f;
    [SerializeField] float distane = 1f;
    [SerializeField] bool facingRight = true;
    [SerializeField] int type = 0;
    [SerializeField] Slider sldHp;
    [SerializeField] GameObject canvasHp;
    [SerializeField] GameObject effectBlood;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletTransform;
    [SerializeField] GameObject attackCls;

    Rigidbody2D myBody;
    Animator myAnimation;
    GameObject player;
    GameObject manager;
    ManagerController managerController;
    float delayRun;
    float delayIdle = 0;
    float mSpeed;
    float mHp;
    Vector3 index;
    float[] timeSpwam = {0.7f, 0.7f, 0.65f};
    float[] m_timeSpwam = {0, 0, 0};
    bool attack = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mSpeed = maxSpeed;
        mHp = maxHp;
        sldHp.maxValue = maxHp;
        sldHp.value = maxHp;
        delayRun = Random.Range(0.2f, 1f);
        //delayIdle = Random.Range(0.2f, 1f);
        canvasHp.SetActive(false);
        myBody = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
        index = transform.position;
        player = GameObject.FindWithTag("Player");
        manager = GameObject.FindWithTag("Manager");
        managerController = manager.GetComponent<ManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(managerController.Pause())
            return;
        if(mHp <= 0)
            return;
        for(int i = 0; i < timeSpwam.Length; i++) {
            if(m_timeSpwam[i] < 0){
                m_timeSpwam[i] = 0;
                continue;
            }
            m_timeSpwam[i] -= Time.deltaTime;
        }
        if(attack)
            return;
        delayRun -= Time.deltaTime;
        if(delayRun > 0)
            this.Run();
        else {
            delayIdle -= Time.deltaTime;
            if(delayIdle < 0){
                delayRun = Random.Range(0.2f, 1f);
                delayIdle = Random.Range(0.2f, 1f);
            }else{
                this.Idle();
            }
        }
        //this.Attack();
    }
    void Idle(){
        if(type != 2)
            myAnimation.SetBool("Run", false);
        myAnimation.SetInteger("Attack", 0);
        attack = false;
    }
    void Run(){
        float move_x = Time.deltaTime;
        if(transform.position.x > index.x + distane || transform.position.x < index.x - distane){
            // if(facingRight){
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            // }
            // facingRight = false;
            facingRight = !facingRight;
        }
        if(facingRight){
            move_x *= mSpeed;
        }else{
             move_x *= -mSpeed;
        }
        if(type != 2)
            myAnimation.SetBool("Run", true);
        transform.position = transform.position + new Vector3(move_x, 0, 0);
    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "BulletPlayer" && mHp > 0){
            PlayerController playerController = player.GetComponent<PlayerController>();
            mHp -= playerController.Dame();
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            if(mHp <= 0){
                myAnimation.SetBool("Die", true);
                Invoke("SetActive", 2f);
            }else{
                canvasHp.SetActive(true);
                sldHp.value = mHp;
                Instantiate(effectBlood, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo viên đạn trùng với hướng hiện tại
                Invoke("CanvasHP", 0.75f);
            }
        }
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerAttack"){
            PlayerController playerController = player.GetComponent<PlayerController>();
            mHp -= playerController.Dame()*3/4;
            if(mHp <= 0){
                myAnimation.SetBool("Die", true);
                Invoke("SetActive", 2f);
            }else{
                canvasHp.SetActive(true);
                sldHp.value = mHp;
                Instantiate(effectBlood, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo viên đạn trùng với hướng hiện tại
                Invoke("CanvasHP", 0.75f);
            }
        }
    }
    void CanvasHP(){
        canvasHp.SetActive(false);
    }
    void SetActive(){
        //gameObject.SetActive(!gameObject.activeSelf);
        Destroy(gameObject);
       
        managerController.TaoItem(new Vector3(transform.position.x, transform.position.y + 0.2f, 0));
    }
    // void AttackCls(){
    //     AttackCls.SetActive(true);
    // }
    public void Attack(){
        if(m_timeSpwam[type] <= 0 && mHp > 0){
            // ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
            managerController.PlaySound(type + 1);
            attack = true;
            m_timeSpwam[type] = timeSpwam[type];
            if(type == 0 || type == 2){
                myAnimation.SetInteger("Attack", 1);
                Invoke("Idle", 0.36f);
            }else{
                if(facingRight){
                    Instantiate(bullet, bulletTransform.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo viên đạn trùng với hướng hiện tại
                }else{
                    Instantiate(bullet, bulletTransform.transform.position, Quaternion.Euler(new Vector3(0, 0, 180))); // tạo viên đạn trùng với hướng hiện tại
                }
                myAnimation.SetInteger("Attack", 2);
                Invoke("Idle", 0.2f);
            }
        }   
    }
    public int Type(){
        return type;
    }
    public float Dame(){
        if(type == 0){
            //attackCls.SetActive(false);
            //Debug.Log("at");
            //Invoke("AttackCls", 0.2f);
            return dame;
        }else
            return dame;
    }
    public void SetFace(){
        //facingRight = value;
        delayRun = Random.Range(0.2f, 1f);
        delayIdle = Random.Range(0.2f, 1f);
    }
}
