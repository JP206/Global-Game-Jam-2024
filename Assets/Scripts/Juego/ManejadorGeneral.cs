using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorGeneral : MonoBehaviour
{
    public GameObject canvasJuegoTerminado, payaso, canvasBarraRisa, carpa, canvasJuegoGanado, canvasCofre, cofre, canvasCofreAbierto, canvasPausa;
    public Sprite carpaAbierta, cofreAbiertoImagen;
    bool cofreAbierto = false;
    bool animacionCofre = false;

    void Start()
    {
        StartCoroutine(SpawnPayasoDelay());
        cofre.SetActive(false);
        StartCoroutine(AparecerCofre());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && Time.timeScale == 0)
        {
            animacionCofre = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu principal");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (canvasPausa.activeSelf)
            {
                canvasPausa.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
                canvasPausa.SetActive(true);
            }
        }
    }

    IEnumerator AparecerCofre()
    {
        yield return new WaitForSeconds(60);
        cofre.SetActive(true);
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
        Camera.main.GetComponent<AudioSource>().Stop();
        GameObject jugador = GameObject.Find("Jugador");
        jugador.GetComponent<MovimientoJugador>().sigueJuego = false;
        jugador.GetComponent<RisaJugador>().sigueJuego = false;
        jugador.GetComponent<InputMicrofono>().sigueJuego = false;
    }

    public void CofreAbierto()
    {
        cofreAbierto = true;
        cofre.GetComponent<SpriteRenderer>().sprite = cofreAbiertoImagen;
        carpa.GetComponent<SpriteRenderer>().sprite = carpaAbierta;
        StartCoroutine(AbrirCofreAnimacion());
    }

    IEnumerator AbrirCofreAnimacion()
    {
        GameObject jugador = GameObject.Find("Jugador");
        jugador.GetComponent<Animator>().SetTrigger("Llave");
        yield return new WaitForSeconds(0.4f);
        GameObject.Find("Jugador").transform.GetChild(1).gameObject.SetActive(true);
        canvasCofreAbierto.SetActive(true);
        Time.timeScale = 0;
        cofre.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitUntil(() => animacionCofre);
        Time.timeScale = 1;
        cofre.transform.GetChild(0).gameObject.SetActive(false);
        canvasCofreAbierto.SetActive(false);
        GameObject.Find("Jugador").transform.GetChild(1).gameObject.SetActive(false);
        jugador.GetComponent<MovimientoJugador>().VolverAnimacion();
    }

    public void SalirPuerta()
    {
        if (cofreAbierto)
        {
            canvasJuegoGanado.SetActive(true);
            GameObject jugador = GameObject.Find("Jugador");
            GameObject.Find("Payaso(Clone)").GetComponent<Payaso>().puedeMover = false;
            jugador.GetComponent<MovimientoJugador>().sigueJuego = false;
            jugador.GetComponent<RisaJugador>().sigueJuego = false;
            jugador.GetComponent<InputMicrofono>().sigueJuego = false;
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
