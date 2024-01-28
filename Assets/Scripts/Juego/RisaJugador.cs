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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            risaJugador();
        }
    }

    void risaJugador()
    {
        if (cargaRisa > 0)
        {
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
        canvasRisas.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        yield return new WaitForSeconds(2);
        canvasRisas.SetActive(false);
        canvasRisas.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (orientacion.Equals(Orientacion.front))
        {
            animator.SetBool("RestSide", false);
            animator.SetTrigger("WalkFront");
            animator.SetTrigger("RestFront");
        }
        else if (orientacion.Equals(Orientacion.side))
        {
            animator.SetTrigger("WalkSide");
            animator.SetBool("RestSide", true);
        }
        else
        {
            animator.SetBool("RestSide", false);
            animator.SetTrigger("WalkBack");
            animator.SetTrigger("RestBack");
        }
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
        canvasRisas.SetActive(false);
        StopAllCoroutines();
    }
}
