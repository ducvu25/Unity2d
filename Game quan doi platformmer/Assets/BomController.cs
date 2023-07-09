using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomController : MonoBehaviour
{
    [SerializeField] GameObject effect;
    ManagerController managerController;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        managerController = GameObject.FindWithTag("Manager").GetComponent<ManagerController>();

    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(player.transform.position.x < transform.position.x + 10 && player.transform.position.x > transform.position.x - 10)
            managerController.PlaySound(7);
        Instantiate(effect, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
   /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(player.transform.position.x < transform.position.x + 10 && player.transform.position.x > transform.position.x - 10)
            managerController.PlaySound(7);
        Instantiate(effect, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
