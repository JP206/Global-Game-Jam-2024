using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float velocidadMovimiento;
    Animator animator;
    bool caminando = false;
    public bool puedeMover = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform.position = GameObject.Find("TileMap").GetComponent<TileMap>().GetCeldaInicio().transform.position;
    }

    void Update()
    {
        if (puedeMover)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoverDerecha();
                if (!caminando)
                {
                    animator.SetTrigger("WalkSide");
                    caminando = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                animator.SetTrigger("RestSide");
                caminando = false;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoverIzquierda();
                if (!caminando)
                {
                    animator.SetTrigger("WalkSide");
                    caminando = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                animator.SetTrigger("RestSide");
                caminando = false;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                MoverArriba();
                if (!caminando)
                {
                    animator.SetTrigger("WalkBack");
                    caminando = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                animator.SetTrigger("RestBack");
                caminando = false;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                MoverAbajo();
                if (!caminando)
                {
                    animator.SetTrigger("WalkFront");
                    caminando = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                animator.SetTrigger("RestFront");
                caminando = false;
            }
        }
    }

    void MoverDerecha()
    {
        rb.MovePosition(rb.position + new Vector2(velocidadMovimiento, 0));
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void MoverIzquierda()
    {
        rb.MovePosition(rb.position - new Vector2(velocidadMovimiento, 0));
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void MoverArriba()
    {
        rb.MovePosition(rb.position + new Vector2(0, velocidadMovimiento));
    }

    void MoverAbajo()
    {
        rb.MovePosition(rb.position - new Vector2(0, velocidadMovimiento));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Tile"))
        {
            if (col.GetComponent<Tile>().esFinal)
            {
                GetComponent<FuncionesAuxiliares>().CambiarEscena("Nivel 2");
            }
        }
        if (col.CompareTag("Payaso"))
        {
            GameObject.Find("Manejador general").GetComponent<ManejadorGeneral>().TerminarJuego();
        }
    }
}
