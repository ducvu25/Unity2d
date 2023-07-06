using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gamemaster : MonoBehaviour
{
    public int points;
    public int pointsave;
    public Text pionttext;
    // Start is called before the first frame update
    void Start()
    {
        pionttext.text = (""+PlayerPrefs.GetInt("pointsave"));
        pointsave = PlayerPrefs.GetInt("pointsave", 0);
        if(PlayerPrefs.HasKey("points"))
        {
            Scene ActiveScene = SceneManager.GetActiveScene();
            if (ActiveScene.buildIndex == 0)
            {
                PlayerPrefs.DeleteKey("points");
                points = 0;

            }
            else
                points = PlayerPrefs.GetInt("points");
        }
    }

    // Update is called once per frame
    void Update()
    {
        pionttext.text = ("" + points);
    }
    public void uppoint(int p)
    {
        points += p;
    }
}
