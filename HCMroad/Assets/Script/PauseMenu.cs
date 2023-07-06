using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public bool pause = false;
    public GameObject pauseUI;

    // Use this for initialization
    void Start()
    {
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            pause = false;
        }
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;

        }

        if (pause)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (pause == false)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }


    }
    public void resume()
    {
        pause = false;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
