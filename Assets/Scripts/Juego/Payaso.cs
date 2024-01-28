using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Payaso : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    GameObject jugador;
    Animator animator;
    bool caminandoArriba = false, caminandoAbajo = false, caminandoCostado = false, perseguirJugador = true;
    public bool puedeMover = true;
    float cambiarDireccion = 0.8f;
    Vector3 esquina1, esquina2, esquina3, esquina4, objetivo;
    //las 4 esquinas de mapa para dirigirse a una de ellas cuando el jugador se rie
    AudioSource audioSource;
    public AudioClip sonidoImpactoTorta, sonidoRisaPersigue, sonidoSpawn;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        jugador = GameObject.Find("Jugador");
        objetivo = jugador.transform.position;
        esquina1 = GameObject.Find("Esquina1").transform.position;
        esquina2 = GameObject.Find("Esquina2").transform.position;
        esquina3 = GameObject.Find("Esquina3").transform.position;
        esquina4 = GameObject.Find("Esquina4").transform.position;
        animator = GetComponent<Animator>();
        audioSource.PlayOneShot(sonidoSpawn);
    }

    void Update()
    {
        if (puedeMover)
        {
            if (perseguirJugador)
            {
                navMeshAgent.SetDestination(jugador.transform.position);
            }
            else
            {
                navMeshAgent.SetDestination(objetivo);
            }
            if (Mathf.Abs(navMeshAgent.velocity.x) > cambiarDireccion && !caminandoCostado && navMeshAgent.velocity.y < cambiarDireccion)
            {
                animator.SetTrigger("ClownSide");
                if (navMeshAgent.velocity.normalized.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                caminandoCostado = true;
                caminandoArriba = false;
                caminandoAbajo = false;
            }
            else if (navMeshAgent.velocity.y > cambiarDireccion && !caminandoArriba && Mathf.Abs(navMeshAgent.velocity.x) < cambiarDireccion)
            {
                animator.SetTrigger("ClownBack");
                caminandoCostado = false;
                caminandoArriba = true;
                caminandoAbajo = false;

            }
            else if (navMeshAgent.velocity.y < -cambiarDireccion && !caminandoAbajo && Mathf.Abs(navMeshAgent.velocity.x) < cambiarDireccion)
            {
                animator.SetTrigger("ClownFront");
                caminandoCostado = false;
                caminandoArriba = false;
                caminandoAbajo = true;
            }
        }
    }

    public void AturdirPayaso()
    {
        StartCoroutine(aturdirPayaso());
        audioSource.PlayOneShot(sonidoImpactoTorta);
    }

    IEnumerator aturdirPayaso()
    {
        if (caminandoCostado)
        {
            animator.SetTrigger("TortaSide");
        }
        else if (caminandoArriba)
        {
            animator.SetTrigger("TortaBack");
        }
        else if (caminandoAbajo)
        {
            animator.SetTrigger("TortaFront");
        }
        puedeMover = false;
        navMeshAgent.speed = 0;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(5);
        if (caminandoCostado)
        {
            animator.SetTrigger("ClownSide");
        }
        else if (caminandoArriba)
        {
            animator.SetTrigger("ClownBack");
        }
        else if (caminandoAbajo)
        {
            animator.SetTrigger("ClownFront");
        }
        navMeshAgent.speed = 2;
        audioSource.PlayOneShot(sonidoRisaPersigue);
        GetComponent<CapsuleCollider2D>().enabled = true;
        puedeMover = true;
    }

    public void AhuyentarPayaso()
    {
        StartCoroutine(ahuyentarPayaso());
    }

    IEnumerator ahuyentarPayaso()
    {
        navMeshAgent.speed = 6;
        objetivo = CalcularEsquinaMasAlejadaDelJugador();
        perseguirJugador = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(10);
        audioSource.PlayOneShot(sonidoRisaPersigue);
        GetComponent<CapsuleCollider2D>().enabled = true;
        navMeshAgent.speed = 2;
        perseguirJugador = true;
    }

    Vector3 CalcularEsquinaMasAlejadaDelJugador()
    {
        float distanciaEsquina1 = Vector3.Distance(jugador.transform.position, esquina1);
        float distanciaEsquina2 = Vector3.Distance(jugador.transform.position, esquina2);
        float distanciaEsquina3 = Vector3.Distance(jugador.transform.position, esquina3);
        float distanciaEsquina4 = Vector3.Distance(jugador.transform.position, esquina4);
        float max1, max2;
        Vector3 vector1, vector2;
        if (distanciaEsquina1 < distanciaEsquina2)
        {
            max1 = distanciaEsquina2;
            vector1 = esquina2;
        }
        else
        {
            max1 = distanciaEsquina1;
            vector1 = esquina1;
        }
        if (distanciaEsquina3 < distanciaEsquina4)
        {
            max2 = distanciaEsquina4;
            vector2 = esquina4;
        }
        else
        {
            max2 = distanciaEsquina3;
            vector2 = esquina3;
        }
        if (max1 < max2)
        {
            return vector2;
        }
        else
        {
            return vector1;
        }
    }
}