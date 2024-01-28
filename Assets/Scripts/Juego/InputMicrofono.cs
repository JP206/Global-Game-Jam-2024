using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMicrofono : MonoBehaviour
{
    int sampleWindow = 64;
    AudioClip microphoneClip;
    float loudnessSensitibity = 100, threshold = 5f, contador = 5;
    public bool sigueJuego = true;

    void Start()
    {
        MicrophoneToAudioClip();
    }

    void Update()
    {
        if (sigueJuego && Time.timeScale == 1)
        {
            float loudness = GetLoudnessFromMicrophone() * loudnessSensitibity;

            if (loudness > threshold && contador >= 5)
            {
                GetComponent<RisaJugador>().risaJugador();
                contador = 0;
            }
            contador += Time.deltaTime;
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
}
