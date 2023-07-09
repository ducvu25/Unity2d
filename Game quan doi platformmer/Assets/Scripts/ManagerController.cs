using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagent;
using UnityEngine.UI;
using TMPro;

public class ManagerController : MonoBehaviour
{
    [SerializeField] GameObject coin;
    [SerializeField] GameObject bullet30;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuEnd;
    [SerializeField] TextMeshProUGUI textNumberCoin;
    //[SerializeField] GameObject prefab;
    [SerializeField] AudioSource BG;
    [SerializeField] AudioClip run;
    AudioSource runAS;
    [SerializeField] AudioClip hit;
    AudioSource hitAS;
    [SerializeField] AudioClip bullet;
    AudioSource bulletAS;
    [SerializeField] AudioClip item;
    AudioSource itemAS;
    [SerializeField] AudioClip end;
    AudioSource endAS;
    [SerializeField] AudioClip eat;
    AudioSource eatAS;
    [SerializeField] GameObject prefab; // Add this line to declare the prefab variable

    private static ManagerController instance;

    bool pause;
    
    void Awake()
    {
        instance = this;
    }

    public void PlaySound(int i, float volume = 1f, bool isLoopback = false)
    {
        switch (i)
        {
            case 0:
                Play(run, ref runAS, volume, isLoopback);
                break;
            case 1:
                Play(hit, ref hitAS, volume, isLoopback);
                break;
            case 2:
                Play(bullet, ref bulletAS, volume, isLoopback);
                break;
            case 3:
                Play(item, ref itemAS, volume, isLoopback);
                break;
            case 4:
                Play(end, ref endAS, volume, isLoopback);
                break;
            case 5:
                Play(eat, ref eatAS, volume, isLoopback);
                break;
        }
    }

    void Play(AudioClip clip, ref AudioSource audioSource, float volume = 1f, bool isLoopback = false)
    {
        if (audioSource != null && audioSource.isPlaying)
            return;
        audioSource = Instantiate(instance.prefab).GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = isLoopback;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
// `   public void StopSound(int i){
//         switch(i){
//             case 0:{
//                 runAS?.Stop();
//                 break;
//             }
//         }
//     }
    void Start()
    {
        pause = false;
        menuPause.SetActive(false);
        menuEnd.SetActive(false);
    }
    public void Pause(bool value){
        pause = value;
        if(!pause){
            menuPause.SetActive(false);
        }else{
            menuPause.SetActive(true);
        }
    }
    public bool Pause(){
        return pause;
    }
    // public void Restart(){
    //     //SceneManagent.LoadScene()
    // }
    // void Update()
    // {

    // }

    public void TaoItem(Vector3 position)
    {
        int x = (int)Random.Range(0, 5);
        if (x == 2)
        {
            Instantiate(coin, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else if (x == 4)
        {
            Instantiate(bullet30, position, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }
    public void EndGame(int coin){
        pause = true;
        menuEnd.SetActive(true);
        textNumberCoin.text = coin.ToString();
    }
}