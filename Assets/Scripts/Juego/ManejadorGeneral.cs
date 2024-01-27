using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorGeneral : MonoBehaviour
{
    public GameObject canvasJuegoTerminado, payaso;

    void Start()
    {
        StartCoroutine(SpawnPayasoDelay());
    }

    IEnumerator SpawnPayasoDelay()
    {
        yield return new WaitForSeconds(10);
        Instantiate(payaso, GameObject.Find("Inicio").transform.position, Quaternion.identity);
    }
        
    public void TerminarJuego()
    {
        canvasJuegoTerminado.SetActive(true);
        GameObject.Find("Jugador").GetComponent<MovimientoJugador>().puedeMover = false;
    }
}
