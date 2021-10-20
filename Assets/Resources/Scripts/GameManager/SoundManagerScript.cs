using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        audioSource.clip = audioClip;
    }
    
    void Update()
    {

    }

    public void PlaySound()
    {
        print("Sound");
        audioSource.PlayOneShot(audioClip);
    }

}