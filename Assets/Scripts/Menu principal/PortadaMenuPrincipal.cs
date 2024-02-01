using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortadaMenuPrincipal : MonoBehaviour
{
    public GameObject portada;
    
    void Start()
    {
        StartCoroutine(EsconderPortada());
    }

    IEnumerator EsconderPortada()
    {
        yield return new WaitForSeconds(2.35f);
        portada.SetActive(false);
    }
}
