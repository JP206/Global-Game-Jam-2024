using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float limiteDerecha, limiteIzquierda, limiteArriba, limiteAbajo;
    GameObject jugador;

    void Start()
    {
        jugador = GameObject.Find("Jugador");
    }

    void LateUpdate()
    {
        transform.position = new Vector3(jugador.transform.position.x, jugador.transform.position.y, -10);
    }
}
