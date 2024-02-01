using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMicrofonoMenuPrincipal : MonoBehaviour
{
    int sampleWindow = 64;
    AudioClip microphoneClip;
    float loudnessSensitibity = 100, threshold = 6f, contador = 5;
    AudioSource audioSource;
    public AudioClip sonidoRisa;
    Animator animator;
    GameObject canvasRisas;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        canvasRisas = transform.GetChild(0).gameObject;
        MicrophoneToAudioClip();
    }

    void Update()
    {
        float loudness = GetLoudnessFromMicrophone() * loudnessSensitibity;

        if ((loudness > threshold && contador >= 5) || Input.GetKeyDown(KeyCode.F))
        {
            risaJugador();
            contador = 0;
        }
        contador += Time.deltaTime;

        if (canvasRisas.activeSelf)
        {
            canvasRisas.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;
        if (startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }

    void risaJugador()
    {
        audioSource.PlayOneShot(sonidoRisa);
        StartCoroutine(risa());
    }

    IEnumerator risa()
    {
        canvasRisas.SetActive(true);
        animator.SetTrigger("ReirFront");
        yield return new WaitForSeconds(5);
        canvasRisas.SetActive(false);
        animator.SetTrigger("RestFront");
    }
}