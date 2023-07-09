using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float forceJump = 200f;
    [SerializeField] float dame = 100f;
    [SerializeField] float maxHp = 100f;
    [SerializeField] int numberBullet = 100;
    [SerializeField] Slider sldHp;
    [SerializeField] Slider[] sldTimeSkill;
    [SerializeField] GameObject effectBlood;
    [SerializeField] GameObject effectBlood2;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletTransform;
    //[SerializeField] 
    [SerializeField] TextMeshProUGUI textNumberBullet;
    [SerializeField] TextMeshProUGUI textNumberCoin;

    Rigidbody2D myBody;
    Animator myAnimation;
    GameObject manager;
    ManagerController managerController;

    int numberCoin;
    float mSpeed;
    float mHp;
    bool touchTheGround = false;
    bool facingRight;
    bool doubleJump;
    float[] timeSpwam = {0.5f, 0.3f};
    float[] m_timeSpwam = {0, 0};
    float delayJump = 1f;
    float m_delayJump = 0;
    // Start is called before the first frame update
    void Start()
    {
        mSpeed = maxSpeed;
        mHp = maxHp;
        facingRight = true;
        doubleJump = false;
        sldHp.maxValue = maxHp;
        sldHp.value = maxHp;
        // for(int i = 0; i < timeSpwam.Length; i++) {
        //     sldTimeSkill[i].value = m_timeSpwam[i];  
        //     sldTimeSkill[i].maxValue = timeSpwam[i]; 
        // }
        myBody = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
        textNumberBullet.text = numberBullet.ToString();
        numberCoin = 0;//PlayerPrefs.GetInt("Coin");
        textNumberCoin.text = numberCoin.ToString();
        effectBlood2.SetActive(false);
        manager = GameObject.FindWithTag("Manager");
        managerController = manager.GetComponent<ManagerController>();
        //Debug.Log("oeke");
    }

    // Update is called once per frame
    void Update()
    {
        if(managerController.Pause())
            return;
        for(int i = 0; i < timeSpwam.Length; i++) {
            if(m_timeSpwam[i] < 0){
                m_timeSpwam[i] = 0;
                continue;
            }
            m_timeSpwam[i] -= Time.deltaTime;
           // sldTimeSkill[i].value = m_timeSpwam[i];  
        }
        this.Run();
        this.Attack();
        this.Jump();
    }
    void Jump(){
        m_delayJump -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)){
            if(touchTheGround){
                myBody.AddForce(new Vector2(0, forceJump));
                doubleJump = true;
            }else if(doubleJump){
                doubleJump = false;
                myBody.AddForce(new Vector2(0, forceJump*4/5));
            }else
                return;
            m_delayJump = delayJump;
            myAnimation.SetBool("Jump", true);
            //Debug.Log("Jump");
            touchTheGround = false;
        }
    }
    void Run(){
        //return;
        float move_x = Time.deltaTime;
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            if(facingRight){
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
            facingRight = false;
            move_x *= -mSpeed;
        }else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            if(!facingRight){
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, 0);
            }
            facingRight = true;
            move_x *= mSpeed;
        }else
            move_x = 0;

        if(move_x != 0){
            if(touchTheGround){
                myAnimation.SetBool("Run", true);
                ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
                managerController.PlaySound(0);
            }
            transform.position = transform.position + new Vector3(move_x, 0, 0);
        }else
            myAnimation.SetBool("Run", false);
    }
    void Attack(){
        if(Input.GetKey(KeyCode.Q) && m_timeSpwam[0] <= 0){
            m_timeSpwam[0] = timeSpwam[0];
            ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
            managerController.PlaySound(1);
            myAnimation.SetInteger("Attack", 1);
            Debug.Log("Skill 1");
            Invoke("DelayAttack", 0.36f);
        }else if(Input.GetKey(KeyCode.W) && m_timeSpwam[1] <= 0 && numberBullet > 0){
            m_timeSpwam[1] = timeSpwam[1];
            ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
            managerController.PlaySound(2);
            numberBullet--;
            textNumberBullet.text = numberBullet.ToString();
            myAnimation.SetInteger("Attack", 2);
            Invoke("DelayAttack", 0.2f);
            //Debug.Log("Skill 2");
            if(facingRight){
                Instantiate(bullet, bulletTransform.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); // tạo viên đạn trùng với hướng hiện tại
            }else{
                Instantiate(bullet, bulletTransform.transform.position, Quaternion.Euler(new Vector3(0, 0, 180))); // tạo viên đạn trùng với hướng hiện tại
            }
        }
        // else 
        //     myAnimation.SetInteger("Attack", 0);
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground") && m_delayJump <= 0){
            touchTheGround = true;
            myAnimation.SetBool("Jump", false);
           // Debug.Log("Ground");
        }
        if(other.gameObject.CompareTag("LineMap")){
            myAnimation.SetBool("Die", true);
            Invoke("EndGame", 1f);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "30Bullet" && other.gameObject.activeSelf){
            ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
            managerController.PlaySound(3);
            numberBullet += 30;
            textNumberBullet.text = numberBullet.ToString();
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
        if(other.tag == "Coin" && other.gameObject.activeSelf){
            //Debug.Log(numberCoin);
            ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
            managerController.PlaySound(3);
            numberCoin += (int)Random.Range(50, 100);
            PlayerPrefs.SetInt("Coin", numberCoin);
            textNumberCoin.text = numberCoin.ToString();
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            //Debug.Log(numberCoin);
        }
    
        if(other.tag == "BulletEnemy"){
            this.AddDame(other.GetComponent<BulletControler>().Dame());
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyAttack"){
            //Debug.Log("ok");
            this.AddDame(other.transform.parent.gameObject.GetComponent<EnemyController>().Dame());
            //other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
        }
    }
    // public void OnAnimationEnd()
    // {
    //     myAnimation.SetTrigger("PlayerIdle"); // Kích hoạt trigger để chuyển đổi sang trạng thái khác
    // }
    void DelayAttack(){
        myAnimation.SetInteger("Attack", 0);
    }
    void PlayIdle(){
        if(effectBlood2.activeSelf)
            effectBlood2.SetActive(false);
        else{
            myAnimation.Play("PlayerIdle");
        }
    }
    public void BuyBullet(){
        if(numberCoin >= 30){
            numberCoin -= 30;
            PlayerPrefs.SetInt("Coin", numberCoin);
            textNumberCoin.text = numberCoin.ToString();
            numberBullet += 30;
            textNumberBullet.text = numberBullet.ToString();
        }
    }
    public void AddDame(float dame){
        mHp -= dame;
        if(mHp < 0)
            mHp = 0;
        else if(mHp > maxHp)
            mHp = maxHp;
        sldHp.value = mHp;
        if(mHp<= 0){
            myAnimation.SetBool("Die", true);
            Invoke("EndGame", 1f);
        }else{
            if(dame > 0){
                myAnimation.Play("PlayerChamBay");
                Invoke("PlayIdle", 0.25f);
                if(facingRight){
                    transform.position = transform.position - new Vector3(mSpeed*Time.deltaTime, 0, 0);
                }else{
                    transform.position = transform.position + new Vector3(mSpeed*Time.deltaTime, 0, 0);
                }
            }else if(!effectBlood2.activeSelf){
                ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
                managerController.PlaySound(5);
                effectBlood2.SetActive(true);
                Invoke("PlayIdle", 1f);
            }
        }
    }
    public float Dame(){
        return dame;
    }
    void EndGame(){
        managerController.EndGame(numberCoin);
    }
}
