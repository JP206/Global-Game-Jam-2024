using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float velocidadMovimiento;
    Animator animator;
    bool caminando = false;
    public bool sigueJuego = true;
    public GameObject torta;
    RisaJugador risaJugador;
    TextMeshProUGUI textoTortas;
    int cantidadTortas = 5;
    ManejadorGeneral manejadorGeneral;
    bool cofreAbierto = false;
    enum Orientacion
    {
        sideDerecha,
        sideIzquierda,
        front,
        back
    }
    Orientacion orientacion = Orientacion.front;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.position = GameObject.Find("Inicio").transform.position;
        textoTortas = GameObject.Find("Barra risa").transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        textoTortas.text = cantidadTortas.ToString();
        risaJugador = GetComponent<RisaJugador>();
        manejadorGeneral = GameObject.Find("Manejador general").GetComponent<ManejadorGeneral>();
    }

    void Update()
    {
        if (sigueJuego && Time.timeScale == 1)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoverDerecha();
                if (!caminando)
                {
                    animator.SetTrigger("WalkSide");
                    caminando = true;
                    animator.SetBool("RestSide", false);
                }
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (risaJugador.riendo)
                {
                    animator.SetTrigger("ReirSide");
                }
                else
                {
                    animator.SetBool("RestSide", true);
                }
                caminando = false;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoverIzquierda();
                if (!caminando)
                {
                    animator.SetTrigger("WalkSide");
                    caminando = true;
                    animator.SetBool("RestSide", false);
                }
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (risaJugador.riendo)
                {
                    animator.SetTrigger("ReirSide");
                }
                else
                {
                    animator.SetBool("RestSide", true);
                }
                caminando = false;
            }
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                MoverArriba();
                if (!caminando)
                {
                    animator.SetTrigger("WalkBack");
                    caminando = true;
                    animator.SetBool("RestSide", false);
                }
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (risaJugador.riendo)
                {
                    animator.SetTrigger("ReirBack");
                }
                else
                {
                    animator.SetTrigger("RestBack");
                }
                
                caminando = false;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                MoverAbajo();
                if (!caminando)
                {
                    animator.SetTrigger("WalkFront");
                    caminando = true;
                    animator.SetBool("RestSide", false);
                }
            }
            else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                if (risaJugador.riendo)
                {
                    animator.SetTrigger("ReirFront");
                }
                else
                {
                    animator.SetTrigger("RestFront");
                }
                
                caminando = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && cantidadTortas > 0)
            {
                cantidadTortas--;
                textoTortas.text = cantidadTortas.ToString();
                LanzarTorta();
            }
        }
    }

    void MoverDerecha()
    {
        rb.MovePosition(rb.position + new Vector2(velocidadMovimiento, 0));
        transform.rotation = Quaternion.Euler(0, 0, 0);
        risaJugador.SetOrientacion("Side");
        orientacion = Orientacion.sideDerecha;
    }

    void MoverIzquierda()
    {
        rb.MovePosition(rb.position - new Vector2(velocidadMovimiento, 0));
        transform.rotation = Quaternion.Euler(0, 180, 0);
        risaJugador.SetOrientacion("Side");
        orientacion = Orientacion.sideIzquierda;
    }

    void MoverArriba()
    {
        rb.MovePosition(rb.position + new Vector2(0, velocidadMovimiento));
        risaJugador.SetOrientacion("Back");
        orientacion = Orientacion.back;
    }

    void MoverAbajo()
    {
        rb.MovePosition(rb.position - new Vector2(0, velocidadMovimiento));
        risaJugador.SetOrientacion("Front");
        orientacion = Orientacion.front;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Puerta"))
        {
            manejadorGeneral.SalirPuerta();
        }
        if (col.CompareTag("Payaso"))
        {
            manejadorGeneral.TerminarJuego();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Cofre") && !cofreAbierto)
        {
            cofreAbierto = true;
            manejadorGeneral.CofreAbierto();
        }
    }

    public void VolverAnimacion()
    {
        if (orientacion.Equals(Orientacion.front))
        {
            animator.SetBool("RestSide", false);
            animator.SetTrigger("WalkFront");
            animator.SetTrigger("RestFront");
        }
        else if (orientacion.Equals(Orientacion.back))
        {
            animator.SetBool("RestSide", false);
            animator.SetTrigger("WalkBack");
            animator.SetTrigger("RestBack");
        }
        else if (orientacion.Equals(Orientacion.sideDerecha))
        {
            animator.SetTrigger("WalkSide");
            animator.SetBool("RestSide", true);
        }
        else if (orientacion.Equals(Orientacion.sideIzquierda))
        {
            animator.SetTrigger("WalkSide");
            animator.SetBool("RestSide", true);
        }
    }

    void LanzarTorta()
    {
        StartCoroutine(lanzarTorta());
    }

    IEnumerator lanzarTorta()
    {
        if (orientacion.Equals(Orientacion.front))
        {
            Vector2 offset = new Vector2(transform.position.x, transform.position.y - 1.35f);
            animator.SetTrigger("TortaFront");
            yield return new WaitForSeconds(0.5f);
            GameObject tortaInstanciada = Instantiate(torta, offset, Quaternion.identity);
            tortaInstanciada.GetComponent<Torta>().SetOrientacion("Front");
            animator.SetBool("RestSide", false);
            animator.SetTrigger("WalkFront");
            animator.SetTrigger("RestFront");
        }
        else if (orientacion.Equals(Orientacion.back))
        {
            Vector2 offset = new Vector2(transform.position.x , transform.position.y + 2.4f);
            animator.SetTrigger("TortaBack");
            yield return new WaitForSeconds(0.5f);
            GameObject tortaInstanciada = Instantiate(torta, offset, Quaternion.identity);
            tortaInstanciada.GetComponent<Torta>().SetOrientacion("Back");
            animator.SetBool("RestSide", false);
            animator.SetTrigger("WalkBack");
            animator.SetTrigger("RestBack");
        }
        else if (orientacion.Equals(Orientacion.sideDerecha))
        {
            Vector2 offset = new Vector2(transform.position.x + 1.5f, transform.position.y);
            animator.SetTrigger("TortaSide");
            yield return new WaitForSeconds(0.5f);
            GameObject tortaInstanciada = Instantiate(torta, offset, Quaternion.identity);
            tortaInstanciada.GetComponent<Torta>().SetOrientacion("SideDerecha");
            animator.SetTrigger("WalkSide");
            animator.SetBool("RestSide", true);
        }
        else if (orientacion.Equals(Orientacion.sideIzquierda))
        {
            Vector2 offset = new Vector2(transform.position.x - 1.2f, transform.position.y);
            animator.SetTrigger("TortaSide");
            yield return new WaitForSeconds(0.5f);
            GameObject tortaInstanciada = Instantiate(torta, offset, Quaternion.identity);
            tortaInstanciada.GetComponent<Torta>().SetOrientacion("SideIzquierda");
            animator.SetTrigger("WalkSide");
            animator.SetBool("RestSide", true);
        }
    }
}