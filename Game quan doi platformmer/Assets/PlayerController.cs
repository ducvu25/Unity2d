using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float forceJump = 200f;
    [SerializeField] float dame = 100f;
    [SerializeField] float maxHp = 100f;
    [SerializeField] Slider sldHp;
    [SerializeField] Slider[] sldTimeSkill;
    [SerializeField] GameObject effectBlood;

    Rigidbody2D myBody;
    Animator myAnimation;
    AnimationClip[] clips;

    float mSpeed;
    float mHp;
    bool touchTheGround = false;
    bool facingRight;
    float[] timeSpwam = {0.5f, 0.2f};
    float[] m_timeSpwam = {0, 0};
    float delayJump = 1f;
    float m_delayJump = 0;
    // Start is called before the first frame update
    void Start()
    {
        mSpeed = maxSpeed;
        mHp = maxHp;
        facingRight = true;
        // sldHp.maxValue = maxHp;
        // sldHp.value = maxHp;
        // for(int i = 0; i < timeSpwam.Length; i++) {
        //     sldTimeSkill[i].value = m_timeSpwam[i];  
        //     sldTimeSkill[i].maxValue = timeSpwam[i]; 
        // }
        myBody = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
        // clips = myAnimation.runtimeAnimatorController.animationClips;
        // foreach (AnimationClip clip in clips) {
        //     if (clip.name == "PlayerJump") {
        //         delayJump = clip.length;
        //         m_delayJump = 0;
        //         break;
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // for(int i = 0; i < timeSpwam.Length; i++) {
        //     if(m_timeSpwam[i] < 0){
        //         m_timeSpwam[i] = 0;
        //         continue;
        //     }
        //     m_timeSpwam[i] -= Time.deltaTime;
        //     sldTimeSkill[i].value = m_timeSpwam[i];  
        // }
        this.Jump();
        this.Run();
        this.Attack();
    }
    void Jump(){
        m_delayJump -= Time.deltaTime;
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && touchTheGround){
            touchTheGround = false;
            m_delayJump = delayJump;
            myBody.AddForce(new Vector2(0, forceJump));
            myAnimation.SetBool("Jump", true);
             Debug.Log("Jump");
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
            myAnimation.SetBool("Run", true);
            transform.position = transform.position + new Vector3(move_x, 0, 0);
        }else
            myAnimation.SetBool("Run", false);
    }
    void Attack(){

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground")){// && m_delayJump <= 0){
            touchTheGround = true;
            myAnimation.SetBool("Jump", false);
           // Debug.Log("Ground");
        }
    }
}
