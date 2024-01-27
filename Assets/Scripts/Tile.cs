using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool esObstaculo;
    [SerializeField] GameObject arriba, abajo, izquierda, derecha;
    int miNumero, miColumna;

    void Start()
    {
        miNumero = Int32.Parse(name.Substring(5));
        miColumna = Int32.Parse(transform.parent.gameObject.name.Substring(8));

        if (miNumero != 1)
        {
            arriba = transform.parent.GetChild(miNumero - 2).gameObject; //arrancan en 1, no en 0, y necesito el numero anterior
        }
        if (miNumero != 10)
        {
            abajo = transform.parent.GetChild(miNumero).gameObject;
        }
        if (miColumna != 1)
        {
            izquierda = transform.parent.parent.transform.GetChild(miColumna - 2).gameObject.transform.GetChild(miNumero - 1).gameObject;
        }
        if (miColumna != 10)
        {
            derecha = transform.parent.parent.transform.GetChild(miColumna).gameObject.transform.GetChild(miNumero - 1).gameObject;
        }
        if (esObstaculo)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public bool EstaEnCamara()
    {
        Vector3 vpPos = Camera.main.WorldToViewportPoint(transform.position);
        if (vpPos.x >= 0f && vpPos.x <= 1f && vpPos.y >= 0f && vpPos.y <= 1f && vpPos.z > 0f) 
        {
            return true;
        }
        return false;
    }
}
