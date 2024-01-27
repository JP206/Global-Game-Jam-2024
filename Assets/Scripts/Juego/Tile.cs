using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool esObstaculo, esInicio, esFinal;

    void Start()
    {
        if (esObstaculo)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
        if (esInicio || esFinal)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
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
