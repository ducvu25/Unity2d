using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float steerSpeed = 30;
    [SerializeField] Color32 color1 = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 color2 = new Color32(1, 1, 1, 1);

    SpriteRenderer spriteRenderer;
    float slowSpeed = 3;
    float fastSpeed = 10;
    float speed;
    bool coThu;
    // Start is called before the first frame update
    void Start()
    {
        coThu = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.color = color1;
        speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        this.Run();
    }
    void Run()
    {
        if(speed > moveSpeed)
            speed -= 0.01f;
        if(speed < moveSpeed)
            speed += 0.01f;

        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        if(speed < fastSpeed && moveAmount != 0)
            speed += 0.15f;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Slow")){
            //Destroy(other.gameObject, 0.5f);
            speed = slowSpeed;
            Debug.Log("cham slow");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Slow"){
            Destroy(other.gameObject, 0.5f);
            speed = slowSpeed;
            Debug.Log("cham slow");
        }
        if(other.tag == "Speed" && !coThu){
            Destroy(other.gameObject, 0.5f);
            speed = fastSpeed;
            coThu = true;
            spriteRenderer.color = color2;
            Debug.Log("cham speed");
        }
        if(other.tag == "Customer" && coThu){
            Destroy(other.gameObject, 0.5f);
            coThu = false;
            spriteRenderer.color = color1;
            Debug.Log("cham nguoi");
        }
    }
}
