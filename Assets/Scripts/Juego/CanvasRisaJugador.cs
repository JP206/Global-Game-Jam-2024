using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasRisaJugador : MonoBehaviour
{
    TextMeshProUGUI texto;
    
    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        StartCoroutine(CambiarColor());
    }

    IEnumerator CambiarColor()
    {
        float tick = 0f;
        while (texto.color != Color.green)
        {
            tick += Time.deltaTime * 2f;
            texto.color = Color.Lerp(Color.white, Color.green, Mathf.PingPong(Time.time, 1));
            yield return null;
        }
    }
}
