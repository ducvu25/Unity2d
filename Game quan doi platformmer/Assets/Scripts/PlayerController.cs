using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 2f; // tốc độ tối đa
    [SerializeField] float forceJump = 200f; // lực nhảy tối đa
    [SerializeField] float dame = 100f; // sát thương
    [SerializeField] float maxHp = 100f; // hp tối đa
    [SerializeField] int numberBullet = 100; // số lượng đạn ban đầu
    [SerializeField] Slider sldHp; // thanh máu
    [SerializeField] Slider[] sldTimeSkill; // thanh thời gian hiển thị kĩ năng
    [SerializeField] GameObject effectBlood; // hiệu ứng mất máu
    [SerializeField] GameObject effectBlood2; // hiệu ứng hồi máu
    [SerializeField] GameObject bullet; // đạn
    [SerializeField] GameObject bulletTransform; // vị trí đạn
    //[SerializeField] 
    [SerializeField] TextMeshProUGUI textNumberBullet; // hiển thị số lượng đạn
    [SerializeField] TextMeshProUGUI textNumberCoin; // hiển thị số lượng vàng hiện có

    Rigidbody2D myBody; // tác động vật lý
    Animator myAnimation; // hiệu ứng chuyển động nhân vật
    GameObject manager; // quản lý màn chơi
    ManagerController managerController; // code tương tác

    int numberCoin; // số lượng vàng hiện có
    float mSpeed; // tốc độ hiện tại
    float mHp; // lượng máu hiện tại
    bool touchTheGround = false; // chạm đất
    bool facingRight; // nhân vật quay mặt về bên phải
    bool doubleJump; // nhảy 2 lần
    float[] timeSpwam = {0.5f, 0.3f}; // thời gian hồi chiêu
    float[] m_timeSpwam = {0, 0}; // thời gian đếm ngược 
    float delayJump = 1f; // khonagr thời gian tối thiểu giữa 2 lần nhảy
    float m_delayJump = 0; // thời gian thực
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
        if(Input.GetKey(KeyCode.Q) && m_timeSpwam[0] <= 0){ // người dùng bấm phím Q và chiêu đã hồi xong
            m_timeSpwam[0] = timeSpwam[0]; // làm mới thời gian hồi
            ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
            managerController.PlaySound(1); // phát âm thanh
            myAnimation.SetInteger("Attack", 1);// cài đặt hiệu ứng tấn công
            Debug.Log("Skill 1"); // thông báo dùng chiêu 1
            Invoke("DelayAttack", 0.36f); // gọi hàm DelayAttack sau 0.36s
        }else if(Input.GetKey(KeyCode.W) && m_timeSpwam[1] <= 0 && numberBullet > 0){ // người dùng bấm phím W và hồi chiêu xong
            m_timeSpwam[1] = timeSpwam[1]; // // làm mới thời gian hồi
            ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
            managerController.PlaySound(2);// phát âm thanh
            numberBullet--;// giảm số lượng đạn
            textNumberBullet.text = numberBullet.ToString(); // cập nhật lên phần hiển thị
            myAnimation.SetInteger("Attack", 2);// cài đặt hiệu ứng tấn công
            Invoke("DelayAttack", 0.2f);// gọi hàm DelayAttack sau 0.2s
            //Debug.Log("Skill 2");
            if(facingRight){// nếu nhân vật đang quay sang phải:
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
        if(other.gameObject.CompareTag("Ground") && m_delayJump <= 0){ // nếu nhân vật chạm đất và độ trễ giữa 2 lần nhỏ hơn 0
            touchTheGround = true; // nhân vật chạm đất
            myAnimation.SetBool("Jump", false); // tắt hiệu ứng nhảy
           // Debug.Log("Ground");
        }
        if(other.gameObject.CompareTag("LineMap")){ // nhân vật chạm giới hạn bản đồ
            myAnimation.SetBool("Die", true); // đặt hiệu ứng chết
            Invoke("EndGame", 1f); // gọi hàm end game sau 1s
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "30Bullet" && other.gameObject.activeSelf){ // nếu chạm với hộp đạn
            ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
            managerController.PlaySound(3); // phát âm thanh
            numberBullet += 30; // tăng số lượng đạn
            textNumberBullet.text = numberBullet.ToString(); // hiển thị lên màn hình
            other.gameObject.SetActive(false); // tắt trạng thái hoạt động của hộp đạn
            Destroy(other.gameObject); // hủy đối tượng hộp đạn
        }
        if(other.tag == "Coin" && other.gameObject.activeSelf){// nếu chạm với đồng vàng
            //Debug.Log(numberCoin);
            ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
            managerController.PlaySound(3);// phát âm thanh
            numberCoin += (int)Random.Range(50, 100);// tạo số lượng vàng từ 50-> 100
            PlayerPrefs.SetInt("Coin", numberCoin);
            textNumberCoin.text = numberCoin.ToString();// hiển thị lên màn hình
            other.gameObject.SetActive(false);// tắt trạng thái hoạt động
            Destroy(other.gameObject); // hủy đối tượng
            //Debug.Log(numberCoin);
        }
    
        if(other.tag == "BulletEnemy"){
            this.AddDame(other.GetComponent<BulletControler>().Dame()); // gọi hàm nhận sát thương
            other.gameObject.SetActive(false); // // tắt trạng thái hoạt động
            Destroy(other.gameObject);// hủy đối tượng
        }
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyAttack"){ // va chạm với vùng sát thương của quái
            //Debug.Log("ok");
            this.AddDame(other.transform.parent.gameObject.GetComponent<EnemyController>().Dame()); // gọi hàm nhận sát thương
            //other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
        }
        if(other.tag == "Bum"){// va chạm với bom
            GameObject plane = GameObject.FindWithTag("Plane");
            PlaneController planeController = plane.GetComponent<PlaneController>();
            //Debug.Log("ok");
            this.AddDame(planeController.Dame());// gọi hàm nhận sát thương
            //other.gameObject.SetActive(false);
            //Destroy(other.gameObject);
        }
        if(other.tag == "Finish") // đến đích
            Invoke("EndGame", 1f); // kết thúc game sau 1s
    }
    // public void OnAnimationEnd()
    // {
    //     myAnimation.SetTrigger("PlayerIdle"); // Kích hoạt trigger để chuyển đổi sang trạng thái khác
    // }
    void DelayAttack(){ // tắt trạng thái tấn công
        myAnimation.SetInteger("Attack", 0);
    }
    void PlayIdle(){ // chuyển về trạng thái nghỉ
        if(effectBlood2.activeSelf) // nếu hồi phục đang bật
            effectBlood2.SetActive(false); // tắt trạng thái
        else{
            myAnimation.Play("PlayerIdle"); // chuyển sang trạng thái đứng
        }
    }
     void EndGame(){
        managerController.EndGame(numberCoin);
    }
    public void BuyBullet(){ // mua đạn - chuyển đổi 30 vàng - 30 viên đạn
        if(numberCoin >= 30){
            numberCoin -= 30;
            PlayerPrefs.SetInt("Coin", numberCoin);
            textNumberCoin.text = numberCoin.ToString();
            numberBullet += 30;
            textNumberBullet.text = numberBullet.ToString();
        }
    }
    public void AddDame(float dame){ // nhận sát thương
        mHp -= dame; // cập nhật lượng máu
        if(mHp < 0) // kiểm tra giới hạn
            mHp = 0;
        else if(mHp > maxHp)
            mHp = maxHp;
        sldHp.value = mHp;

        if(mHp<= 0){ // nếu nhân vật hết máu
            myAnimation.SetBool("Die", true); // chuyển trạng thái
            Invoke("EndGame", 1f); // kết thúc game sau 1s
        }else{
            if(dame > 0){ // nhận sát thương
                myAnimation.Play("PlayerChamBay"); // hiển thị  trạng thái nhận sát thương
                Invoke("PlayIdle", 0.25f); // chuyển về trạng thái đứng sau 0.25s
                // đẩy lùi nhân vật ra sau
                if(facingRight){
                    transform.position = transform.position - new Vector3(mSpeed*Time.deltaTime, 0, 0); 
                }else{
                    transform.position = transform.position + new Vector3(mSpeed*Time.deltaTime, 0, 0); // đẩy nhân vật tiến lên
                }
            }else if(!effectBlood2.activeSelf){ // nếu hiệu ứng hồi phục không hoạt động
                ManagerController managerController = manager.GetComponent<ManagerController>();
                //Debug.Log("oeke");
                managerController.PlaySound(5); // phát âm thanh hồi phục
                effectBlood2.SetActive(true); // bật hiệu ứng hồi phục
                Invoke("PlayIdle", 0.75f);// chuyeenr trạng thái đứng sau 0.75s
            }
        }
    }
    public float Dame(){ // trả về sát thương nhân vật
        return dame;
    }
}
