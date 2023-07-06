using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioClip m416, m249;
    public AudioSource adisrc;
    // Use this for initialization
    void Start () {
		m416 = Resources.Load<AudioClip>("m416");
        m249 = Resources.Load<AudioClip>("M249");

        adisrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Playsound(string clip)
    {
        switch (clip)
        {
            case "m416":
                adisrc.clip = m416;
                adisrc.PlayOneShot(m416, 0.6f);
                break;

            case "m249":
                adisrc.clip = m249;
                adisrc.PlayOneShot(m249, 1f);
                break;

        

        }
    }
}
