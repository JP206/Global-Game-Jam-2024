using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class Payaso : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    GameObject jugador;
    Animator animator;
    bool caminandoArriba = false, caminandoAbajo = false, caminandoCostado = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        jugador = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        navMeshAgent.SetDestination(jugador.transform.position);
        if (Mathf.Abs(navMeshAgent.velocity.x) > 1f && !caminandoCostado && navMeshAgent.velocity.y < 1f)
        {
            animator.SetTrigger("ClownSide");
            caminandoCostado = true;
            caminandoArriba = false;
            caminandoAbajo = false;
        }
        else if (navMeshAgent.velocity.y > 1f && !caminandoArriba && Mathf.Abs(navMeshAgent.velocity.x) < 1f)
        {
            animator.SetTrigger("ClownBack");
            caminandoCostado = false;
            caminandoArriba = true;
            caminandoAbajo = false;
        }
        else if (navMeshAgent.velocity.y < -1f && !caminandoAbajo && Mathf.Abs(navMeshAgent.velocity.x) < 1f)
        {
            animator.SetTrigger("ClownFront");
            caminandoCostado = false;
            caminandoArriba = false;
            caminandoAbajo = true;
        }
    }
}