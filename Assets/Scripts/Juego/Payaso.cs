using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payaso : MonoBehaviour
{
    GameObject jugador, origen;
    [SerializeField] float velocidadMovimiento;
    bool caminandoX = false, caminandoY = false;
    Animator animator;
    public bool puedeMover = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        jugador = GameObject.Find("Player");
    }

    void Update()
    {
        if (puedeMover)
        {
            //primero se alinea en x y despues en y
            if (transform.position.x != jugador.transform.position.x)
            {
                caminandoY = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(jugador.transform.position.x, transform.position.y, 0), velocidadMovimiento * Time.deltaTime);
                if (!caminandoX && transform.position.x < jugador.transform.position.x)
                {
                    caminandoX = true;
                    animator.SetTrigger("ClownSide");
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (!caminandoX && transform.position.x > jugador.transform.position.x)
                {
                    caminandoX = true;
                    animator.SetTrigger("ClownSide");
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }

            if (transform.position.y != jugador.transform.position.y && transform.position.x == jugador.transform.position.x)
            {
                caminandoX = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, jugador.transform.position.y, 0), velocidadMovimiento * Time.deltaTime);
                if (!caminandoY && transform.position.y < jugador.transform.position.y)
                {
                    caminandoY = true;
                    animator.SetTrigger("ClownBack");
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (!caminandoY && transform.position.y > jugador.transform.position.y)
                {
                    caminandoY = true;
                    animator.SetTrigger("ClownFront");
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }

    /// <summary>
    /// Setea la esquina en la pantalla en donde arranco.
    /// 1 es arriba izquierda
    /// 2 es arriba derecha
    /// 3 es abajo iquierda
    /// 4 es abajo derecha
    /// </summary>
    /// <param name="esquinaPantalla"></param>
    public void SetOrigen(int esquinaPantalla)
    {

    }
}

/*GameObject[] grafoTiles;
    int V = 40;
    int[] caminoVertices = new int[40];


    int minDistance(int[] dist,
                    bool[] sptSet)
    {
        // Initialize min value
        int min = int.MaxValue, min_index = -1;

        for (int v = 0; v < V; v++)
            if (sptSet[v] == false && dist[v] <= min && dist[v] != 0)
            {
                min = dist[v];
                min_index = v;
            }

        return min_index;
    }

    // A utility function to print
    // the constructed distance array
    void printSolution(int[] dist, int n)
    {
        for (int i = 0; i < V; i++)
            //print("vertice: " + i + ", distancia: " + dist[i]);
            print(caminoVertices[i]);
    }

    // Function that implements Dijkstra's
    // single source shortest path algorithm
    // for a graph represented using adjacency
    // matrix representation
    void dijkstra(int[,] graph, int src)
    {
        int[] dist = new int[V]; // The output array. dist[i]
                                 // will hold the shortest
                                 // distance from src to i
        int j = 0;

        // sptSet[i] will true if vertex
        // i is included in shortest path
        // tree or shortest distance from
        // src to i is finalized
        bool[] sptSet = new bool[V];

        // Initialize all distances as
        // INFINITE and stpSet[] as false
        for (int i = 0; i < V; i++)
        {
            dist[i] = int.MaxValue;
            sptSet[i] = false;
        }

        // Distance of source vertex
        // from itself is always 0
        dist[src] = 0;

        // Find shortest path for all vertices
        for (int count = 0; count < V - 1; count++)
        {
            // Pick the minimum distance vertex
            // from the set of vertices not yet
            // processed. u is always equal to
            // src in first iteration.
            int u = minDistance(dist, sptSet);

            // Mark the picked vertex as processed
            sptSet[u] = true;

            // Update dist value of the adjacent
            // vertices of the picked vertex.
            for (int v = 0; v < V; v++)

                // Update dist[v] only if is not in
                // sptSet, there is an edge from u
                // to v, and total weight of path
                // from src to v through u is smaller
                // than current value of dist[v]
                if (!sptSet[v] && graph[u, v] != 0 &&
                     dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                {
                    dist[v] = dist[u] + graph[u, v];
                    caminoVertices[j] = graph[u, v];
                    j++;
                }

            //guardar aca el objeto
        }

        // print the constructed distance array
        printSolution(dist, V);
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            grafoTiles = GameObject.Find("TileMap").GetComponent<TileMap>().ObtenerTilesEnCamara();
            dijkstra(CrearGrafoTiles(), 0);
        }
    }

    int[,] CrearGrafoTiles()
    {
        int[] patron = { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 };
        int offset = -5;
        int recorrerPatron = 5;
        int contarHastaOffset = 0;
        int[,] resultado = new int[40, 40];
        int poner0en7 = 1;
        int poner0en3 = 0;
        for (int i = 0; i < 40; i++)
        {
            if (offset < 0)
            {
                recorrerPatron = Math.Abs(offset);
            }
            else
            {
                recorrerPatron = 0;
            }
            for (int j = 0; j < 40; j++)
            {
                if (offset < 0)
                {
                    if (recorrerPatron < patron.Length)
                    {
                        resultado[i, j] = patron[recorrerPatron];
                        recorrerPatron++;
                    }
                    else
                    {
                        resultado[i, j] = 0;
                    }
                }
                else
                {
                    if (contarHastaOffset == offset && recorrerPatron < patron.Length)
                    {
                        resultado[i, j] = patron[recorrerPatron];
                        recorrerPatron++;
                    }
                    else
                    {
                        contarHastaOffset++;
                        resultado[i, j] = 0;
                    }
                }
                if (poner0en3 == 5)
                {
                    resultado[i, j] = 0;
                    poner0en3 = 0;
                }
                if (poner0en7 == 5)
                {
                    resultado[i, j] = 0;
                    poner0en7 = 0;
                }
            }
            poner0en3++;
            poner0en7++;
            offset++;
            recorrerPatron = 0;
            contarHastaOffset = 0;
        }
        return resultado;
    }*/