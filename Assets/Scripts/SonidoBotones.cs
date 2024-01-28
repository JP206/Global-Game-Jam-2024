using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoBotones : MonoBehaviour
{
    public AudioClip sonido;
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void TocarBotonSonido()
    {
        audioSource.PlayOneShot(sonido);
    }
}
