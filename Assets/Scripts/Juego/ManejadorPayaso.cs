using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorPayaso : MonoBehaviour
{
    public GameObject payaso;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AparecerPayaso();
        }
    }

    /// <summary>
    /// Aparece aleatorio el payaso en una de las esquinas de la camara. 
    /// Los indices de las esquinas son:
    /// abajo derecha: 0
    /// abajo izquierda: 35
    /// arriba izquierda: 39
    /// arriba derecha: 4
    /// </summary>
    void AparecerPayaso()
    {
        //obtengo los tiles que se ven en la camara
        /*GameObject[] arrayTilesEnCamara = GameObject.Find("TileMap").GetComponent<TileMap>().ObtenerTilesEnCamara();
        int random = Random.Range(1, 5);
        if (random == 1) //arriba izquierda
        {
            GameObject payasoInstanciado = Instantiate(payaso, arrayTilesEnCamara[39].transform.position, Quaternion.identity);
            //payasoInstanciado.GetComponent<Payaso>().SetOrigen(1);
        }
        else if (random == 2) //arriba derecha
        {
            GameObject payasoInstanciado = Instantiate(payaso, arrayTilesEnCamara[4].transform.position, Quaternion.identity);
            //payasoInstanciado.GetComponent<Payaso>().SetOrigen(2);
        }
        else if (random == 3) //abajo izquierda
        {
            GameObject payasoInstanciado = Instantiate(payaso, arrayTilesEnCamara[35].transform.position, Quaternion.identity);
            //payasoInstanciado.GetComponent<Payaso>().SetOrigen(3);
        }
        else //abajo derecha
        {
            GameObject payasoInstanciado = Instantiate(payaso, arrayTilesEnCamara[0].transform.position, Quaternion.identity);
            //payasoInstanciado.GetComponent<Payaso>().SetOrigen(4);
        }*/
    }
}
