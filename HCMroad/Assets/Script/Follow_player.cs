
using UnityEngine;

public class Follow_player : MonoBehaviour
{
    public Transform playerTransform;
    public float offset;
     void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()

    {
        Vector3 temp = transform.position;
        temp.x = playerTransform.position.x;
        temp.x += offset;
        transform.position = temp;
    }
}
