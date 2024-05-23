using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource playerAudio;

    public AudioClip jumpscare;
    public AudioClip gunshot;
    public AudioClip doorOpen;
    public AudioClip trapDoor;
    public AudioClip henchmen;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playJumpscare()
    {
        playerAudio.PlayOneShot(jumpscare, 1.0f);
    }
    public void playGunshot()
    {
        playerAudio.PlayOneShot(gunshot, 1.0f);
    }
    public void playDoorOpen()
    {
        playerAudio.PlayOneShot(doorOpen, 1.0f);
    }
    public void playTrapDoor()
    {
        playerAudio.PlayOneShot(trapDoor, 1.0f);
    }
    public void playHenchmen()
    {
        playerAudio.PlayOneShot(henchmen, 1.0f);
    }
}
