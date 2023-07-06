using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rank_controller : MonoBehaviour
{ 
    // Tạo xăng xe
    public petrol_bar p;
    public int maxPetrol = 1000;
    private int currentPetrol;
    public int dagame = 1; // giá trị xăng giảm khi chạy

    // tạo di chuyển 
    public float speed = 4f;// vận tốc
    public float maxVelocity = 8f;// vận tốc tối đa
    [SerializeField]
    private Rigidbody2D myBody;

   
    //Tạo âm thanh
    public AudioSource audioSource;
    public AudioClip run, petrol_touch;

    // animation cho player
    private Animator anim;
    float forxeX = 0f;
    // Biến điều khiển
    public game_manager manager;

    private bool dk = true;
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //vào game: xăng = max xăng
        currentPetrol = maxPetrol;
        p.setMaxPetrol(maxPetrol);

    }
    public Text notification;
    public GameObject notifica;
 
  
    void FixedUpdate()
    {
        moveKeyboard();
        if (currentPetrol <= 0)
        {
            manager.EndGame();
        }
      if(touch) manager.Died();//nóc xe chạm sẽ dừng lại
    }
    void moveKeyboard()
    {
        if (dk == true)
        {
            float vel = Mathf.Abs(myBody.velocity.x);//giá trị tọa độ x
            float w = Input.GetAxisRaw("Horizontal");// dịch chuyển theo chiều ngang
            //  float h = Input.GetAxisRaw("Vertical");
            go(w, vel);
            myBody.AddForce(new Vector2(forxeX, 0));
            //go(h,vel);
            //myBody.AddForce(new Vector2(0, forxeX));
        }

    }
    void go( float d, float vel)
    {
        forxeX = 0f;
        if (d > 0)
        {
            _takeDamage();
            audioSource.PlayOneShot(run);// phát âm khi nhấn
            if (vel < maxVelocity) forxeX = speed;
            anim.SetFloat("go", vel);
        }
        else
        {
            if (d < 0)
            {
                _takeDamage();
                audioSource.PlayOneShot(run);
                if (vel < maxVelocity) forxeX = -speed;
                anim.SetFloat("go", vel);
            }
            else
            {
                anim.SetFloat("go", vel);
            }
        }
    }
    // Chạm petrol
    void OnCollisionEnter2D(Collision2D ob)
    {
        if (ob.gameObject.tag == "petrol")// vật chạm là petrol
        {
            audioSource.PlayOneShot(petrol_touch);//phát âm khi chạm
            Destroy(ob.gameObject);
            currentPetrol = maxPetrol;// Xăng =max
            p.setPetrol(currentPetrol);// gán lại
        }
        if (ob.gameObject.tag == "flag")// vật chạm là lá cờ
        {
            manager.Complete();// gọi hàm hoàn thành
        }
        if(ob.gameObject.tag == "bomb")// xe chạm bom là chết
        {
            manager.Died();//
        }
    }
    // chịu hư hại: giảm xăng khi chạy
    void _takeDamage()
    {
        currentPetrol -= dagame;
        p.setPetrol(currentPetrol);
    }
   // Xử lý va chạm mặt đất
    private bool touch = false;//chạm mặt đất hay k
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")//nếu chạm
        {
            touch = true;
             manager.Died();
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        touch = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        touch = false;


    }

}
