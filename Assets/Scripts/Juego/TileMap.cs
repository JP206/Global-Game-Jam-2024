using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMap : MonoBehaviour //chequea recursivamente los objetos en camara
{
    public GameObject[] ObtenerTilesEnCamara()
    {
        LinkedList<GameObject> lista = new();
        obtenerTilesEnCamara(lista);
        return lista.ToArray<GameObject>();
    }

    void obtenerTilesEnCamara(LinkedList<GameObject> lista)
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            if (GetComponent<Tile>().EstaEnCamara())
            {
                lista.AddFirst(gameObject);
            }
        }
        int cantidadDeHijos = transform.childCount;
        for (int i = 0; i < cantidadDeHijos; i++)
        {
            transform.GetChild(i).GetComponent<TileMap>().obtenerTilesEnCamara(lista);
        }
    }

    public GameObject GetCeldaInicio()
    {
        for (int i = 0; i < 10; i++) //columnas
        {
            for (int j = 0; j < 10; j++) //filas
            {
                if (transform.GetChild(i).transform.GetChild(j).GetComponent<Tile>().esInicio)
                {
                    return transform.GetChild(i).transform.GetChild(j).gameObject;
                }
            }
        }
        return null;
    } 
}
