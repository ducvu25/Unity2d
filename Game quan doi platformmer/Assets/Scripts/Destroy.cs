using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] float time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }
}
