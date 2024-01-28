using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RisaJugador : MonoBehaviour
{
    Payaso payaso = null;
    Animator animator;
    int cargaRisa = 100;
    bool riendo = false;
    Slider slider;
    public Gradient gradient;
    Image relleno;
    GameObject canvasRisas;
    enum Orientacion
    {
        side,
        front,
        back
    }
    Orientacion orientacion = Orientacion.front;

    void Start()
    {
        slider = GameObject.Find("Barra risa").transform.GetChild(1).GetComponent<Slider>();
        relleno = GameObject.Find("Barra risa").transform.GetChild(1).transform.GetChild(1).GetComponent<Image>();
        animator = GetComponent<Animator>();
        slider.maxValue = cargaRisa;
        slider.value = cargaRisa;
        relleno.color = gradient.Evaluate(1f);
        canvasRisas = transform.GetChild(0).gameObject;
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
        if (payaso == null && GameObject.Find("Payaso(Clone)") != null)
        {
            payaso = GameObject.Find("Payaso(Clone)").GetComponent<Payaso>();
        }
        if (payaso != null)
        {
            payaso.AhuyentarPayaso();
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
            riendo = true;
            StartCoroutine(risa());
        }
    }

    IEnumerator risa()
    {
        canvasRisas.SetActive(true);
        canvasRisas.transform.rotation = Quaternion.Euler(0, 0, 0);
        while (riendo)
        {
            if (cargaRisa > 0)
            {
                cargaRisa -= 5;
                slider.value = cargaRisa;
                relleno.color = gradient.Evaluate(slider.normalizedValue);
                if (cargaRisa <= 0)
                {
                    break;
                }
                yield return new WaitForSeconds(1);
            }
            yield return null;
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
        riendo = false;
        StopAllCoroutines();
    }
}
