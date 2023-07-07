using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayController : MonoBehaviour
{
    [SerializeField] float dame = 10f;
    [SerializeField] float time = 0.5f;
    float m_time = 0;
    void Update()
    {
        if(m_time > 0)
            m_time -= Time.deltaTime;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && m_time <= 0){
            Debug.Log("gai");
            m_time = time;
            PlayerController  playerController = other.GetComponent<PlayerController>();
            if(playerController != null)
                playerController.AddDame(dame);
        }
    }
    // void OnTriggerEnter2D(Collider2D other)
    // {
        
    // }
}
