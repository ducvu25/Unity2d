using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int LevelLoad = 1;
    public gamemaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gamemaster").GetComponent<gamemaster>();
    }

    // Update is called once per frame
private void OnTriggerEnter2D(Collider2D col)
    {
       
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
                if (Input.GetKeyDown("e"))
                {
                    savescore();
                    SceneManager.LoadScene(LevelLoad);
                }
            }
    }
    private void OnTriggerExit2D(Collider2D col)
    { }
    void savescore()
    {
        PlayerPrefs.SetInt("points", gm.points);
    }
}
