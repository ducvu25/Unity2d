using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public rank_controller movement;
    public float restartDelay = 2f;// thời gian chờ để restart 
    public GameObject completeUI;
    public Text managerText;
    //Tạo âm thanh
    public AudioSource audioSource;
    public AudioClip  complete,died;
    bool gameHasEnded = false;//kiểm tra player còn hoạt động
    public void Complete()
    {
        movement.enabled = false;
        completeUI.SetActive(true);//hiện dòng chữ Complete
        managerText.text = "Complete !!!";
        audioSource.PlayOneShot(complete);//phát âm khi chạm cờ
     
    }
   
    public void Died()
    {
        movement.enabled = false;
        completeUI.SetActive(true);//hiện dòng chữ
        managerText.text = "Died..";
        audioSource.PlayOneShot(died);//phát âm khi chạm bom và chết
        Invoke("Restart", restartDelay);// chờ restart
    }
    public void EndGame()
    {
        movement.enabled = false;
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            completeUI.SetActive(true);//hiện dòng chữ
            managerText.text = "Cạn năng lượng..";
            Invoke("Restart", restartDelay);
        }

    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
