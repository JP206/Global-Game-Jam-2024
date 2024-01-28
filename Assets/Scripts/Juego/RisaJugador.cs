using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RisaJugador : MonoBehaviour
{
    Payaso payaso = null;
    Animator animator;
    int cargaRisa = 5;
    GameObject canvasRisas;
    TextMeshProUGUI textoRisa;
    public bool riendo = false; //riendo es para diferenciar entre microfono y espacio
    public bool sigueJuego = true;
    float contador = 5;
    MovimientoJugador jugador;
    enum Orientacion
    {
        side,
        front,
        back
    }
    Orientacion orientacion = Orientacion.front;

    void Start()
    {
        animator = GetComponent<Animator>();
        canvasRisas = transform.GetChild(0).gameObject;
        textoRisa = GameObject.Find("Barra risa").transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        textoRisa.text = cargaRisa.ToString();
        jugador = GetComponent<MovimientoJugador>();
    }

    void Update()
    {
        if (sigueJuego && Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.F) && contador >= 5)
            {
                risaJugador();
            }
            contador += Time.deltaTime;

            if (canvasRisas.activeSelf)
            {
                canvasRisas.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void risaJugador()
    {
        if (cargaRisa > 0 && !riendo)
        {
            riendo = true;
            if (payaso == null && GameObject.Find("Payaso(Clone)") != null)
            {
                payaso = GameObject.Find("Payaso(Clone)").GetComponent<Payaso>();
            }
            if (payaso != null)
            {
                payaso.AhuyentarPayaso();
            }
            cargaRisa -= 1;
            textoRisa.text = cargaRisa.ToString();
            StartCoroutine(risa());
        }
    }

    IEnumerator risa()
    {
        canvasRisas.SetActive(true);
        if (orientacion.Equals(Orientacion.front))
        {
            animator.SetTrigger("ReirFront");
        }
        else if (orientacion.Equals(Orientacion.side))
        {
            animator.SetTrigger("ReirSide");
        }
        else
        {
            animator.SetTrigger("ReirBack");
        }
        yield return new WaitForSeconds(5);
        canvasRisas.SetActive(false);
        riendo = false;
    }

    public void SetOrientacion(string orientacion)
    {
        if (orientacion.Equals("Side"))
        {
            this.orientacion = Orientacion.side;
        }
        else if (orientacion.Equals("Front"))
        {
            this.orientacion = Orientacion.front;
        }
        else
        {
            this.orientacion = Orientacion.back;
        }
    }
}
