using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 15;
    [SerializeField] float maxSpeed = 30;
    [SerializeField] float minSpeed = 15;
    [SerializeField] ParticleSystem effectSnow;
    SurfaceEffector2D surfaceEffector2D;
    Rigidbody2D rb2D;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        speed = minSpeed;
        surfaceEffector2D.speed = speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > minSpeed)
            speed -= 0.1f;
        if (torqueAmount > 15)
            torqueAmount -= 0.1f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2D.AddTorque(-torqueAmount);
            //Debug.Log("Bam nut");
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2D.AddTorque(torqueAmount);
        }
        else
        {
            rb2D.AddTorque(-torqueAmount / 3);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed = maxSpeed;
            torqueAmount = 25;

        }
        surfaceEffector2D.speed = speed;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Finish")
        {
            Invoke("NewGame", 1);
        }
    }
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Map")
        {
            effectSnow.Play();
        }
    }
    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Map")
        {
            effectSnow.Stop();
        }
    }
    void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
