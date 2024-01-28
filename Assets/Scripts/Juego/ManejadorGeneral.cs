using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorGeneral : MonoBehaviour
{
    public GameObject canvasJuegoTerminado, payaso, canvasBarraRisa, carpa, canvasJuegoGanado, canvasCofre, cofre;
    public Sprite carpaAbierta, cofreAbiertoImagen;
    bool cofreAbierto = false;

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
        canvasBarraRisa.SetActive(false);
        GameObject.Find("Jugador").GetComponent<MovimientoJugador>().puedeMover = false;
    }

    public void CofreAbierto()
    {
        cofreAbierto = true;
        cofre.GetComponent<SpriteRenderer>().sprite = cofreAbiertoImagen;
        carpa.GetComponent<SpriteRenderer>().sprite = carpaAbierta;
    }

    public void SalirPuerta()
    {
        if (cofreAbierto)
        {
            canvasJuegoGanado.SetActive(true);
        }
        else
        {
            StartCoroutine(CanvasCofre());
        }
    }

    IEnumerator CanvasCofre()
    {
        canvasCofre.SetActive(true);
        yield return new WaitForSeconds(3);
        canvasCofre.SetActive(false);
    }
}
