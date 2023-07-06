using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] float torqueAmount = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow)){
            rb2D.AddTorque(-torqueAmount);
            //Debug.Log("Bam nut");
        }else if(Input.GetKey(KeyCode.LeftArrow)){
            rb2D.AddTorque(torqueAmount);
        }else{
            rb2D.AddTorque(-torqueAmount/2);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Finish"){
            Invoke("NewGame", 1);
        }
    }
    void NewGame(){
        SceneManager.LoadScene(0);
    }
}
