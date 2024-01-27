using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float limiteDerecha, limiteIzquierda, limiteArriba, limiteAbajo;
    GameObject jugador;

    void Start()
    {
        jugador = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        transform.position = new Vector3(jugador.transform.position.x, jugador.transform.position.y, -10);
        if (transform.position.x < limiteIzquierda)
        {
            transform.position = new Vector3(limiteIzquierda, jugador.transform.position.y, -10);
        }
        if (transform.position.x > limiteDerecha)
        {
            transform.position = new Vector3(limiteDerecha, jugador.transform.position.y, -10);
        }
        if (transform.position.y > limiteArriba)
        {
            transform.position = new Vector3(jugador.transform.position.x, limiteArriba, -10);
        }
        if (transform.position.y < limiteAbajo)
        {
            transform.position = new Vector3(jugador.transform.position.x, limiteAbajo, -10);
        }
    }
}
